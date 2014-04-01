using UnityEngine;
using System.Collections;

public enum Command { ATTACK, COLLECT, STAY, UNLOAD }

public class CommandHandler : MonoBehaviour
{
	const int NUMBER_OF_PAGES = 2;
	bool[] isCommandWindowOpen;
	Rect[] windowRect;

	void Start()
	{
		isCommandWindowOpen = new bool[NUMBER_OF_PAGES];
		for(int i = 0; i < isCommandWindowOpen.Length; i++)
		{
			isCommandWindowOpen[i] = false;
		}

		windowRect = new Rect[NUMBER_OF_PAGES];
		for(int i = 0; i < windowRect.Length; i++)
		{
			windowRect[i] = false;
		}
	}

	void Update()
	{
		if(Input.GetAxis("Left Trigger") == 1)
		{
			isCommandWindowOpen[0] = true;
			isCommandWindowOpen[1] = false;
		}
		else if(Input.GetAxis("Right Trigger") == 1)
		{
			isCommandWindowOpen[0] = false;
			isCommandWindowOpen[1] = true;
		}
		else
		{
			isCommandWindowOpen[0] = false;
			isCommandWindowOpen[1] = false;
		}
	}

	void OnGUI()
	{
		if(isCommandWindowOpen[0])
		{
			GUI.Window(0, new Rect(0, Screen.height - (Screen.height / 2), 200, 200), CommandWindow1, "Unit Command Page 1");
		}
		else if(isCommandWindowOpen[1])
		{
			GUI.Window(0, new Rect(0, Screen.height - (Screen.height / 2), 200, 200), CommandWindow2, "Unit Command Page 2");
		}
	}

	void CommandWindow1(int windowID)
	{
		GUI.Label(new Rect(10, 25, 70, 30), "1");
		GUI.Label(new Rect(50, 25, 70, 30), "Attack");
		GUI.Label(new Rect(10, 75, 70, 30), "2");
		GUI.Label(new Rect(50, 75, 70, 30), "Collect");
		GUI.Label(new Rect(10, 125, 70, 30), "3");
		GUI.Label(new Rect(50, 125, 70, 30), "Stay");
		GUI.Label(new Rect(10, 175, 70, 30), "4");
		GUI.Label(new Rect(50, 175, 70, 30), "Unload");
	}

	void CommandWindow2(int windowID)
	{
		GUI.Label(new Rect(10, 25, 70, 30), "1");
		GUI.Label(new Rect(50, 25, 70, 30), "Placeholder");
		GUI.Label(new Rect(10, 75, 70, 30), "2");
		GUI.Label(new Rect(50, 75, 70, 30), "Placeholder");
		GUI.Label(new Rect(10, 125, 70, 30), "3");
		GUI.Label(new Rect(50, 125, 70, 30), "Placeholder");
		GUI.Label(new Rect(10, 175, 70, 30), "4");
		GUI.Label(new Rect(50, 175, 70, 30), "Placeholder");
	}
}