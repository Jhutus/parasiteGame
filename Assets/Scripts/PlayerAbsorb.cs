using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerAbsorb : MonoBehaviour
{
    private SpriteRenderer playerSprite; // Sprite actual del jugador
    private Sprite originalSprite; // Sprite inicial del jugador
    private bool canAbsorb = true; // Controla si puede absorber
    private List<Sprite> absorbedSprites = new List<Sprite>(); // Lista de sprites absorbidos

    public float absorbDuration = 10f; // Tiempo que mantiene el sprite absorbido
    public float hurryDuration = 5f; // Tiempo que queda de absorcion
    public float cooldownTime = 2f; // Tiempo antes de poder absorber de nuevo


    public GameObject greenCircle;  // Apto para absorber
    public GameObject blueCircle;   // En absorción
    public GameObject orangeCircle; // 5 segundos restantes
    public GameObject redCircle;    // Cooldown
    //UI
    public GameObject spriteHistoryPanel; // Panel donde se mostrarán los sprites absorbidos
    public GameObject spriteIconPrefab; // Prefab de un icono en la UI
    public TextMeshProUGUI cooldownText; // Texto de cooldown con TMP

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer del jugador
        originalSprite = playerSprite.sprite; // Guarda el sprite inicial
        cooldownText.text = "";
        UpdateStateIndicator("ready");
    }

    void Update()
    {
        // Detectar teclas para cambiar de sprite
        for (int i = 0; i < absorbedSprites.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Alpha1 = tecla '1', Alpha2 = tecla '2', etc.
            {
                ChangeSprite(i);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canAbsorb && other.CompareTag("Enemy")) // Verifica si puede absorber
        {
            SpriteRenderer enemySprite = other.GetComponent<SpriteRenderer>(); // Obtiene el sprite del enemigo
            EnemyChange enemy = other.GetComponent<EnemyChange>(); // Accede al script del enemigo

            if (enemySprite != null && enemy != null)
            {
                playerSprite.sprite = enemySprite.sprite; // El jugador toma la apariencia del enemigo
                absorbedSprites.Add(enemySprite.sprite); // Guarda el sprite absorbido en la lista

                enemy.ChangeSprite(); // Cambia el sprite del enemigo
                canAbsorb = false; // Desactiva la capacidad de absorber temporalmente
                enemy.tag = "Sick"; // Cambia el tag 

                // Actualiza la UI con los sprites absorbidos
                UpdateSpriteHistoryUI();

                // Inicia la cuenta regresiva para volver al sprite original y activar el cooldown
                StartCoroutine(ResetSpriteAfterTime());
            }
        }
    }

    IEnumerator ResetSpriteAfterTime()
    {
        UpdateStateIndicator("absorbing"); // 🔵 Estado azul
        yield return new WaitForSeconds(absorbDuration - hurryDuration);

        UpdateStateIndicator("warning"); // 🟠 Estado naranja (Tiempo restante)
        yield return new WaitForSeconds(hurryDuration);

        playerSprite.sprite = originalSprite; // Vuelve al sprite original
        UpdateStateIndicator("cooldown"); // 🔴 Estado rojo
        yield return new WaitForSeconds(cooldownTime); // Espera 2 segundos antes de poder absorber de nuevo

        canAbsorb = true; // Habilita la absorción nuevamente
        UpdateStateIndicator("ready"); // 🟢 Estado verde
    }

    public void ChangeSprite(int index)
    {
        if (index < absorbedSprites.Count && canAbsorb)
        {
            playerSprite.sprite = absorbedSprites[index];
            canAbsorb = false;

            // Actualiza la UI con los sprites absorbidos
            UpdateSpriteHistoryUI();

            StartCoroutine(ResetSpriteAfterTime());
        }
    }
    void UpdateSpriteHistoryUI()
    {
        // Limpia la UI antes de actualizar
        foreach (Transform child in spriteHistoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Crea nuevos iconos para cada sprite absorbido
        for (int i = 0; i < absorbedSprites.Count; i++)
        {
            // Instancia el prefab y asigna el sprite
            GameObject newIcon = Instantiate(spriteIconPrefab, spriteHistoryPanel.transform);
            Image iconImage = newIcon.GetComponent<Image>();
            iconImage.sprite = absorbedSprites[i];

            // Asigna el número de tecla
            TextMeshProUGUI textComponent = newIcon.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = (i + 1).ToString();
            }
        }
    }
    // 📌 **Método para actualizar los círculos de estado**
    void UpdateStateIndicator(string state)
    {
        greenCircle.SetActive(state == "ready");      // 🟢 Listo para absorber
        blueCircle.SetActive(state == "absorbing");   // 🔵 Absorbiendo
        orangeCircle.SetActive(state == "warning");   // 🟠 5 segundos restantes
        redCircle.SetActive(state == "cooldown");     // 🔴 Cooldown
    }
}