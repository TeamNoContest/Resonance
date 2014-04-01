using UnityEngine;
using System.Collections;

public class SubclassTest : SuperclassTest
{
	new void Update()
	{
		base.Update();
		print("Subclass: " + base.number/2);
	}
}