using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
	protected GameController gcScript;
	protected int screenWidth, screenHeight;
	protected int popupWindowWidth, popupWindowHeight;
	protected bool isPaused;

	public GameObject gameController;
	public GUIStyle textStyleBase;
	private GUIStyle textStyleCenter, textStyleRight;

	protected void Start()
	{
		popupWindowWidth = Screen.width / 5;
		popupWindowHeight = Screen.height / 2;

		gcScript = gameController.GetComponent<GameController>();
		isPaused = false;

		textStyleBase.name = "Text Left";
		textStyleBase.normal.textColor = Color.white;
		textStyleBase.fontSize = 30;

		textStyleCenter = new GUIStyle(textStyleBase);
		textStyleCenter.alignment = TextAnchor.UpperCenter;

		textStyleRight = new GUIStyle(textStyleBase);
		textStyleRight.alignment = TextAnchor.UpperRight;
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
				// ...display time elapsed
				GUI.Label(new Rect(10, 10, 300, 50), "Time Elapsed: " + Mathf.FloorToInt(gcScript.CurrentTime), textStyleBase);
			}
			else
			{
				// ...else display time remaining
				GUI.Label(new Rect(10, 10, 300, 50), "Time Remaining: " + Mathf.FloorToInt(gcScript.TimeGoal - gcScript.CurrentTime), textStyleBase);
			}
			#endregion

			#region Display Unit Cap
			GUI.Label(new Rect(Screen.width / 2 - 25, 10, 100, 50), "Units: " + gcScript.UnitCount + "/" + gcScript.UnitCap, textStyleCenter);
			#endregion

			#region Display Resources
			// If the game mode does not have a resource goal...
			if(gcScript.ResourceGoal < 0)
			{
				// ...display only current resources
				GUI.Label(new Rect(Screen.width - 125, 10, 100, 50),
				          "Resources: " + gcScript.CurrentResources.ToString("F0"), textStyleRight);
			}
			else
			{
				// ...else display current resources and resource goal
				GUI.Label(new Rect(Screen.width - 125, 10, 100, 50),
				          "Resources: " + gcScript.CurrentResources.ToString("F0") + "/" + gcScript.ResourceGoal, textStyleRight);
			}
			#endregion
		}
	}

	protected void HandleOnPause(bool flag)
	{
		isPaused = flag;
	}
}