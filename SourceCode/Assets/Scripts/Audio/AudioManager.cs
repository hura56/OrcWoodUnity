using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip music;
    public AudioClip ambient;
    public AudioClip hit;
    public AudioClip can;
    public AudioClip eat;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        audioSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
