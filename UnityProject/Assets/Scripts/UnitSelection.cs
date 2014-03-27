using UnityEngine;
using System.Collections;

public class UnitSelection : MonoBehaviour
{
	public int listSize = 10;
	public float sphereIncreaseRate;
	Vector3 maxScale, defaultScale;
	public GameObject[] selectedObjectList;
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
		maxScale = new Vector3(120f, 120f, 120f);
		defaultScale = transform.localScale;
		selectedObjectList = new GameObject[listSize];
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

			if(transform.localScale.x < maxScale.x)
			{
				float x = transform.localScale.x + sphereIncreaseRate * Time.deltaTime;
				float y = transform.localScale.y + sphereIncreaseRate * Time.deltaTime;
				float z = transform.localScale.z + sphereIncreaseRate * Time.deltaTime;
				transform.localScale = new Vector3(x, y, z);
			}

			// The selection process has now gone through one frame, so it's no longer "starting"
			isStartOfSelection = false;
		}
		else
		{
			transform.localScale = defaultScale;
			//selectionCollider.radius = defaultRadius;
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
