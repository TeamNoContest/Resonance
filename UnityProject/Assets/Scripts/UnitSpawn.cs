// This script must be put on the same GameObject as GameController.cs.

using UnityEngine;
using System.Collections;

public class UnitSpawn : MonoBehaviour
{
	GameController gcScript;
	bool isPaused;
	bool isSpawnWindowOpen;
	int input;

	// Used for calling the Spawn event
	public delegate void SpawnUnit(string unit);
	public static event SpawnUnit Spawn;

	void Start()
	{
		gcScript = gameObject.GetComponent<GameController>();
		isPaused = false;
		isSpawnWindowOpen = false;
		input = 0;
	}

	void OnEnable()
	{
		GameController.OnPause += HandleOnPause;
	}

	void OnDisable()
	{
		GameController.OnPause -= HandleOnPause;
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
			GUI.Window(0, new Rect(Screen.width - 200, Screen.height - 200, 200, 200), UnitSpawnWindow, "Unit Spawn");
		}
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

	void HandleOnPause(bool flag)
	{
		isPaused = flag;
	}
}