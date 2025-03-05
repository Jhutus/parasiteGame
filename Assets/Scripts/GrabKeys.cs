using UnityEngine;

public class GrabKeys : MonoBehaviour
{
    [SerializeField] LayerMask grabbableLayer;
    [SerializeField] Transform grabPoint;
    
    public Vector3 Direction { get; set; }

    GameObject keyHolding;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (keyHolding)
            {
                keyHolding.transform.position = transform.position + Direction;
                keyHolding.transform.parent = null;
                if (keyHolding.GetComponent<Rigidbody2D>())
                    keyHolding.GetComponent<Rigidbody2D>().simulated = true;
                keyHolding = null;
            }
            else
            {
               Collider2D grabbableItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, grabbableLayer);
                if (grabbableItem)
                {
                    keyHolding = grabbableItem.gameObject;
                    keyHolding.transform.position = grabPoint.position;
                    keyHolding.transform.parent = transform;
                    if (keyHolding.GetComponent<Rigidbody2D>())
                        keyHolding.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
        }

    }
 
}
