using UnityEngine;

public class OptionsPanelController : MonoBehaviour
{
    [Header("Prefab de Options")]
    public GameObject optionsPrefab; // Arrastra tu prefab "Options" en el Inspector

    // Variable para guardar la instancia ya creada del panel de opciones
    private GameObject optionsPanelInstance;

    /// <summary>
    /// Instancia o muestra el panel de opciones.
    /// </summary>
    public void OpenOptionsPanel()
    {
        Debug.Log("Intentando abrir el panel de opciones...");
        
        // Si aún no se ha instanciado el prefab, se instancia
        if (optionsPanelInstance == null)
        {
            // Busca un Canvas en la escena para ser padre del panel
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas != null)
            {
                // Instancia el prefab y lo establece como hijo del Canvas
                optionsPanelInstance = Instantiate(optionsPrefab, canvas.transform);
                Debug.Log("Prefab de opciones instanciado y agregado al Canvas.");
            }
            else
            {
                Debug.LogError("No se encontró un Canvas en la escena.");
                return;
            }
        }
        else
        {
            // Si ya existe, simplemente lo activamos
            optionsPanelInstance.SetActive(true);
            Debug.Log("Panel de opciones activado.");
        }
    }

    /// <summary>
    /// Cierra o desactiva el panel de opciones.
    /// </summary>
    public void CloseOptionsPanel()
    {
        if (optionsPanelInstance != null)
        {
            optionsPanelInstance.SetActive(false);
            Debug.Log("Panel de opciones desactivado.");
        }
    }
}
