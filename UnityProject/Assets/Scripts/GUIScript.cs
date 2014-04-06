using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
	protected GameController gcScript;
	protected int screenWidth, screenHeight;
	protected int popupWindowWidth, popupWindowHeight;
	protected bool isPaused;

	public GameObject gameController;

	protected void Start()
	{
		popupWindowWidth = Screen.width / 5;
		popupWindowHeight = Screen.height / 2;

		gcScript = gameController.GetComponent<GameController>();
		isPaused = false;
	}

	protected void OnEnable()
	{
		GameController.OnPause += HandleOnPause;
	}

	protected void OnDisable()
	{
		GameController.OnPause -= HandleOnPause;
	}

	void OnGUI()
	{
		if(!isPaused)
		{
			#region Display Time
			// If the game mode does not have a time goal...
			if(gcScript.TimeGoal < 0)
			{
				//...display time elapsed
				GUI.Label(new Rect(10, 10, 175, 50), "Time Elapsed: " + Mathf.FloorToInt(gcScript.CurrentTime));
			}
			else
			{
				//...else display time remaining
				GUI.Label(new Rect(10, 10, 175, 50), "Time Remaining: " + Mathf.FloorToInt(gcScript.TimeGoal - gcScript.CurrentTime));
			}
			#endregion

			#region Display Unit Cap
			GUI.Label(new Rect(Screen.width / 2 - 25, 10, 100, 50), "Units: " + gcScript.UnitCount + "/" + gcScript.UnitCap);
			#endregion

			#region Display Resources
			// If the game mode does not have a resource goal...
			if(gcScript.ResourceGoal < 0)
			{
				//...display only current resources
				GUI.Label(new Rect(Screen.width - 150, 10, 150, 50),
				          "Resources: " + gcScript.CurrentResources.ToString("F0"));
			}
			else
			{
				//...else display current resources and resource goal
				GUI.Label(new Rect(Screen.width - 150, 10, 150, 50),
				          "Resources: " + gcScript.CurrentResources.ToString("F0") + "/" + gcScript.ResourceGoal);
			}
			#endregion
		}
	}

	protected void HandleOnPause(bool flag)
	{
		isPaused = flag;
	}
}