using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
	GameObject gcProp;
	GameController gcScript;
	int resourceCurrent, resourceGoal;
	float timeCurrent, timeGoal;

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

		if(GUI.Button(new Rect(10, Screen.height - 85, 75, 75), "Restart"))
		{
			Application.LoadLevel(0);
		}
	}
}