using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleScreenHandler : MonoBehaviour
{
	public GUIText[] menuSelections;
	public GUITexture arrow;

	int currentIndex;
	GUIText currentSelection;
	bool isAxisInUse;

	void Start()
	{
		currentIndex = 0;
		currentSelection = menuSelections[currentIndex];
		isAxisInUse = false;
	}

	void Update()
	{
		if(Input.GetAxisRaw("Vertical") > 0)
		{
			if(!isAxisInUse)
			{
				try
				{
					currentSelection = menuSelections[--currentIndex];
				}
				catch(System.IndexOutOfRangeException e)	// If at the top and trying to go up
				{
					currentIndex = menuSelections.Length - 1;
					currentSelection = menuSelections[currentIndex];
				}
				isAxisInUse = true;
			}
		}
		else if(Input.GetAxisRaw("Vertical") < 0)
		{
			if(!isAxisInUse)
			{
				try
				{
					currentSelection = menuSelections[++currentIndex];
				}
				catch(System.IndexOutOfRangeException e)	// If at the bottom and trying to go down
				{
					currentIndex = 0;
					currentSelection = menuSelections[currentIndex];
				}
				isAxisInUse = true;
			}
		}
		else
		{
			isAxisInUse = false;
		}

		// Update arrow's position
		arrow.transform.position = new Vector3(arrow.transform.position.x, currentSelection.transform.position.y, arrow.transform.position.z);

		// Execute the menu item's function
		if(Input.GetButton("Fire1"))
		{
			switch(currentSelection.name)
			{
			case "Play Text":
				Application.LoadLevel(1);
				break;
			case "Quit Text":
				Application.Quit();
				break;
			default:
				break;
			}
		}
	}
}