using UnityEngine;
using TMPro;

public class ShowMessage : MonoBehaviour
{
  
    public GameObject messagePanel; // Panel con el mensaje

    private void Start()
    {
        if (messagePanel != null)
            messagePanel.SetActive(false); // Ocultar mensaje al inicio
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica que el Player se acerque
        {
            messagePanel.SetActive(true); // Mostrar mensaje
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Cuando se aleja, ocultar mensaje
        {
            messagePanel.SetActive(false);
        }
    }

}
