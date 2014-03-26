using UnityEngine;
using System.Collections;

public class SuperclassTest : MonoBehaviour
{
	protected internal int number;
	
	protected void Start()
	{
		number = 0;
	}
	
	protected void Update()
	{
		print("Superclass: " + number++);
	}
}