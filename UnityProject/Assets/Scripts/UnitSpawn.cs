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
		if(Input.GetButton("Spawn Menu") && !isPaused)
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
		GUI.Label(new Rect(10, 40, 70, 30), "A");
		GUI.Label(new Rect(50, 40, 70, 30), "Interceptor");
		GUI.Label(new Rect(130, 40, 70, 30), gcScript.InterceptorCost.ToString());
		GUI.Label(new Rect(10, 100, 70, 30), "B");
		GUI.Label(new Rect(50, 100, 70, 30), "Freighter");
		GUI.Label(new Rect(130, 100, 70, 30), gcScript.FreighterCost.ToString());
		GUI.Label(new Rect(10, 160, 70, 30), "X");
		GUI.Label(new Rect(50, 160, 70, 30), "Resonator");
		GUI.Label(new Rect(130, 160, 70, 30), gcScript.ResonatorCost.ToString());
	}
}