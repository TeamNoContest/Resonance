using UnityEngine;
using System.Collections;

public class GenericUnitBehavior : MonoBehaviour
{

	public enum State
	{
		Dummy,
		ChaseTarget,
		//Other states go here.
	}

	//Property Instantiation Section
	public State state = State.ChaseTarget;

	public string AllyTag { get; set; }

	private float GatheringRate { get; set; }

	private float AttackRate { get; set; }

	private float RateModifer { get; set; }

	private float MovementSpeed { get; set; }

	private float ResourceCapacity { get; set; }

	private float ResourceLoad { get; set; }

	private float Integrity { get; set; } //This is kinda like a healthbar. This shouldn't be very important in our prototype's mode of play.;

	private Vector3 startPosition;

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
			
			if (target != null) 
			{
				//Make sure to check that the target is within a desirable distance. So there should be a maximum distance variable. - Moore
				transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z ));
				transform.position += (transform.forward * MovementSpeed);
			}
			
			//If too far from the start position, teleport back to start.
			if (Vector3.Distance(startPosition, transform.position) > 100) 
			{ 
				transform.position = startPosition;
			}

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
