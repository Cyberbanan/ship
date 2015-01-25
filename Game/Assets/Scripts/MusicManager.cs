using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour {

	// Singleton
	private static MusicManager instance = null;
	public static MusicManager main
	{
		get { return instance; }
    }

	public List<AudioClip> tracks = new List<AudioClip>();

	private float timer = 0.0f;
	private float crossfadeDuration = 3.0f;

	private AudioSource source1;
	private AudioSource source2;

	private bool lastSource2 = false;

	private int currentTrack = 0;

	public enum FadeState
	{
		None,
		GoToLevel2,
		GoToLevel3
	}

	private FadeState state = FadeState.None;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;

			source1 = gameObject.AddComponent<AudioSource>();
			source2 = gameObject.AddComponent<AudioSource>();
			
			source1.loop = true;
			source1.volume = 1.0f;
			source2.loop = true;
			source2.volume = 0.0f;
            source1.clip = tracks[0];
            source1.Play();
		}
		else
        {
            Destroy(this);
        }
	}

	void Update()
	{
		switch(state)
		{
		case FadeState.GoToLevel2:
			timer += Time.deltaTime;

			if(timer < crossfadeDuration)
			{
				source2.volume = timer / crossfadeDuration;
				source1.volume = 1.0f - (timer / crossfadeDuration);
			}
			else
			{
				source2.volume = 1.0f;
				source1.volume = 0.0f;
				source1.Stop();
				timer = 0.0f;
				state = FadeState.None;
			}

			break;
		case FadeState.GoToLevel3:
			timer += Time.deltaTime;

			if(timer < crossfadeDuration)
			{
				source1.volume = timer / crossfadeDuration;
			}
	        else
	        {
	            source1.volume = 1.0f;
	            timer = 0.0f;
				state = FadeState.None;
	        }

			break;
		}
	}

	public void NextLevel()
	{
		currentTrack++;

		if(currentTrack == 1)
		{
			GoToLevel2();
		}
		else if(currentTrack == 2)
		{
			GoToLevel3();
		}
	}

	private void GoToLevel2()
	{
		timer = 0.0f;
		source2.clip = tracks[1];
		source2.volume = 0.0f;
		source2.Play();
		state = FadeState.GoToLevel2;
	}

	private void GoToLevel3()
	{
		timer = 0.0f;
		source1.clip = tracks[2];
		source1.volume = 0.0f;
		source1.Play();
		source1.timeSamples = source2.timeSamples;
		state = FadeState.GoToLevel3;
	}
}