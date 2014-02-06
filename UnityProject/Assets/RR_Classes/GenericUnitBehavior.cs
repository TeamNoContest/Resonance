using UnityEngine;
using System.Collections;

public class GenericUnitBehavior : MonoBehaviour
{

	//Property Instantiation Section
	public string AllyTag { get; set; }

	private float GatheringRate { get; set; }

	private float AttackRate { get; set; }

	private float RateModifer { get; set; }

	private float MovementSpeed { get; set; }

	private float ResourceCapacity { get; set; }

	private float ResourceLoad { get; set; }

	private float Integrity { get; set; } //This is kinda like a healthbar. This shouldn't be very important in our prototype's mode of play.;

	//Event Methods - Start

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	//Event Methods - End

	//Accessor Methods - Start

	//Accessor Methods - End

	//Mutator Methods - Start

	//Mutator Methods - End
}
