using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	private Vector3 camForward, camRight;
	private Vector3 lastPosition, currentPosition, newPosition, direction;
	private float vertMove, horizMove;
	private bool isPaused;

	void Start()
	{
		isPaused = false;
	}

	void OnEnable()
	{
		GameController.OnPause += HandleOnPause;
	}

	void OnDisable()
	{
		GameController.OnPause -= HandleOnPause;
	}

	void Update()
	{
		lastPosition = transform.position;

		camForward = Camera.main.transform.forward;
		camRight = Camera.main.transform.right;

		vertMove = Input.GetAxis("Vertical");
		horizMove = Input.GetAxis("Horizontal");

		if(!isPaused)
		{
			camForward = new Vector3(camForward.x, 0, camForward.z);	// Ignore the y value
			camRight = new Vector3(camRight.x, 0, camRight.z);			// Ignore the y value

			if(vertMove < 0)	// Back
			{
				transform.position += camForward * -1;
			}
			else if(vertMove > 0)	// Forward
			{
				transform.position += camForward;
			}

			if(horizMove < 0)
			{
				transform.position += camRight * -1;
			}
			else if(horizMove > 0)
			{
				transform.position += camRight;
			}

			newPosition = transform.position;
		}
	}

	void LateUpdate()
	{
		direction = lastPosition - newPosition;
		if(direction != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(direction);
		}
	}

	void HandleOnPause(bool flag)
	{
		isPaused = flag;
	}
}