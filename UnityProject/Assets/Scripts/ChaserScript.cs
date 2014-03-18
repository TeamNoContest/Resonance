using UnityEngine;
using System.Collections;

public class ChaserScript : MonoBehaviour {

	GameObject target;
	Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		//Pick the cloest target with the appropriate tag and move towards it. - Moore
		target = FindClosestGameObjectWithTag("Target");

		if (target != null) 
		{
			//Make sure to check that the target is within a desirable distance. So there should be a maximum distance variable. - Moore
			transform.LookAt (target.transform.position);
			transform.position += (transform.forward * 0.1f);
		}

		//If too far from the start position, teleport back to start.
		if (Vector3.Distance(startPosition, transform.position) > 100) 
		{ 
			transform.position = startPosition;
		}
	}

	GameObject FindClosestGameObjectWithTag(string tagToFind)
	{
		GameObject result = null;
		GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tagToFind);
		
		foreach (GameObject current in allObjects)
		{
			if (current != this.gameObject)
			{
				if (result == null)
				{
					result = current;
				} 
				else
				{
					if (Vector3.Distance(transform.position, result.transform.position) > Vector3.Distance(transform.position, current.transform.position))
					{
						result = current;
					}
				}
			}
		}
		return result;
	}
}
