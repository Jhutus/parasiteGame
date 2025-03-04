using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsControllerVA : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider SliderMusicVolumen;
    public Slider SliderFXVolumen;
    
    public GameObject mainPanel;
    public GameObject options;
    public GameObject exit;

    void Start()
    {
        // Cargar valores guardados o establecer valores predeterminados
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        // Asignar los valores a los sliders
        SliderMusicVolumen.value = musicVolume;
        SliderFXVolumen.value = sfxVolume;

        // Aplicar los valores al AudioMixer
        ApplyVolume(musicVolume, "MusicVolume");
        ApplyVolume(sfxVolume, "SFXVolume");

        // Escuchar cambios en los sliders
        SliderMusicVolumen.onValueChanged.AddListener(ChangeMusicVolume);
        SliderFXVolumen.onValueChanged.AddListener(ChangeSFXVolume);
    }

    public void ChangeMusicVolume(float volume)
    {
        ApplyVolume(volume, "MusicVolume");
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void ChangeSFXVolume(float volume)
    {
        ApplyVolume(volume, "SFXVolume");
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    private void ApplyVolume(float volume, string exposedParameter)
    {
        // Evitar error de volumen 0 con logaritmo
        float dB = (volume <= 0.0001f) ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat(exposedParameter, dB);
    }
}
