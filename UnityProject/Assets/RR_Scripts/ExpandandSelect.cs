using UnityEngine;
using System.Collections;

public class ExpandandSelect : MonoBehaviour {

	public int listSize = 10;
	float defaultRadius = 10.0f;
	float maxRadius = 30.0f;
	public GameObject[] selectedObjectList;
	SphereCollider selectionCollider;

	// Use this for initialization
	void Start () {
		selectedObjectList = new GameObject[listSize];
		selectionCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.T))
		{
			if(selectionCollider.radius < maxRadius)
			{
			selectionCollider.radius += 15f * Time.deltaTime;
			}
		}
		else
		{
			selectionCollider.radius = defaultRadius; 
		}
	}
}
