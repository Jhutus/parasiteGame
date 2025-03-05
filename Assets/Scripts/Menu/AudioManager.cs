using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Para hacer el AudioManager persistente

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioMixer audioMixer;

    public AudioClip mainMenuMusic;
    public AudioClip sampleSceneMusic;
    public AudioClip buttonClickSFX;

    private bool isMuted = false; // Estado de mute

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Permite que el AudioManager sobreviva al cambio de escenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        PlayMusicForScene();
        LoadMuteState(); // Cargar estado de mute guardado
    }

    public void PlayMusicForScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (sceneName == "MainMenu")
            musicSource.clip = mainMenuMusic;
        else if (sceneName == "SampleScene")
            musicSource.clip = sampleSceneMusic;

        musicSource.Play();
    }

    public void PlayButtonClick()
    {
        if (!isMuted)
            sfxSource.PlayOneShot(buttonClickSFX);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        float volume = isMuted ? -80f : 0f; // -80dB silencia el audio en AudioMixer

        audioMixer.SetFloat("MusicVolume", volume);
        audioMixer.SetFloat("SFXVolume", volume);

        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadMuteState()
    {
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        float volume = isMuted ? -80f : 0f;

        audioMixer.SetFloat("MusicVolume", volume);
        audioMixer.SetFloat("SFXVolume", volume);
    }
}
