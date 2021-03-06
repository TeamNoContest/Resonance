﻿using UnityEngine;
using System.Collections.Generic;

public class UnitSelection : MonoBehaviour
{
	public int listSize = 10;
	public GameObject selectionSphere;
	public float sphereIncreaseRate;
	Vector3 maxScale, defaultScale;
	//public GameObject[] selectedObjectList; - Tweaked this, because working with arrays can be a pain. Using a generic list instead. - Moore
    public List<GameObject> selectedObjectList;
	bool isStartOfSelection;

	public AudioClip[] clipsCommands;
	AudioSource[] audioCommands;

	public delegate void SelectionEventHandler();
	public static event SelectionEventHandler OnDeselect;

	// If there are listeners, remove them.
	public static void RemoveAllListeners()
	{
		if(OnDeselect != null)
		{
			OnDeselect();
		}
	}

	void Awake()
	{
		audioCommands = new AudioSource[clipsCommands.Length];

		// Fill the audioCommands AudioSources with clipCommands clips
		for(int i = 0; i < audioCommands.Length; i++)
		{
			audioCommands[i] = AddAudio(clipsCommands[i]);
		}
	}

	void Start()
	{
		maxScale = new Vector3(120f, 120f, 120f);
		defaultScale = selectionSphere.transform.localScale;
		selectedObjectList = new List<GameObject>(listSize);
	}

	void Update()
	{
		if(Input.GetButton("Select Sphere"))
		{
			selectionSphere.SetActive(true);
			// If it's the start of the selection process, play an audio
			if(isStartOfSelection)
			{
				if(OnDeselect != null)	// If there are subscribers, do something. Else, do nothing.
				{
					OnDeselect();
				}
				audioCommands[Random.Range(0, audioCommands.Length)].Play();
                ClearList();
			}

			if(selectionSphere.transform.localScale.x < maxScale.x)
			{
				float x = selectionSphere.transform.localScale.x + sphereIncreaseRate * Time.deltaTime;
				float y = selectionSphere.transform.localScale.y + sphereIncreaseRate * Time.deltaTime;
				float z = selectionSphere.transform.localScale.z + sphereIncreaseRate * Time.deltaTime;
				selectionSphere.transform.localScale = new Vector3(x, y, z);
			}

			// The selection process has now gone through one frame, so it's no longer "starting"
			isStartOfSelection = false;
		}
		else
		{
			selectionSphere.transform.localScale = defaultScale;
			//selectionCollider.radius = defaultRadius;

			// The selection process is over, so reset the "start of selection"
			isStartOfSelection = true;

			selectionSphere.SetActive(false);
		}
	}

	AudioSource AddAudio(AudioClip clip)
	{
		AudioSource tempAudio = gameObject.AddComponent<AudioSource>();
		tempAudio.clip = clip;
		return tempAudio;
	}

    //The following methods manipulate the contents of this object. Mutators, more or less. - Moore
    public void AddObjectToList( GameObject theObject)
    {
        if (selectedObjectList.Count < selectedObjectList.Capacity && !selectedObjectList.Contains(theObject))
            selectedObjectList.Add(theObject);
    }

    public void ClearList()
    {
        selectedObjectList.Clear();
    }
}