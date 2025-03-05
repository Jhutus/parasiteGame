using UnityEngine;

public class MusicSelect : MonoBehaviour
{
    private AudioSource music;
    public AudioClip menuClip;
    public AudioClip backgroundClip;

    void Start()
    {
        music = GetComponent<AudioSource>();
        music.loop = true;
        
        // Suscribirse al evento del GameManager
        GameManager.OnGameStateChanged += UpdateMusic;

        // Configurar música inicial
        UpdateMusic(GameManager.Instance.CurrentState);
    }

    private void OnDestroy()
    {
        // Desuscribirse al destruir el objeto para evitar errores
        GameManager.OnGameStateChanged -= UpdateMusic;
    }

    void UpdateMusic(GameManager.GameState newState)
    {
        AudioClip newClip = (newState == GameManager.GameState.Playing) ? backgroundClip : menuClip;

        if (music.clip != newClip)
        {
            music.clip = newClip;
            music.Play();
        }
    }
}