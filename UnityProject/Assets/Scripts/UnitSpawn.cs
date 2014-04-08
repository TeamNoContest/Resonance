// This script must be put on the same GameObject as GameController.cs.

using UnityEngine;
using System.Collections;

public class UnitSpawn : GUIScript
{
	bool isSpawnWindowOpen;
	Rect popupWindowRect;

	// Used for calling the Spawn event
	public delegate void SpawnUnit(string unit);
	public static event SpawnUnit Spawn;

	new void Start()
	{
		base.Start();
		popupWindowRect = new Rect(Screen.width - popupWindowWidth, Screen.height - popupWindowHeight, popupWindowWidth, popupWindowHeight);
		isSpawnWindowOpen = false;
	}

	void Update()
	{
		if((Input.GetAxis("Spawn Menu") == 1 || Input.GetButton("Spawn Menu")) && !isPaused)
		{
			isSpawnWindowOpen = true;
			if(Input.GetButtonDown("Fire1"))
			{
				Spawn("interceptor");
			}
			else if(Input.GetButtonDown("Fire2"))
			{
				Spawn("freighter");
			}
			else if(Input.GetButtonDown("Fire3"))
			{
				Spawn("resonator");
			}
		}
		else
		{
			isSpawnWindowOpen = false;
		}
	}

	void OnGUI()
	{
		if(isSpawnWindowOpen)
		{
			GUI.Window(0, popupWindowRect, UnitSpawnWindow, "Unit Spawn");
		}
	}

	void UnitSpawnWindow(int windowID)
	{
		GUI.Label(new Rect(popupWindowWidth * (1f/7f), popupWindowHeight * (1f/5f), 70, 30), "A");
		GUI.Label(new Rect(popupWindowWidth * (1f/3f), popupWindowHeight * (1f/5f), 70, 30), "Interceptor");
		GUI.Label(new Rect(popupWindowWidth * (5f/7f), popupWindowHeight * (1f/5f), 70, 30), gcScript.InterceptorCost.ToString());
		GUI.Label(new Rect(popupWindowWidth * (1f/7f), popupWindowHeight * (2f/5f), 70, 30), "B");
		GUI.Label(new Rect(popupWindowWidth * (1f/3f), popupWindowHeight * (2f/5f), 70, 30), "Freighter");
		GUI.Label(new Rect(popupWindowWidth * (5f/7f), popupWindowHeight * (2f/5f), 70, 30), gcScript.FreighterCost.ToString());
		GUI.Label(new Rect(popupWindowWidth * (1f/7f), popupWindowHeight * (3f/5f), 70, 30), "X");
		GUI.Label(new Rect(popupWindowWidth * (1f/3f), popupWindowHeight * (3f/5f), 70, 30), "Resonator");
		GUI.Label(new Rect(popupWindowWidth * (5f/7f), popupWindowHeight * (3f/5f), 70, 30), gcScript.ResonatorCost.ToString());
	}
}