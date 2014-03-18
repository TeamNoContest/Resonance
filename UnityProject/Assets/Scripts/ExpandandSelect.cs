using UnityEngine;
using System.Collections;

public class ExpandandSelect : MonoBehaviour
{
	public int listSize = 10;
	float defaultRadius = 10.0f;
	float maxRadius = 30.0f;
	public GameObject[] selectedObjectList;
	SphereCollider selectionCollider;
	bool isStartOfSelection;

	public AudioClip[] clipsCommands;
	AudioSource[] audioCommands;

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
		selectedObjectList = new GameObject[listSize];
		selectionCollider = GetComponent<SphereCollider>();
	}

	void Update()
	{
		if(Input.GetButton("Select Sphere"))
		{
			// If it's the start of the selection process, play an audio
			if(isStartOfSelection)
			{
				audioCommands[Random.Range(0, audioCommands.Length)].Play();
			}

			if(selectionCollider.radius < maxRadius)
			{
				selectionCollider.radius += 15f * Time.deltaTime;
			}

			// The selection process has now gone through one frame, so it's no longer "starting"
			isStartOfSelection = false;
		}
		else
		{
			selectionCollider.radius = defaultRadius;
			// The selection process is over, so reset the "start of selection"
			isStartOfSelection = true;
		}
	}

	AudioSource AddAudio(AudioClip clip)
	{
		AudioSource tempAudio = gameObject.AddComponent<AudioSource>();
		tempAudio.clip = clip;
		return tempAudio;
	}
}
