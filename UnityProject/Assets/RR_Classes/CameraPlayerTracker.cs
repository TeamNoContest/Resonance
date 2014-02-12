using UnityEngine;
using System.Collections;

public class CameraPlayerTracker : MonoBehaviour {

	//Public properties to be set in inspector.
	public GameObject thePlayer;
	public float maximumPlayerCameraDistance;
	public float cameraChaseSpeed;
	public float currentDistance; //This exists soley to make it easier to watch in the inspector. - Moore

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (thePlayer != null)
		{

			currentDistance = Vector3.Distance(transform.position, thePlayer.transform.position);
			
			if (currentDistance > maximumPlayerCameraDistance) 
			{
				//Vector3.MoveTowards(transform.position, new Vector3(thePlayer.transform.position.x, transform.position.y, thePlayer.transform.position.z ), cameraChaseSpeed); //Note, this doesn't change the elevation of the GameObject using this script. - Moore
				transform.position = Vector3.Lerp(transform.position, new Vector3(thePlayer.transform.position.x, transform.position.y, thePlayer.transform.position.z ), cameraChaseSpeed / 100);
			}
		}

		transform.LookAt(thePlayer.transform.position);
	
	}
}
