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
		DropoffAtFreighter,
		Attack,
		
		//Other states go here.
	}

	public enum ShipType
	{
		Interceptor,
		Freighter,
		Alpha,
		Resonator,
		Node,
	}

	//Property Instantiation Section
	public State state = State.FollowPlayer;

	public ShipType shipType;

	public string AllyTag { get; set; }

	protected float gatheringRate { get; set; }

	protected float attackRate { get; set; }

	protected float rateModifer { get; set; }

	protected float movementSpeed { get; set; }

	protected float resourceCapacity { get; set; }

	protected float resourceLoad { get; set; }

	protected float integrity { get; set; } //This is kinda like a healthbar. This shouldn't be very important in our prototype's mode of play.;

	protected float interestRate { get; set; }// This one is taken from the GameController and updated with each update. - Moore

	protected Vector3 startPosition;

	protected const float distanceTreshold = 20.0f;

	private bool isPaused;

	public GameObject target;
	public GameObject altTarget;
	public GameObject playingArea;
	public GameObject theGameController;

	//Templates for creating more units into the field.

	public GameObject templateInterceptor;
	public GameObject templateResonator;
	public GameObject templateFreighter;

	protected GameController theGameControllerScript;

	//Event Methods - Start
	void OnEnable()
	{
		GameController.OnPause += HandleOnPause;
	}

	void OnDisable()
	{
		GameController.OnPause += HandleOnPause;
	}

	// Use this for initialization
	void Start ()
	{
		if (shipType == null) {shipType = ShipType.Node;} //Defaults to Node only if a type is not selected.
		if (shipType == ShipType.Alpha) {theGameControllerScript = theGameController.GetComponent<GameController>();}

		// NOTE FROM JARED:
		// I commented out the line above and took this out of the 'if' statement.
		// I need access to this script so I can detect if the game is paused.
		// (Refer to the OnGUI method.)
		//theGameController = GameObject.Find("Game Controller Prop");
		//theGameControllerScript = theGameController.GetComponent<GameController>();

		playingArea = GameObject.FindGameObjectWithTag("PlayingArea");

		startPosition = transform.position;

		movementSpeed = 10.0f;
		resourceCapacity = 600.0f;
		ResourceLoad = 500.0f;
		gatheringRate = 2.0f;
		rateModifer = 1.0f;

		interestRate = 0.005f; //Reminder: This will be overwritten each update by the value in the Game Controller. - Moore

		isPaused = false;

		//TODO: Use a switch case here to vary up the starting stats based on whatever shipType this unit is.
		switch (shipType)
		{
		case ShipType.Alpha:
		{
			gatheringRate = 100.0f;
			attackRate = 0.0f;
			movementSpeed = 15.0f;
			resourceCapacity = -1.0f;
			ResourceLoad = 1400.0f;
			integrity = 100.0f;
			break;
		}
		case ShipType.Freighter:
		{
			gatheringRate = 10.0f;
			attackRate = 0.0f;
			movementSpeed = 5.0f;
			resourceCapacity = 1000.0f;
			ResourceLoad = 500.0f; 
			integrity = 200.0f;
			break;
		}
		case ShipType.Interceptor:
		{
			gatheringRate = 1.0f;
			attackRate = 0.1f;
			movementSpeed = 25.0f;
			resourceCapacity = 250.0f;
			ResourceLoad = 50.0f;
			integrity = 100.0f;
			break;
		}

		default:
			break;
		}
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
			//Search for the nearest resource point. - Moore
			target = FindClosestGameObjectWithTag("ResourcePoint");

			//If close enough, draw resources from it. If not, then fly closer. - Moore
			if (IsWithinDistanceThreshold(target))
			{
				GatherResourcesFromSource(target);
			}
			else
			{
				FlyTowardsGameObject(target);
			}

			//If my resources are maxed out, return to the alpha ship (player) - Moore.
			if (ResourceLoad >= resourceCapacity)
			{
				if (shipType == ShipType.Freighter){state = State.ReturnToBase;}
				else {state = State.DropoffAtFreighter;}
			}

			break;

		case State.ReturnToBase:
			target = FindClosestGameObjectWithTag("Player"); //This had been planned to be a separate ship, but now it refers to the player. - Moore
			if (IsWithinDistanceThreshold(target))
			{
				TransferResourcesToSource(target); //If you're close enough to the player, drop off your resources until you're empty. - Moore
			}
			else
			{
				FlyTowardsGameObject(target); //If you're not close enough, get closer. - Moore.
			}
			
			//If no more resources to deposit, go back to the default behavior of following the player.
			if (ResourceLoad <= 0.0f)
			{
				state = State.GatherNearestResourcePoint;
			}
			
			break;

		case State.DropoffAtFreighter:
			target = FindClosestGameObjectWithTag("Freighter");
			altTarget = FindClosestGameObjectWithTag("Player");

			//Change the target to point at the same thing as the altTarget if the altTarget is closer.
			if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.transform.position.x, target.transform.position.z)) > Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(altTarget.transform.position.x, altTarget.transform.position.z)))
		    {
				target = altTarget;
			}

			if (IsWithinDistanceThreshold(target))
			{
				TransferResourcesToSource(target); //If you're close enough to the player, drop off your resources until you're empty. - Moore
			}
			else
			{
				FlyTowardsGameObject(target); //If you're not close enough, get closer. - Moore.
			}
			
			//If no more resources to deposit, go back to the default behavior of following the player.
			if (ResourceLoad <= 0.0f)
			{
				state = State.GatherNearestResourcePoint;
			}
			
			break;



		default:
			{
				break;
			}
		}

		//Done cycling through options based on the state. This is now based on the ship's type.
		switch (shipType)
		{
		case ShipType.Alpha:
		{
			//Only the player gains interest and communicates with the GameController.
			if (theGameControllerScript != null)
			{
				//Update the interest from the GameController.
				interestRate = theGameControllerScript.GetInterestRate();
				//Bank the interest from existing resources.
				ResourceLoad += ResourceLoad * interestRate * Time.deltaTime;

				/* NOTE FROM JARED: I'm commenting out the autospend to test my manual spend.
				//If the player has enough resources, auto-spend some of it to make more units. - Moore
				//TODO: It would be preferable to get rid of auto-spend and instead have the player use a command to make more units at will.
				if (ResourceLoad > 1500) 
				{
					ResourceLoad -= 1500;

					//I was never able to make this version work.
					//GameObject newAlliedUnit = Instantiate(Resources.Load<GameObject>("/RR_Prefabs/Interceptor"), transform.position, Quaternion.identity) as GameObject;
					//GameObject newAlliedUnit2 = Instantiate(Resources.Load<GameObject>("RR_Prefabs/Resonator"), transform.position, Quaternion.identity) as GameObject;
					//GameObject newAlliedUnit3 = Instantiate(Resources.Load<GameObject>("RR_Prefabs/Freighter"), transform.position, Quaternion.identity) as GameObject;


					GameObject newAlliedUnit = Instantiate(templateInterceptor, transform.position, Quaternion.identity) as GameObject;
					GameObject newAlliedUnit2 = Instantiate(templateResonator, transform.position, Quaternion.identity) as GameObject;
					GameObject newAlliedUnit3 = Instantiate(templateFreighter, transform.position, Quaternion.identity) as GameObject;
				}
				*/
			}
			break;
		}
		case ShipType.Node:
		{
			//Only the player gains interest and communicates with the GameController.
			if (ResourceLoad <= 0)
			{
				//Destroy(gameObject);
				ResourceLoad += Random.Range(200, 2000);
				transform.position = RandomVector3InRange(playingArea.renderer.bounds.min.x, playingArea.renderer.bounds.max.x,0,0,playingArea.renderer.bounds.min.z, playingArea.renderer.bounds.max.z);

			}
			break;
		}
			//Interceptors should occasionally attack other units.
			//Freighters don't have much in the way of special behavior, but may cause resource nodes to appear when attacked?
			//To make the AI smarter, maybe consider having them drop to the nearest freighter instead of the player if the freighter is closer. - Moore
			//Resonators should spend some of their Resource load to boost other allied units.
		default:
			break;
		}

	}

	//Credit to Jared Cerbin for coming up with this GUI stuff.
	void OnGUI()
	{
		// Display the GUI only if the game is not paused. Take this out of the 'if' to see why. - Jared
		if(!isPaused)
		{
			Vector2 labelPos = Camera.main.WorldToScreenPoint(transform.position);
			GUI.Label(new Rect(labelPos.x, Screen.height - labelPos.y - 30, 50, 20), ResourceLoad.ToString("F0")); //I did a slight tweak to not draw the decimial portion of the resource thinger. - Moore
		}
	}

	//Event Methods - End

	//Accessor/Mutator Methods - Start

	public float ResourceLoad
	{
		get {return resourceLoad;} //Accessor
		set {resourceLoad = value;} //Mutator
	}

	//Accessor/Mutator Methods - End

	//Mutator/Logic Methods - Start



	public float GatherResourcesFromSource(GameObject targetObject)
	{
		//Precondition: You should really only call this method once per calling object per update. - Moore
		float result = 0.0f;
		GenericUnitBehavior gubScript = targetObject.GetComponent<GenericUnitBehavior>();
		if (gubScript != null)
		{
			result = gubScript.TransferResourcesToSource(gameObject);
		}
		return result;
	}

	public float TransferResourcesToSource(GameObject targetObject)
	{

		//Precondition: You should only call this once per calling object per update.

		/*
		result = 0.0f;
		GenericUnitBehavior gubScript = targetObject.GetComponent<GenericUnitBehavior>();
		if (gubScript != null)
		{
		result = gubScript.GatherResourcesFromSource(this);
		}
		return result;
		// This version is a redirection helper mathod to swap arguments from GatherResourcesFromSource. DELETEME - Moore.
		 */


		//Check how many resources the target wants to take.
		float result = 0.0f;
		float amountRequested = 0.0f;
		GenericUnitBehavior gubScript = targetObject.GetComponent<GenericUnitBehavior>();

		amountRequested = gubScript.gatheringRate * gubScript.rateModifer * Time.deltaTime;

		//Can't draw more than the capacity. - Moore
		if (gubScript.resourceCapacity < 0.0f)
		{
			;// This is here because a negative capacity effectively means infinite capacity. It prevents the following branch from bouncing back.
		}
		else if (amountRequested + gubScript.ResourceLoad > gubScript.resourceCapacity)
		{
			amountRequested = gubScript.resourceCapacity - gubScript.ResourceLoad;
		}

		//If this request can be fulfilled in full, do so. 
		if (amountRequested <= ResourceLoad)
		{
			ResourceLoad -= amountRequested;
			gubScript.ResourceLoad += amountRequested;
		}
		else //If not, give all that remains. Leave cleanup for another method to deal with. - Moore.
		{
			amountRequested = ResourceLoad;
			ResourceLoad -= amountRequested;
			gubScript.ResourceLoad += amountRequested;
		}

		result = amountRequested;
		return result;


	}

	//Mutator/Logic Methods - End

	//Utility Methods - Start

	protected void FlyTowardsGameObject(GameObject destination)
	{
		//Precondition: You should only be calling this once per caller per update. - Moore
		if (destination != null) 
		{
			//Make sure to check that the target is within a desirable distance. So there should be a maximum distance variable. - Moore
			transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z ));
			if (IsNotWithinDistanceThreshold(destination)) //4:23 Defect - Had the < intstead of >.
			{
				transform.position += (transform.forward * movementSpeed * rateModifer * Time.deltaTime);
			}
		}
	}

	protected bool IsWithinDistanceThreshold(GameObject destination)
	{
		bool result = false;

		if (Vector3.Distance(transform.position, destination.transform.position) > distanceTreshold) //If it is outside of the threshold...
		{
			result = false; //Return false. - Moore
		}

		else // Otherwise, return true. - Moore
		{
			return true;
		}

		return result;
	}

	//Convenience Negation Wrapper Method for the above:
	protected bool IsNotWithinDistanceThreshold(GameObject destination)
	{
		return !IsWithinDistanceThreshold(destination);
	}

	protected GameObject FindClosestGameObjectWithTag(string tagToFind)
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

	protected Vector3 RandomVector3InRange(float xLow, float xHigh, float yLow, float yHigh, float zLow, float zHigh)
	{
		float tempX = Random.Range(xLow, xHigh);
		float tempY = Random.Range(yLow, yHigh);
		float tempZ = Random.Range(zLow, zHigh);

		return new Vector3(tempX, tempY, tempZ);
	}

	// This is the class that handles the OnPause event, which is broadcast by GameController whenever the RunState changes.
	void HandleOnPause(bool flag)
	{
		isPaused = flag;
	}
	//Utility Methods - end
}
