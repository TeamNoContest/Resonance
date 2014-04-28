// This script must be put on the same GameObject as GameController.cs.

using UnityEngine;
using System.Collections;

public class UnitSpawn : GUIScript
{
	bool isSpawnWindowOpen;
	Rect popupWindowRect;
	GUIStyle windowStyle, windowTextStyle;

	public Texture xboxA, xboxB, xboxX, xboxY;

	// Used for calling the Spawn event
	public delegate void SpawnUnit(string unit);
	public static event SpawnUnit Spawn;

	new void Start()
	{
		base.Start();
		popupWindowRect = new Rect(Screen.width - popupWindowWidth, Screen.height - popupWindowHeight, popupWindowWidth, popupWindowHeight);
		isSpawnWindowOpen = false;

		windowStyle = new GUIStyle();
		windowTextStyle = new GUIStyle(base.textStyleBase);
		windowTextStyle.fontSize = 20;
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
			GUI.Window(0, popupWindowRect, UnitSpawnWindow, "Unit Spawn", windowStyle);
		}
	}

	void UnitSpawnWindow(int windowID)
	{
		GUI.DrawTexture(new Rect(popupWindowWidth * 0.05f, popupWindowHeight * 0.25f, 50, 50), xboxA);
		GUI.Label(new Rect(popupWindowWidth * 0.30f, popupWindowHeight * 0.30f, 70, 30), "Interceptor", windowTextStyle);
		GUI.Label(new Rect(popupWindowWidth * 0.75f, popupWindowHeight * 0.30f, 70, 30), gcScript.InterceptorCost.ToString(), windowTextStyle);
		GUI.DrawTexture(new Rect(popupWindowWidth * 0.05f, popupWindowHeight * 0.50f, 50, 50), xboxB);
		GUI.Label(new Rect(popupWindowWidth * 0.30f, popupWindowHeight * 0.55f, 70, 30), "Freighter", windowTextStyle);
		GUI.Label(new Rect(popupWindowWidth * 0.75f, popupWindowHeight * 0.55f, 70, 30), gcScript.FreighterCost.ToString(), windowTextStyle);
		GUI.DrawTexture(new Rect(popupWindowWidth * 0.05f, popupWindowHeight * 0.75f, 50, 50), xboxX);
		GUI.Label(new Rect(popupWindowWidth * 0.30f, popupWindowHeight * 0.80f, 70, 30), "Resonator", windowTextStyle);
		GUI.Label(new Rect(popupWindowWidth * 0.75f, popupWindowHeight * 0.80f, 70, 30), gcScript.ResonatorCost.ToString(), windowTextStyle);
	}
}