using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	AudioSource theSong;

	//Unity Events - Start

	void Start()
	{
		UpdateAudioSource();
	}
	
	void OnEnable()
	{
		GameController.OnPause += HandleOnPause;
	}
	
	void OnDisable()
	{
		GameController.OnPause -= HandleOnPause;
	}

	//Unity Events - End


	//Mutator / Accessor Methods - Start

	protected void PauseMusic()
	{
		UpdateAudioSource();
		if (theSong != null)
		{
			theSong.Pause();
		}
	}

	protected void PlayMusic()
	{
		UpdateAudioSource();
		if (theSong != null)
		{
			theSong.Play();
		}
	}

	//Mutator / Action Methods - End


	//Logic Methods - Start
	protected void HandleOnPause(bool flag) //Based directly on the version in UnitySpawn.cs - Moore.
	{
		//If the game is being paused, then pause the music. If the game is resumed, resume music.
		if (flag)
		{
			PauseMusic();
		}
		else
		{
			PlayMusic();
		}
	}

	//Logic Methods - End


	//Utility Methods - Start

	protected void UpdateAudioSource()
	{
		theSong = gameObject.GetComponent<AudioSource>();
	}

	//Utility Methods - End
}
