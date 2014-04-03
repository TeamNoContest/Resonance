using UnityEngine;
using System.Collections;

public enum Command { NULL, ATTACK, COLLECT, STAY, UNLOAD }

public class CommandHandler : MonoBehaviour
{
	const int NUMBER_OF_PAGES = 3;

	bool[] isCommandWindowOpen;
	public bool isFirstFrameOfSelection;
	Rect windowRect;
	bool disablePageTurn;	// Disable page turning after the first frame the button is held.
	int currentPage;
	Command command;

	public delegate void CommandEventHandler(Command command);
	public static event CommandEventHandler OnCommand;

	void Start()
	{
		isCommandWindowOpen = new bool[NUMBER_OF_PAGES];
		for(int i = 0; i < isCommandWindowOpen.Length; i++)
		{
			isCommandWindowOpen[i] = false;
		}

		isFirstFrameOfSelection = true;
		windowRect = new Rect(0, Screen.height - (1f/3f * (float)Screen.height), 1f/6f * (float)Screen.width, 1f/3f * (float)Screen.height);
		currentPage = 0;
	}

	void Update()
	{
		#region Detect Input for Windows
		if(Input.GetAxis("Left Trigger") == 1)
		{
			isCommandWindowOpen[currentPage] = true;

			if(Input.GetAxis("Digital Move Horizontal") == -1)
			{
				if(disablePageTurn == false)
				{
					try
					{
						OpenPage(--currentPage);
					}
					catch(System.IndexOutOfRangeException)	// If an exception was caught, then currentPage is the first element.
					{
						currentPage = isCommandWindowOpen.Length - 1;
						OpenPage(currentPage);
					}
					disablePageTurn = true;
				}
			}
			else if(Input.GetAxis("Digital Move Horizontal") == 1)
			{
				if(disablePageTurn == false)
				{
					try
					{
						OpenPage(++currentPage);
					}
					catch(System.IndexOutOfRangeException)	// If an exception was caught, then currentPage is the last element.
					{
						currentPage = 0;
						OpenPage(currentPage);
					}
					disablePageTurn = true;
				}
			}
			else
			{
				disablePageTurn = false;
			}
		}
		else
		{
			isFirstFrameOfSelection = true;
			isCommandWindowOpen[currentPage] = false;
			currentPage = 0;
			disablePageTurn = false;
		}
		#endregion

		#region Detect Input for Command
		if(isCommandWindowOpen[0])
		{
			if(Input.GetButtonDown("Fire1"))
			{
				command = Command.ATTACK;
			}
			else if(Input.GetButtonDown("Fire2"))
			{
				command = Command.COLLECT;
			}
			else if(Input.GetButtonDown("Fire3"))
			{
				command = Command.STAY;
			}
			else if(Input.GetButtonDown("Fire4"))
			{
				command = Command.UNLOAD;
			}
		}

		if(command != Command.NULL && OnCommand != null)
		{
			OnCommand(command);
			command = Command.NULL;
		}
		#endregion
	}

	void OpenPage(int pageNumber)
	{
		// Close all pages, then open the page requested.
		for(int i = 0; i < isCommandWindowOpen.Length; i++)
		{
			isCommandWindowOpen[i] = false;
		}
		isCommandWindowOpen[pageNumber] = true;
	}

	void OnGUI()
	{
		if(isCommandWindowOpen[0])
		{
			GUI.Window(0, windowRect, CommandWindow1, "Unit Command Page 1");
		}
		else if(isCommandWindowOpen[1])
		{
			GUI.Window(1, windowRect, CommandWindow2, "Unit Command Page 2");
		}
		else if(isCommandWindowOpen[2])
		{
			GUI.Window(2, windowRect, CommandWindow3, "Unit Command Page 3");
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

	void CommandWindow3(int windowID)
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