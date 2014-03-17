using UnityEngine;
using System.Collections;

public class TestCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider thisCollider)//This is testing ontriggerEvents
	{

		Debug.Log("Been Hit~!" + thisCollider.tag.ToString());//420 AM im going to sleep...
		light.color = Color.red;
	}
}
