using UnityEngine;

public class EnemyFlee : MonoBehaviour
{
    [SerializeField] Transform player;  // Referencia al jugador
    public float fleeSpeed = 3f;  // Velocidad de huida
    public float detectionRadius = 5f;  // Radio de detecci�n

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        // Calcular la distancia al jugador
        float distance = Vector2.Distance(transform.position, player.position);

        // Si el jugador est� dentro del radio de detecci�n, huir
        if (distance < detectionRadius)
        {
            Vector2 fleeDirection = (transform.position - player.position).normalized;
            rb.linearVelocity = fleeDirection * fleeSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;  // Se detiene si est� fuera del rango
        }
    }
}
