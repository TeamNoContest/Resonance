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

	// Use this for initialization
	void Start()
	{
		gameMode = GameMode.TimeLimit;
		resourceCurrent = 100;
		resourceGoal = 1000;
		timeCurrent = 0f;
		timeGoal = 3600f;	// one hour time limit
		interestRate = 0.05f;
		interestTime = 5f;
		unitCount = 0;
		unitCap = 10;

		InvokeRepeating("AwardInterest", interestTime, interestTime);
	}

	// Update is called once per frame
	void Update()
	{
		// Listen for Unit Increase/Decrease event.
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
}