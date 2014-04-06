using UnityEngine;
using System.Collections;

public class ResonatorUnitBehavior : MonoBehaviour {

	GenericUnitBehavior gun;

	// Use this for initialization
	void Start () {
		gun = GetComponent<GenericUnitBehavior>();
		if (gun != null)
		{
			gun.MovementSpeed = 10.0f;
			gun.ResourceCapacity = 600.0f;
			gun.ResourceLoad = 0.0f;
			gun.GatheringRate = 2.0f;
			gun.RateModifier = 1.0f;
		}
	
	}
}
