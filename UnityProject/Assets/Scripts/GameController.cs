// This script must be put on the same GameObject as UnitSpawn.cs.

using UnityEngine;
using System.Collections;

public enum GameMode { TIME_LIMIT };
public enum RunState { RUNNING, PAUSED };

public class GameController : MonoBehaviour
{
	GameMode gameMode;		// Game mode being played
	int resourceCurrent;	// Number of resources banked in the mothership
	int resourceGoal;		// Number of resources to win;
							// if negative, game mode doesn't have resource goal
	float timeCurrent;		// Time elapsed since beginning of game, in seconds
	float timeGoal;			// Amount of time alloted for player to win, in seconds;
							// if negative, game mode doesn't have time goal
	float interestRate;		// Percentage of curent resources gained per second
	float interestTime;		// The amount of time in seconds between interest awards
	int unitCount;			// Current number of units under player control
	int unitCap;			// Maximum number of units available to purchase

	public RunState runState;		// Used to mark the game as paused or running
	public int interceptorCost;		// Cost in resources to create a new Interceptor
	public int freighterCost;		// Cost in resources to create a new Freighter
	public int resonatorCost;		// Cost in resources to create a new Resonator

	// Variables to hold the unit prefabs for the purpose of spawning new units
	public GameObject interceptorPrefab, freighterPrefab, resonatorPrefab;

	// Variable to hold the Menu Screen objects
	public GameObject menuObjectsRoot;

	// Used for calling the OnPause event. The flag indicates whether the game is being paused (true) or unpaused (false).
	public delegate void PauseEventHandler(bool flag);
	public static event PauseEventHandler OnPause;

	//I realize I shouldn't directly manipulate your code, but this is done to have the always up-to-date value from the player/mothership. - Moore
	GameObject player;
	GenericUnitBehavior playerScript;

	void Start()
	{
		gameMode = GameMode.TIME_LIMIT;
		runState = RunState.RUNNING;
		resourceCurrent = 100;
		resourceGoal = 1000;
		timeCurrent = 0f;
		timeGoal = 3600f;	// one hour time limit
		interestRate = 0.01f; // This basically means a bonus 1 point per second for every 200, or - Moore
		interestTime = 5f;
		unitCount = 0;
		unitCap = 10;
		interceptorCost = 1000;
		freighterCost = 1000;
		resonatorCost = 1000;

		//InvokeRepeating("AwardInterest", interestTime, interestTime); // Sorry for tweaking this without asking first. Gonna use Time.deltaTime for continuous intrest accruement. - Moore

		//Initializing the values that retlate this to the other script. 
		//This is not the best OOP because of the forced coupling. If you have an idea for how to keep the functionality and reduce this coupling, let me know. - Moore
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<GenericUnitBehavior>();
		resourceCurrent = (int)playerScript.ResourceLoad;
	}

	void OnEnable()
	{
		UnitSpawn.Spawn += SpawnUnit;
	}

	void OnDisable()
	{
		UnitSpawn.Spawn -= SpawnUnit;
	}

	void Update()
	{
		// Listen for Unit Increase/Decrease event.

		//Keeping the value up to date with respect to the player - Moore.
		if (playerScript != null)
		{
			resourceCurrent = (int)playerScript.ResourceLoad;
		}

		// Check for pause input
		if(Input.GetButtonDown("Start"))
		{
			if(runState == RunState.RUNNING)
			{
				Time.timeScale = 0;
				runState = RunState.PAUSED;
				menuObjectsRoot.SetActive(true);
				OnPause(true);
			}
			else if(runState == RunState.PAUSED)
			{
				menuObjectsRoot.SetActive(false);
				Time.timeScale = 1;
				runState = RunState.RUNNING;
				OnPause(false);
			}
		}
	}

	void LateUpdate()
	{
		timeCurrent += Time.deltaTime;	// Increase time by the amount of time since last update.
		CheckWinLoss();
	}

	/// <summary>
	/// Award the player with resources based of compound interest.
	/// </summary>
	void AwardInterest()
	{
		resourceCurrent = Mathf.FloorToInt(resourceCurrent + resourceCurrent * interestRate);
	}

	/// <summary>
	/// Checks if the player has won or lost the game.
	/// </summary>
	void CheckWinLoss()
	{
		if(gameMode == GameMode.TIME_LIMIT)
		{
			if(resourceCurrent >= resourceGoal)
			{
				// Player wins
			}
			else if(timeCurrent >= timeGoal)
			{
				// Player loses
			}
		}
	}

	void SpawnUnit(string unit)
	{
		if(unitCount < unitCap)
		{
			switch(unit.ToLower())
			{
			case "interceptor":
				Instantiate(interceptorPrefab, player.transform.position, Quaternion.identity);
				resourceCurrent -= interceptorCost;
				break;
			case "freighter":
				Instantiate(freighterPrefab, player.transform.position, Quaternion.identity);
				resourceCurrent -= freighterCost;
				break;
			case "resonator":
				Instantiate(resonatorPrefab, player.transform.position, Quaternion.identity);
				resourceCurrent -= resonatorCost;
				break;
			default:
				break;
			}
			unitCount++;
		}
	}

	public int[] GetResources()
	{
		int[] temp = {resourceCurrent, resourceGoal};
		return temp;
	}

	public float[] GetTimes()
	{
		float[] temp = {timeCurrent, timeGoal};
		return temp;
	}

	public float GetInterestRate() //ACCESSOR: Added this so I can check it in the Units. - Moore
	{
		return interestRate;
	}

	public int[] GetUnitCosts()
	{
		int[] temp = {interceptorCost, freighterCost, resonatorCost};
		return temp;
	}

	public int[] GetUnitCountAndCap()
	{
		int[] temp = {unitCount, unitCap};
		return temp;
	}
}