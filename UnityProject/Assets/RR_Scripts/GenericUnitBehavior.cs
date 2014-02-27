using UnityEngine;
using System.Collections;

public class GenericUnitBehavior : MonoBehaviour
{

	public enum State
	{
		Dummy,
		ChaseTarget,
		FollowPlayer,
		GatherNearestResourcePoint,
		ReturnToBase,
		HoldDefensive,

		//Other states go here.
	}

	//Property Instantiation Section
	public State state = State.FollowPlayer;

	public string AllyTag { get; set; }

	protected float GatheringRate { get; set; }

	protected float AttackRate { get; set; }

	protected float RateModifer { get; set; }

	protected float MovementSpeed { get; set; }

	protected float ResourceCapacity { get; set; }

	protected float ResourceLoad { get; set; }

	protected float Integrity { get; set; } //This is kinda like a healthbar. This shouldn't be very important in our prototype's mode of play.;

	protected Vector3 startPosition;

	protected const float distanceTreshold = 5.0f;

	public GameObject target;

	//Event Methods - Start

	// Use this for initialization
	void Start ()
	{
		startPosition = transform.position;

		MovementSpeed = 0.1f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (state)
		{
		case State.Dummy:
				break;

		case State.ChaseTarget:
			//Pick the cloest target with the appropriate tag and move towards it. - Moore
			target = FindClosestGameObjectWithTag("Target");
			FlyTowardsGameObject(target);


			//If too far from the start position, teleport back to start.
			if (Vector3.Distance(startPosition, transform.position) > 100) 
			{ 
				transform.position = startPosition;
			}

			break;

		case State.FollowPlayer:
			target = FindClosestGameObjectWithTag("Player");
			FlyTowardsGameObject(target);
			break;

		case State.GatherNearestResourcePoint:
			target = FindClosestGameObjectWithTag("ResourcePoint");
			//GatherResources(target); //To make this work, we'd need to get the script component and reference that. - Moore
			break;

		case State.ReturnToBase:

			break;



		default:
			{
				break;
			}

		}
		


	}

	//Event Methods - End

	//Accessor Methods - Start

	//Accessor Methods - End

	//Mutator Methods - Start

	//Mutator Methods - End

	//Utility Methods - Start

	protected void FlyTowardsGameObject(GameObject destination)
	{
		if (destination != null) 
		{
			//Make sure to check that the target is within a desirable distance. So there should be a maximum distance variable. - Moore
			transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z ));
			if (Vector3.Distance(transform.position, destination.transform.position) > distanceTreshold) //4:23 Defect - Had the < intstead of >.
			{
				transform.position += (transform.forward * MovementSpeed);
			}
		}
	}

	GameObject FindClosestGameObjectWithTag(string tagToFind)
	{
		GameObject result = null;
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tagToFind);
		
		foreach (GameObject current in allObjects)
		{
			if (current != this.gameObject)
			{
				if (result == null)
				{
					result = current;
				} 
				else
				{
					if (Vector3.Distance(transform.position, result.transform.position) > Vector3.Distance(transform.position, current.transform.position))
					{
						result = current;
					}
				}
			}
		}
		return result;
	}

	//Utility Methods - end
}
