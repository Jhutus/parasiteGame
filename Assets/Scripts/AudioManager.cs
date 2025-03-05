using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixer audioMixer;
    private float lastMusicVolume = 0f;
    private float lastSFXVolume = 0f;
    private bool isMuted = false;

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
            return;
        }

        LoadAudioSettings();
    }

    public void SetMusicVolume(float volume)
    {
        lastMusicVolume = Mathf.Log10(volume) * 20;
        PlayerPrefs.SetFloat("MusicVolume", lastMusicVolume);
        PlayerPrefs.SetFloat("MusicSlider", volume);
        if (!isMuted) audioMixer.SetFloat("MusicVolume", lastMusicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        lastSFXVolume = Mathf.Log10(volume) * 20;
        PlayerPrefs.SetFloat("SFXVolume", lastSFXVolume);
        PlayerPrefs.SetFloat("SFXSlider", volume);
        if (!isMuted) audioMixer.SetFloat("SFXVolume", lastSFXVolume);
    }

    public void SetMute(bool mute)
    {
        isMuted = mute;
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);

        if (mute)
        {
            audioMixer.SetFloat("MusicVolume", -80f);
            audioMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0f));
            audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0f));
        }
    }

    private void LoadAudioSettings()
    {
        lastMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        lastSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        audioMixer.SetFloat("MusicVolume", isMuted ? -80f : lastMusicVolume);
        audioMixer.SetFloat("SFXVolume", isMuted ? -80f : lastSFXVolume);
    }
}
