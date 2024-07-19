using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class SoundManager : MonoBehaviour 
	{
		public AudioSource efxSource;					
		public AudioSource musicSource;
		public AudioSource reserveEfxSourcee;
		public static SoundManager instance = null;		
		public float lowPitchRange = .95f;				
		public float highPitchRange = 1.05f;
	void Awake()
	{
		instance = this;
	}
    private void Update()
    {
        if (efxSource.isPlaying || reserveEfxSourcee.isPlaying)
		{ 
			musicSource.volume = 0.3f;
		}
		else
		{
            musicSource.volume = 1f;
        }
    }
    public void PlaySingle(AudioClip clip)
		{
			if (efxSource.isPlaying)
			{
				reserveEfxSourcee.clip = clip;
				reserveEfxSourcee.Play();
			}
            efxSource.clip = clip;
			efxSource.Play ();
		}
		public void RandomizeSfx(params AudioClip[] clips)
		{
			int randomIndex = Random.Range(0, clips.Length);
			float randomPitch = Random.Range(lowPitchRange, highPitchRange);
			efxSource.pitch = randomPitch;
			efxSource.clip = clips[randomIndex];
			efxSource.Play();
		}
	}

