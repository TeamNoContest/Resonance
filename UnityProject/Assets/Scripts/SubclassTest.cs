using UnityEngine;
using System.Collections;

public class SubclassTest : SuperclassTest
{
	void Update()
	{
		base.Update();
		print("Subclass: " + base.number/2);
	}
}