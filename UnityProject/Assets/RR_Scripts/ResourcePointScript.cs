using UnityEngine;
using System.Collections;

public class ResourcePointScript : MonoBehaviour
{
	int resourceCount;
	int resourcesLostPerSecond;
	int unitsAcquired;
	
	void Start()
	{
		resourceCount = 1000;
		resourcesLostPerSecond = 0;
		unitsAcquired = 0;
	}
	
	void Update()
	{
		if(resourceCount <= 0)
		{
			Destroy(gameObject);
		}
	}
	
	void OnEnable()
	{
		InvokeRepeating("SubtractResource", 0f, 1f);
		
		// Subscribe to Attach event with AquireUnit
		// Subscribe to Release event with ReleaseUnit
	}
	
	void OnDisable()
	{
		// Unsubscribe from all events
	}
	
	void AcquireUnit(int unitGatherRate)
	{
		unitsAcquired++;
		resourcesLostPerSecond += unitGatherRate;
	}
	
	void ReleaseUnit(int unitGatherRate)
	{
		unitsAcquired--;
		resourcesLostPerSecond -= unitGatherRate;
	}
	
	void SubtractResource()
	{
		resourceCount -= resourcesLostPerSecond;
	}
	
	void OnGUI()
	{
		Vector2 labelPos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(labelPos.x, Screen.height - labelPos.y - 30, 50, 20), resourceCount.ToString());
	}
}