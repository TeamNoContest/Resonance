using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	private bool cameraMode = false;
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			cameraMode = true;
		}
		else if(Input.GetKeyUp(KeyCode.C))
		{
			cameraMode = false;
		}

		if(!cameraMode)
		{
			if(Input.GetAxis("Vertical") < 0)
			{
				transform.position += transform.forward;
			}
			else if(Input.GetAxis("Vertical") > 0)
			{
				transform.position += transform.forward * -1;
			}

			if(Input.GetAxis("Horizontal") < 0)
			{
				transform.position += transform.right;
			}
			else if(Input.GetAxis("Horizontal") > 0)
			{
				transform.position += transform.right * -1;
			}
		}
	}
}