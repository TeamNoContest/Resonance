// This script must be put on the same GameObject as GameController.cs.

using UnityEngine;
using System.Collections;

public class UnitSpawn : MonoBehaviour
{
	GameController gcScript;
	bool isPaused;

	// Used for calling the Spawn event
	public delegate void SpawnUnit(string unit);
	public static event SpawnUnit Spawn;

	void Start()
	{
		gcScript = gameObject.GetComponent<GameController>();
		isPaused = false;
	}

	void OnEnable()
	{
		GameController.OnPause += HandleOnPause;
	}

	void OnDisable()
	{
		GameController.OnPause -= HandleOnPause;
	}

	void OnGUI()
	{
		if(Input.GetButton("Spawn Menu") && !isPaused)
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