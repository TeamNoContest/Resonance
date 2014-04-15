using UnityEngine;
using System.Collections;

public class ResonatorUnitBehavior : MonoBehaviour {

	GenericUnitBehavior gun;
	public ResonatorEffectAreaBehavior reab;

	const float resonatorEffectStartThreshold = 300f;
	const float resonatorEffectCutoffThreshold = 100f;

	public bool resonating;


	// Use this for initialization
	void Start () 
	{
		gun = GetComponent<GenericUnitBehavior>();
		reab = GetComponentInChildren<ResonatorEffectAreaBehavior>(); //Unlike the reab in the Generic Unit Behavior, this refers to our own reab, not one from a collision. - Moore
	}

	void Update () 
	{
		if (gun != null && reab != null)
		{
			//Drain some resources if we have them to boost others nearby. But only if there are others nearby. Do not include self as a boostable.
			if (gun.ResourceLoad >= resonatorEffectStartThreshold && reab.GetNumberOfResonatingUnits() > 0)
			{
				resonating = true;
			}

			else if (gun.ResourceLoad <= resonatorEffectCutoffThreshold)
			{
				resonating = false;
			}

			if (resonating)
			{
				//Deduct the cost of resonating all units in my AoE.
				gun.ConsumeResources((Time.deltaTime * reab.GetNumberOfResonatingUnits())); // "Time.deltaTime * reab.GetNumberOfResonatingUnits()" = One Res Cost per Second Per Unit applied constantly.

				//Apply the Boost.
				//Suggestion: This could be a gradually building boost in increments of +0.01. That way, multiple resonators overlapping would be beneficial. But it would have a flaw when leaving the range of any one resonator.

				//gun.RateModifier = 5; //Uncomment this if you want the Resonator to have the boost effect on itself. It WON'T keep track of itself in the event, though.
				reab.SetRateModifer(5f);
			}
		}
	}

	protected void SetStartValues()
	{
		if (gun != null)
		{
			gun.MovementSpeed = 10.0f;
			gun.ResourceCapacity = 600.0f;
			gun.ResourceLoad = 290.0f;
			gun.GatheringRate = 2.0f;
			gun.RateModifier = 1.0f;
			gun.Integrity = 50f;
		}
	}
}
