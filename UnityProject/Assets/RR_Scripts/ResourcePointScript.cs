/// <summary>
/// ResourcePointScript
/// This script handles the actions of resource points, including its
/// interactions with collection units.
/// 
/// Here's the basic procedure:
/// 1.	Unit approaches resource node. When it is X units away, it stores
/// 	a reference to the appropriate resource point, then calls
/// 	AttachUnit on that point.
/// 2.	The resource point then creates an "attach line" starting at the
/// 	point and ending at the unit. The attach line is a LineRenderer.
/// 	I store all the attach lines coming from the point in a list.
/// 	(I needed to store the unit positions in another List, since
/// 	once the LineRenderer is created I can't access its position.)
/// 3.	Every second, each unit attached to a resource point will call
/// 	RequestResource on the appropriate resource point. If the point
/// 	has enough resources, it returns that amount. If it doesn't have
/// 	enough, it returns as much as it has left. Each unit calls the
/// 	method seperately, so the RequestResources calls will not happen
/// 	all at the same time; they will be staggered. That should reduce
/// 	the chance of a resource giving resources it doesn't have.
/// 4.	When the resource point is depleted, it destroys the attach line.
/// 	I still need to figure out a way to make the attached units know
/// 	that it has been destroyed (so the units don't keep trying to call
/// 	RequestResource).
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcePointScript : MonoBehaviour
{
	public GameObject attachLinePrefab;
	public List<LineRenderer> attachLinesList;	// Hold all the attach lines coming from the resource point.
	public List<Vector3> unitPositionsList;		// Hold the positions of all units attached to the resource point.
	public int resourceCurrent;

	void Update()
	{
		if(resourceCurrent <= 0)
		{
			Destroy(gameObject);
		}
	}

	LineRenderer AttachUnit(Vector3 unitPosition)
	{
		LineRenderer attachLine = (LineRenderer)Instantiate(attachLinePrefab, transform.position, Quaternion.identity);
		attachLine.SetPosition(0, transform.position);
		attachLine.SetPosition(1, unitPosition);
		attachLinesList.Add(attachLine);

		return attachLine;
	}

	void DetachUnit(LineRenderer attachLine)
	{
		attachLinesList.Remove(attachLine);
		Destroy(attachLine);

		// We need a way to let the attached units that the resource point has been destroyed.
	}

	// The unit will request an amount from the resource point.
	// The resource point will attempt to give the unit that amount
	// in the return statement.
	int RequestResource(int amount)
	{
		int tempInt = 0;

		if(amount >= resourceCurrent)
		{
			tempInt = amount;
			resourceCurrent -= amount;
		}
		else
		{
			tempInt = resourceCurrent;
			resourceCurrent = 0;
		}

		return tempInt;
	}

	void OnGUI()
	{
		Vector2 labelPos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(labelPos.x, Screen.height - labelPos.y - 30, 50, 20), resourceCurrent.ToString());
	}

	void OnDestroy()
	{
		foreach(LineRenderer lr in attachLinesList)
		{
			Destroy(lr);
		}
	}
}