using UnityEngine;
using System.Collections;

public class AlphaUnitBehavior : MonoBehaviour {

	
	GenericUnitBehavior gun;
	
	// Use this for initialization
	void Start ()
	{
		gun = GetComponent<GenericUnitBehavior> ();
		
		
	}
	
	protected void SetStartValues()
	{
		if (gun != null) 
		{
			gun.GatheringRate = 100.0f;
			gun.MovementSpeed = 15.0f;
			gun.ResourceCapacity = -1f;
			gun.ResourceLoad = 3000.0f; 
			gun.Integrity = 200.0f;
		}
	}

}
