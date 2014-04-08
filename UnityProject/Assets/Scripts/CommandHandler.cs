using UnityEngine;
using System.Collections;

public enum Command { NULL, ATTACK, COLLECT, STAY, UNLOAD }

public class CommandHandler : GUIScript
{
	const int NUMBER_OF_PAGES = 1;

	bool[] isCommandWindowOpen;
	//bool isFirstFrameOfSelection;
	Rect popupWindowRect;
	bool disablePageTurn;	// Disable page turning after the first frame the button is held
	int currentPage;
	Command command;

	public delegate void CommandEventHandler(Command command);
	public static event CommandEventHandler OnCommand;

	new void Start()
	{
		base.Start();

		isCommandWindowOpen = new bool[NUMBER_OF_PAGES];
		for(int i = 0; i < isCommandWindowOpen.Length; i++)
		{
			isCommandWindowOpen[i] = false;
		}

		//isFirstFrameOfSelection = true;
		popupWindowRect = new Rect(0, Screen.height - popupWindowHeight, popupWindowWidth, popupWindowHeight);


		currentPage = 0;
	}

	void Update()
	{
		#region Detect Input for Windows
		if(Input.GetAxis("Command Menu") == 1 || Input.GetButton("Command Menu"))
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
			//isFirstFrameOfSelection = true;
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
		for(int i = 0; i < NUMBER_OF_PAGES; i++)
		{
			if(isCommandWindowOpen[i])
			{
				GUI.Window(i, popupWindowRect, CommandWindow, "Unit Command Page " + (i+1));
			}
		}
	}

	// To create another command page, just replace the "Placeholder" string with the name of the command.
	// Be sure to copy and paste the placeholder case so you always have the placeholder code!
	// Don't worry, it won't be displayed unless you set the NUMBER_OF_PAGES const to be equal to the
	// number of pages you want.
	void CommandWindow(int windowID)
	{
		switch(windowID)
		{
		case 0:
			GUI.Label(new Rect(popupWindowWidth * (1f/4f), popupWindowHeight * (1f/5f), 70, 30), "A");
			GUI.Label(new Rect(popupWindowWidth * (1f/2f), popupWindowHeight * (1f/5f), 70, 30), "Attack");
			GUI.Label(new Rect(popupWindowWidth * (1f/4f), popupWindowHeight * (2f/5f), 70, 30), "B");
			GUI.Label(new Rect(popupWindowWidth * (1f/2f), popupWindowHeight * (2f/5f), 70, 30), "Collect");
			GUI.Label(new Rect(popupWindowWidth * (1f/4f), popupWindowHeight * (3f/5f), 70, 30), "X");
			GUI.Label(new Rect(popupWindowWidth * (1f/2f), popupWindowHeight * (3f/5f), 70, 30), "Stay");
			GUI.Label(new Rect(popupWindowWidth * (1f/4f), popupWindowHeight * (4f/5f), 70, 30), "Y");
			GUI.Label(new Rect(popupWindowWidth * (1f/2f), popupWindowHeight * (4f/5f), 70, 30), "Unload");
			break;
		case 1:
			GUI.Label(new Rect(popupWindowWidth * (1f/4f), popupWindowHeight * (1f/5f), 70, 30), "A");
			GUI.Label(new Rect(popupWindowWidth * (1f/2f), popupWindowHeight * (1f/5f), 70, 30), "Placeholder");
			GUI.Label(new Rect(popupWindowWidth * (1f/4f), popupWindowHeight * (2f/5f), 70, 30), "B");
			GUI.Label(new Rect(popupWindowWidth * (1f/2f), popupWindowHeight * (2f/5f), 70, 30), "Placeholder");
			GUI.Label(new Rect(popupWindowWidth * (1f/4f), popupWindowHeight * (3f/5f), 70, 30), "X");
			GUI.Label(new Rect(popupWindowWidth * (1f/2f), popupWindowHeight * (3f/5f), 70, 30), "Placeholder");
			GUI.Label(new Rect(popupWindowWidth * (1f/4f), popupWindowHeight * (4f/5f), 70, 30), "Y");
			GUI.Label(new Rect(popupWindowWidth * (1f/2f), popupWindowHeight * (4f/5f), 70, 30), "Placeholder");
			break;
		default:
			break;
		}
	}
}