﻿using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
	GameObject gcProp;
	GameController gcScript;
	int resourceCurrent, resourceGoal;
	float timeCurrent, timeGoal;

	public delegate void SpawnUnit(string unit);
	public static event SpawnUnit Spawn;

	void Start()
	{
		gcProp = GameObject.Find("Game Controller Prop");
		gcScript = gcProp.GetComponent<GameController>();
	}

	void Update()
	{
		resourceCurrent = gcScript.GetResources()[0];
		resourceGoal = gcScript.GetResources()[1];
		timeCurrent = gcScript.GetTimes()[0];
		timeGoal = gcScript.GetTimes()[1];
	}

	void OnGUI()
	{
		if(gcScript.runState == RunState.RUNNING)
		{
			#region Display Time
			// If the game mode does not have a time goal...
			if(timeGoal < 0)
			{
				//...display time elapsed
				GUI.Label(new Rect(10, 10, 175, 50), "Time Elapsed: " + timeCurrent);
			}
			else
			{
				//...else display time remaining
				GUI.Label(new Rect(10, 10, 175, 50), "Time Remaining: " + (timeGoal - timeCurrent));
			}
			#endregion

			#region Display Resources
			// If the game mode does not have a resource goal...
			if(resourceGoal < 0)
			{
				//...display only current resources
				GUI.Label(new Rect(Screen.width - 150, 10, 150, 50),
				          "Resources: " + resourceCurrent);
			}
			else
			{
				//...else display current resources and resource goal
				GUI.Label(new Rect(Screen.width - 150, 10, 150, 50),
				          "Resources: " + resourceCurrent + "/" + resourceGoal);
			}
			#endregion

			#region Unit Spawn Window
			if(Input.GetButton("Spawn Menu"))
			{
				GUI.Window(0, new Rect(Screen.width - 200, Screen.height - 200, 200, 200), UnitSpawnWindow, "Unit Spawn");

				string unitToSpawn = "";
				if(Input.GetButtonDown("Fire1"))
				{
					unitToSpawn = "interceptor";
				}
				else if(Input.GetButtonDown("Fire2"))
				{
					unitToSpawn = "freighter";
				}
				else if(Input.GetButtonDown("Fire3"))
				{
					unitToSpawn = "resonator";
				}

				Spawn(unitToSpawn);
				// Trigger event and pass unitToSpawn
			}
			#endregion
		}

		#region Display Restart Button
		/*if(GUI.Button(new Rect(10, Screen.height - 85, 75, 75), "Restart"))
		{
			Application.LoadLevel("DefaultTestingScene");
		}
		*/
		#endregion
	}

	void UnitSpawnWindow(int windowID)
	{
		GUI.Label(new Rect(10, 40, 70, 30), "1");
		GUI.Label(new Rect(50, 40, 70, 30), "Interceptor");
		GUI.Label(new Rect(130, 40, 70, 30), gcScript.GetUnitCosts()[0].ToString());
		GUI.Label(new Rect(10, 100, 70, 30), "2");
		GUI.Label(new Rect(50, 100, 70, 30), "Freighter");
		GUI.Label(new Rect(130, 100, 70, 30), gcScript.GetUnitCosts()[1].ToString());
		GUI.Label(new Rect(10, 160, 70, 30), "3");
		GUI.Label(new Rect(50, 160, 70, 30), "Resonator");
		GUI.Label(new Rect(130, 160, 70, 30), gcScript.GetUnitCosts()[2].ToString());
	}
}