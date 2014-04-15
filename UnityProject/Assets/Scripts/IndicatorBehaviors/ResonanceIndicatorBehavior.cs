using UnityEngine;
using System.Collections;

public class ResonanceIndicatorBehavior : MonoBehaviour 
{
	public Material matCyan;

	public void SetVisible()
	{
		renderer.enabled = true;
	}

	public void SetInvisible()
	{
		renderer.enabled = false;
	}
}
