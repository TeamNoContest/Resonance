using UnityEngine;
using System.Collections;

public class InterceptorUnitBehavior : MonoBehaviour {
	
	GenericUnitBehavior gun;

	float attackRate;

	// Use this for initialization
	void Start () 
	{
		gun = GetComponent<GenericUnitBehavior>();
	
	}

	protected void SetStartValues()
	{
		if (gun != null)
		{
			//This can't be put in the start because timing DOES matter. Instead, GenericUnitBehavior will broadcast a message asking to overwrite values. - Moore
			gun.GatheringRate = 1.0f;
			gun.MovementSpeed = 25.0f;
			gun.ResourceCapacity = 250.0f;
			gun.ResourceLoad = 0.0f;
			gun.Integrity = 100.0f;
			
			//This property is unique to interceptors.
			attackRate = 0.1f;
		}
	}

}
