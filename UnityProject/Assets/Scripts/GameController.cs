// This script must be put on the same GameObject as UnitSpawn.cs.

using UnityEngine;
using System.Collections;

public enum GameMode { TIME_LIMIT };
public enum RunState { RUNNING, PAUSED };

public class GameController : MonoBehaviour
{
	#region Class variables
	GameMode gameMode;					// Game mode being played
	GameObject player;
	GenericUnitBehavior playerScript;

	// Percentage of current resources gained per second
	private float _interestRate;
	public float InterestRate { get { return _interestRate; } }

	//float interestTime;		// The amount of time in seconds between interest awards

	// Number of resources inside the player's ship
	private float _currentResources;
	public float CurrentResources { get { return _currentResources; } }
	// Number of resources required to win; if negative, there is no resource goal
	private float _resourceGoal;
	public float ResourceGoal { get { return _resourceGoal; } }

	// Time elapsed since beginning of game, in seconds
	private float _currentTime;
	public float CurrentTime { get { return _currentTime; } }
	// Time alloted for player to win, in seconds; if negative, there is no resource goal
	private float _timeGoal;
	public float TimeGoal { get { return _timeGoal; } }

	// The minimum and maximum bounds of the playing area.
	private float _minBoundX;
	public float MinBoundX { get { return _minBoundX; } }
	private float _maxBoundX;
	public float MaxBoundX { get { return _maxBoundX; } }
	private float _minBoundZ;
	public float MinBoundZ { get { return _minBoundZ; } }
	private float _maxBoundZ;
	public float MaxBoundZ { get { return _maxBoundZ; } }

	// Current and maximum number of units
	private int _unitCount;
	public int UnitCount { get { return _unitCount; } }
	private int _unitCap;
	public int UnitCap { get { return _unitCap; } }

	// Costs to create the units
	private int _interceptorCost;
	public int InterceptorCost { get { return _interceptorCost; } }
	private int _freighterCost;
	public int FreighterCost { get { return _freighterCost; } }
	private int _resonatorCost;
	public int ResonatorCost { get { return _resonatorCost; } }

	public RunState runState;		// Used to mark the game as paused or running

	// Variables to hold the unit prefabs for the purpose of spawning new units
	public GameObject interceptorPrefab, freighterPrefab, resonatorPrefab;

	// Variable to hold the Menu Screen objects
	public GameObject menuObjectsRoot;

	// Used for calling the OnPause event. The flag indicates whether the game is being paused (true) or unpaused (false).
	public delegate void PauseEventHandler(bool flag);
	public static event PauseEventHandler OnPause;
	#endregion

	void Start()
	{
		gameMode = GameMode.TIME_LIMIT;
		runState = RunState.RUNNING;
		_resourceGoal = 10000f;
		_currentTime = 0f;
		_timeGoal = 300f;	// five-minute time limit
		_interestRate = 0.001f;
		//interestTime = 5f;
		_minBoundX = -500;
		_maxBoundX = 500;
		_minBoundZ = -500;
		_maxBoundZ = 500;
		_unitCount = 0;
		_unitCap = 10;
		_interceptorCost = 1000;
		_freighterCost = 1000;
		_resonatorCost = 1000;

		//InvokeRepeating("AwardInterest", interestTime, interestTime); // Sorry for tweaking this without asking first. Gonna use Time.deltaTime for continuous intrest accruement. - Moore

		//Initializing the values that retlate this to the other script. 
		//This is not the best OOP because of the forced coupling. If you have an idea for how to keep the functionality and reduce this coupling, let me know. - Moore
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<GenericUnitBehavior>();
		_currentResources = playerScript.ResourceLoad;
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
		// Keep the value up to date with respect to the player
		if(playerScript != null)
		{
			_currentResources = playerScript.ResourceLoad;
		}

		// Check for pause input
		if(Input.GetButtonDown("Start"))
		{
			if(runState == RunState.RUNNING)
			{
				PauseGame(true);
			}
			else if(runState == RunState.PAUSED)
			{
				PauseGame(false);
			}
		}

		// Check for Resource Cheat input
		if(Input.GetKeyDown(KeyCode.End))
		{
			playerScript.ResourceLoad += 1000;
		}
	}

	void LateUpdate()
	{
		_currentTime += Time.deltaTime;	// Increase time by the amount of time since last update.
		CheckWinLoss();
	}

	/// <summary>
	/// Checks if the player has won or lost the game.
	/// </summary>
	void CheckWinLoss()
	{
		if(gameMode == GameMode.TIME_LIMIT)
		{
			if(_currentResources >= _resourceGoal)
			{
				// Player wins
				Time.timeScale = 0;
				Application.LoadLevel(2);
			}
			else if(_currentTime >= _timeGoal)
			{
				// Player loses
				Time.timeScale = 0;
				Application.LoadLevel(3);
			}
		}
	}

	public void PauseGame(bool shouldPause)
	{
		if(shouldPause)
		{
			Time.timeScale = 0;
			runState = RunState.PAUSED;
			menuObjectsRoot.SetActive(true);
			OnPause(true);
		}
		else
		{
			menuObjectsRoot.SetActive(false);
			Time.timeScale = 1;
			runState = RunState.RUNNING;
			OnPause(false);
		}
	}

	void SpawnUnit(string unit)
	{
		if(_unitCount < _unitCap)
		{
			GameObject unitPrefab;
			int unitCost;

			switch(unit.ToLower())
			{
			case "interceptor":
				unitPrefab = interceptorPrefab;
				unitCost = _interceptorCost;
				break;
			case "freighter":
				unitPrefab = freighterPrefab;
				unitCost = _freighterCost;
				break;
			case "resonator":
				unitPrefab = resonatorPrefab;
				unitCost = _resonatorCost;
				break;
			default:
				unitPrefab = null;
				unitCost = 0;
				break;
			}

			if(unitPrefab != null && _currentResources >= unitCost)
			{
				Instantiate(unitPrefab, player.transform.position, Quaternion.identity);
				playerScript.ResourceLoad -= unitCost;
				_unitCount++;
			}
		}
	}
}