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
        // Determinar qué música debe sonar según la escena actual
        PlayMusicForScene();
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
}

