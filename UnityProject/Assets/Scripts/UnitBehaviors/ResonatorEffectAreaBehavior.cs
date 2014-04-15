using UnityEngine;
using System.Collections;

public class ResonatorEffectAreaBehavior : MonoBehaviour 
{

	//  - Moore
	public delegate void ResonanceEventHandler(float amount);
	public event ResonanceEventHandler OnResonanceChange; // This is not static. The Resonators must each keep track of their own. - Moore.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//When this method is called, it triggers the Resonance event, sending out the new RateModifer out to every listening unit. 1 = 100, 2 = 200% 0.2 = 20%. - Moore
	public void SetRateModifer (float newRate)
	{
		if (OnResonanceChange != null)
		{
			print ("Sending out Resonance Event. Number of Listeners: " + OnResonanceChange.GetInvocationList().Length);
			OnResonanceChange(newRate);
		}
	}

	public int GetNumberOfResonatingUnits()
	{
		int result = 0;
		if (OnResonanceChange != null)
		{
			result = OnResonanceChange.GetInvocationList().Length;
		}
		return result;
	}
}
