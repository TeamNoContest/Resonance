using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	public GameObject gameController;

	private Vector3 camForward, camRight;
	private Vector3 lastPosition, currentPosition, newPosition;
	private float vertMove, horizMove;

	void Update()
	{
		GameController gcScript = gameController.GetComponent<GameController>();

		lastPosition = transform.position;

		camForward = Camera.main.transform.forward;
		camRight = Camera.main.transform.right;

		vertMove = Input.GetAxis("Vertical");
		horizMove = Input.GetAxis("Horizontal");

		if(gcScript.runState != RunState.PAUSED)
		{
			if(vertMove < 0)	// Back
			{
				camForward = new Vector3(camForward.x, 0, camForward.z);	// Ignore the y value
				//transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, camForward, 10 * Time.deltaTime, 0f));
				transform.position += camForward * -1;
			}
			else if(vertMove > 0)	// Forward
			{
				camForward = new Vector3(camForward.x, 0, camForward.z);
				//transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, -camForward, 10 * Time.deltaTime, 0f));
				transform.position += camForward;
			}

			if(horizMove < 0)
			{
				camRight = new Vector3(camRight.x, 0, camRight.z);
				//transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, camRight, 10 * Time.deltaTime, 0f));
				transform.position += camRight * -1;
			}
			else if(horizMove > 0)
			{
				camRight = new Vector3(camRight.x, 0, camRight.z);
				//transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, -camRight, 10 * Time.deltaTime, 0f));
				transform.position += camRight;
			}

			newPosition = transform.position;
		}
	}

	void LateUpdate()
	{
		Vector3 direction = lastPosition - newPosition;
		if(direction != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(direction);
		}
	}
}