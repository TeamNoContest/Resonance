using UnityEngine;
using System.Collections;

enum GameMode { TimeLimit };

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

	//I realize I shouldn't directly manipulate your code, but this is done to have the always up-to-date value from the player/mothership. - Moore
	GameObject thePlayer;
	GenericUnitBehavior thePlayerScript;

	// Use this for initialization
	void Start()
	{
		gameMode = GameMode.TimeLimit;
		resourceCurrent = 100;
		resourceGoal = 1000;
		timeCurrent = 0f;
		timeGoal = 3600f;	// one hour time limit
		interestRate = 0.01f; // This basically means a bonus 1 point per second for every 200, or - Moore
		interestTime = 5f;
		unitCount = 0;
		unitCap = 10;

		//InvokeRepeating("AwardInterest", interestTime, interestTime); // Sorry for tweaking this without asking first. Gonna use Time.deltaTime for continuous intrest accruement. - Moore

		//Initializing the values that retlate this to the other script. 
		//This is not the best OOP because of the forced coupling. If you have an idea for how to keep the functionality and reduce this coupling, let me know. - Moore
		thePlayer = GameObject.FindGameObjectWithTag("Player");
		thePlayerScript = thePlayer.GetComponent<GenericUnitBehavior>();
		resourceCurrent = (int)thePlayerScript.ResourceLoad;

	}

	// Update is called once per frame
	void Update()
	{
		// Listen for Unit Increase/Decrease event.

		//Keeping the value up to date with respect to the player - Moore.
		if (thePlayerScript != null)
		{
			resourceCurrent = (int)thePlayerScript.ResourceLoad;
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
		if(gameMode == GameMode.TimeLimit)
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
}