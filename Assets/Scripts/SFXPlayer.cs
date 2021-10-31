using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources = null;

    [SerializeField] private AudioClip[] birdSounds = null;
    [SerializeField] private float cooldownForBirds = 0.0f;
    [SerializeField] private float volumeForBirds = 0.0f;
    private float timeToNextBirdSound;

    private static SFXPlayer instance;
    private int currentAudioSourceIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Time.time > timeToNextBirdSound)
        {
            PlaySFX(birdSounds[Random.Range(0, birdSounds.Length)]);
            timeToNextBirdSound = Time.time + cooldownForBirds;
        }
    }

    public static void PlaySFX(AudioClip sfx)
    {
        instance.audioSources[ instance.currentAudioSourceIndex ].PlayOneShot(sfx);
        instance.currentAudioSourceIndex = (instance.currentAudioSourceIndex + 1) 
                                                    % instance.audioSources.Length;
    }
    public static void PlaySFX(AudioClip sfx, float volume)
    {
        instance.audioSources[instance.currentAudioSourceIndex].PlayOneShot(sfx, volume);
        instance.currentAudioSourceIndex = (instance.currentAudioSourceIndex + 1)
                                                    % instance.audioSources.Length;
    }
}