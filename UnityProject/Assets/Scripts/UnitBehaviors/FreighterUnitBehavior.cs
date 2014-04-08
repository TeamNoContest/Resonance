using UnityEngine;
using System.Collections;

public class FreighterUnitBehavior : MonoBehaviour
{
	
	GenericUnitBehavior gun;

	// Use this for initialization
	void Start ()
	{
		gun = GetComponent<GenericUnitBehavior> ();

	
	}

	void FixedUpdate ()
	{
		if (gun != null) 
		{
			/*
			switch (gun.state) {
			case State.Dummy:
				break;
					
			case State.Stay:
					//Distinct from dummy. If this unit has distinct behaviors like attacking, it should still perform those duties... just not in this script. - Moore
				 SendMessage("ApplyBrakes", null, SendMessageOptions.DontRequireReceiver);
				break;
					
			case State.FollowPlayer:
				gun.Target = LibRevel.FindClosestGameObjectWithTag (gameObject, "Player");
				 SendMessage("FlyTowardsGameObjectWithSmartBraking", gun.Target, SendMessageOptions.DontRequireReceiver);
				break;
					
			case State.GatherNearestResourcePoint:
					//Search for the nearest resource point. - Moore
				gun.Target = LibRevel.FindClosestGameObjectWithTag (gameObject, "ResourcePoint");
					
					//If close enough, draw resources from it. If not, then fly closer. - Moore
				if (LibRevel.IsWithinDistanceThreshold (gameObject, gun.Target, gun.DistanceThreshold)) {
					 SendMessage("ApplyBrakes", null, SendMessageOptions.DontRequireReceiver);
					SendMessage("GatherResourcesFromSource", gun.Target, SendMessageOptions.DontRequireReceiver);
				} else {
					 SendMessage("FlyTowardsGameObjectWithSmartBraking", gun.Target, SendMessageOptions.DontRequireReceiver);
				}
					
					//If my resources are maxed out, return to the alpha ship (player) - Moore.
				if (gun.ResourceLoad >= gun.ResourceCapacity) {
					gun.state = State.DropoffAtFreighter;
				}
					
				break;
					
			case State.ReturnToBase:
				gun.Target = LibRevel.FindClosestGameObjectWithTag (gameObject, "Player"); //This had been planned to be a separate ship, but now it refers to the player. - Moore
				if (LibRevel.IsWithinDistanceThreshold (gameObject, gun.Target, gun.DistanceThreshold)) {
					 SendMessage("ApplyBrakes", null, SendMessageOptions.DontRequireReceiver);
					SendMessage("TransferResourcesToSource", gun.Target, SendMessageOptions.DontRequireReceiver); //If you're close enough to the player, drop off your resources until you're empty. - Moore
				} else {
					 SendMessage("FlyTowardsGameObjectWithSmartBraking", gun.Target, SendMessageOptions.DontRequireReceiver); //If you're not close enough, get closer. - Moore.
				}
					
					//If no more resources to deposit, go back to the default behavior of following the player.
				if (gun.ResourceLoad <= 0.0f) {
					gun.state = State.FollowPlayer;
				}
					
				break;
					
			case State.DropoffAtFreighter:
				gun.state = State.ReturnToBase;
				
				break;

			case State.Attack:
				gun.state = gun.prevState;
				
				break;
					
					
					
			default:
				{
					break;
				}

			}
			*/
			
			//Any time the state changes, update the status indicator.
			if (gun.prevState != gun.state) {
				SendMessage("UpdateStatusIndicator", null, SendMessageOptions.DontRequireReceiver);
				gun.prevState = gun.state;
			}

			//Freighters shouldn't drop off at themselves. Potential race-condition here in that the Freighter might have the wrong state for one update tick, but it should correct quickly. - Moore.
			if (gun.state == State.DropoffAtFreighter) {
				gun.state = State.ReturnToBase;
			}
		}
	}

	protected void SetStartValues()
	{
		if (gun != null) 
		{
			gun.GatheringRate = 10.0f;
			gun.MovementSpeed = 5.0f;
			gun.ResourceCapacity = 1000.0f;
			gun.ResourceLoad = 0.0f; 
			gun.Integrity = 200.0f;
		}
	}

}
