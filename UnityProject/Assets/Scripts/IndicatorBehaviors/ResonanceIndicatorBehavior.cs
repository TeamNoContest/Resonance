using UnityEngine;
using System.Collections;

public class ResonanceIndicatorBehavior : MonoBehaviour 
{
	public Material matCyan;

	public void SetRIVisible()
	{
		renderer.enabled = true;
	}

	public void SetRIInvisible()
	{
		renderer.enabled = false;
	}
}
