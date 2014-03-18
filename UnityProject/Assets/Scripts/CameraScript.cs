/// <summary>
/// CameraScript.cs
/// 
/// Keyboard controls:
/// 	Rotate: Q and E
/// 	Zoom: 9 and 0
/// Xbox controls:
/// 	Rotate: Right Stick vertical
/// 	Zoom: Right Stick horizontal
/// </summary>

using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public const int MIN_ZOOM_DISTANCE = 80;
	public const int MAX_ZOOM_DISTANCE = 200;
	public float distanceFromPlayer;

	Transform target;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		distanceFromPlayer = 120;
	}

	void Update()
	{
		transform.position = target.position - transform.forward * distanceFromPlayer;

		#region Rotation
		if(Input.GetAxis("Camera Horizontal") > 0)
		{
			transform.RotateAround(target.position, Vector3.up, 40 * Time.deltaTime);
		}
		else if(Input.GetAxis("Camera Horizontal") < 0)
		{
			transform.RotateAround(target.position, -Vector3.up, 40 * Time.deltaTime);
		}
		#endregion

		#region Zoom
		if(Input.GetAxis("Camera Vertical") > 0)
		{
			if(Vector3.Distance(transform.position, target.position) <= MAX_ZOOM_DISTANCE)
			{
				//transform.position += transform.forward * -1;
				distanceFromPlayer++;
			}
		}
		else if(Input.GetAxis("Camera Vertical") < 0)
		{
			if(Vector3.Distance(transform.position, target.position) >= MIN_ZOOM_DISTANCE)
			{
				//transform.position += transform.forward;
				distanceFromPlayer--;
			}
		}
		#endregion

		transform.position = target.position - transform.forward * distanceFromPlayer;
	}
}