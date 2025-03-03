using UnityEngine;

public class EnemyChange : MonoBehaviour
{
    public Sprite newSprite; // Asigna un sprite diferente en el Inspector

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer
    }

    public void ChangeSprite()
    {
        if (newSprite != null)
        {
            spriteRenderer.sprite = newSprite; // Cambia el sprite del enemigo
        }
    }
}
