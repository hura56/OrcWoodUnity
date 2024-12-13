using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public static VolumeManager Instance { get; private set; }

    private float currentMusicVolume;
    private float currentSFXVolume;

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

    private void OnEnable()
    {
        LoadVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        currentMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        currentSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (musicSlider != null)
            musicSlider.value = currentMusicVolume;

        if (sfxSlider != null)
            sfxSlider.value = currentSFXVolume;
    }

    private void Start()
    {
        currentMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        currentSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (musicSlider != null)
            musicSlider.value = currentMusicVolume;

        if (sfxSlider != null)
            sfxSlider.value = currentSFXVolume;
    }

    private void Update()
    {
        if (musicSlider != null && Mathf.Abs(musicSlider.value - currentMusicVolume) > Mathf.Epsilon)
        {
            currentMusicVolume = musicSlider.value;
            PlayerPrefs.SetFloat("MusicVolume", currentMusicVolume);
            PlayerPrefs.Save();

            AudioManager.Instance.SetMusicVolume(currentMusicVolume);
        }

        if (sfxSlider != null && Mathf.Abs(sfxSlider.value - currentSFXVolume) > Mathf.Epsilon)
        {
            currentSFXVolume = sfxSlider.value;
            PlayerPrefs.SetFloat("SFXVolume", currentSFXVolume);
            PlayerPrefs.Save();

            AudioManager.Instance.SetSFXVolume(currentSFXVolume);
        }
    }

    public float GetMusicVolume()
    {
        return currentMusicVolume;
    }

    public float GetSFXVolume()
    {
        return currentSFXVolume;
    }

    public void SetSliders(Slider music, Slider sfx)
    {
        musicSlider = music;
        sfxSlider = sfx;

        if (musicSlider != null)
            musicSlider.value = currentMusicVolume;

        if (sfxSlider != null)
            sfxSlider.value = currentSFXVolume;
    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    LoadVolumeSettings();
    //}
}
