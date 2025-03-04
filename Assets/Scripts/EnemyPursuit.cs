
using UnityEngine;

public class EnemyPursuit : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    Transform objective;
    Rigidbody2D rb;
    Vector2 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        objective = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (objective)
        {
            Vector3 direction = (objective.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    void FixedUpdate()
    {
        if (objective)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

}

