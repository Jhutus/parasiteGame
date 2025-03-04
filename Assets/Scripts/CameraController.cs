using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] float smoothSpeed;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;
        transform.position = targetPosition;
    }


}
