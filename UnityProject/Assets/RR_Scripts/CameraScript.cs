// CameraScript.cs
//
// Q and E rotate camera. 9 and 0 zoom out and in. This will change when we use an Xbox controller:
// Holding the left shoulder button will put the player in "camera mode." While it's held, the
// player can rotate the camera left and right by moving the right analog stick left and right, and he can
// zoom out and in by moving the right analog stick down and up. Releasing the left shoulder button will
// make the player leave "camera mode."

using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public const int MIN_ZOOM_DISTANCE = 80;
	public const int MAX_ZOOM_DISTANCE = 200;

	Transform target;

	void Start()
	{
		target = transform.parent;
	}

	void Update()
	{
		transform.LookAt(target);

		if(Input.GetKey(KeyCode.Q) || Input.GetAxis("RightStick Horizontal") > 0)
		{
			transform.RotateAround(target.position, Vector3.up, 40 * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.E) || Input.GetAxis("RightStick Horizontal") < 0)
		{
			transform.RotateAround(target.position, -Vector3.up, 40 * Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.Alpha9) || Input.GetAxis("RightStick Vertical") > 0)
		{
			Debug.Log(Vector3.Distance(transform.position, target.position));

			if(Vector3.Distance(transform.position, target.position) <= MAX_ZOOM_DISTANCE)
			{
				transform.position += transform.forward * -1;
			}
		}
		else if(Input.GetKey(KeyCode.Alpha0) || Input.GetAxis("RightStick Vertical") < 0)
		{
			Debug.Log(Vector3.Distance(transform.position, target.position));

			if(Vector3.Distance(transform.position, target.position) >= MIN_ZOOM_DISTANCE)
			{
				transform.position += transform.forward;
			}
		}
	}
}