using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    public GameObject gameOverPanel; // Asigna el panel en el Inspector
    public GameSceneManager gameSceneManager; // Asigna el GameSceneManager en el Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto con el que choca es el jugador
        {
            // Activa el panel de Game Over
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
            else
            {
                Debug.LogError("El panel de Game Over no está asignado en el Enemy.");
            }

            // Llamar a GameOver si el GameSceneManager está asignado
            if (gameSceneManager != null)
            {
                gameSceneManager.GameOver();
            }
            else
            {
                Debug.LogError("GameSceneManager no está asignado en el Enemy.");
            }
        }
    }
}