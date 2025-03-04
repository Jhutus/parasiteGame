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

    private float absorbDuration = 5f; // Tiempo que mantiene el sprite absorbido
    private float cooldownTime = 2f; // Tiempo antes de poder absorber de nuevo

    //UI
    public GameObject spriteHistoryPanel; // Panel donde se mostrarán los sprites absorbidos
    public GameObject spriteIconPrefab; // Prefab de un icono en la UI
    public TextMeshProUGUI cooldownText; // Texto de cooldown con TMP

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer del jugador
        originalSprite = playerSprite.sprite; // Guarda el sprite inicial
        cooldownText.text = "";
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

                // Actualiza la UI con los sprites absorbidos
                UpdateSpriteHistoryUI();

                // Inicia la cuenta regresiva para volver al sprite original y activar el cooldown
                StartCoroutine(ResetSpriteAfterTime());
            }
        }
    }

    IEnumerator ResetSpriteAfterTime()
    {
        cooldownText.text = "Absorción activa";
        yield return new WaitForSeconds(absorbDuration); // Espera 10 segundos
        playerSprite.sprite = originalSprite; // Vuelve al sprite original
        cooldownText.text = "Cooldown...";
        yield return new WaitForSeconds(cooldownTime); // Espera 5 segundos antes de poder absorber de nuevo
        cooldownText.text = "";
        canAbsorb = true; // Habilita la absorción nuevamente
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
}