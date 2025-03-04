using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad del jugador
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Captura la entrada del jugador
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Normaliza el vector de movimiento para evitar mayor velocidad en diagonales
        movement = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Aplica el movimiento al Rigidbody2D


        if (movement.magnitude > 0)
        {
            rb.linearVelocity = movement * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Detiene el movimiento inmediatamente
        }
    }

}
