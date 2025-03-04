using UnityEngine;
using UnityEngine.AI;

public class EnemyEscape : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] Transform objective = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(agent == null)
        {
            if(!TryGetComponent(out agent))
            {
                Debug.LogWarning(name + "needs a nav mesh agent");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objective)
            MoveToTarget();
    }

    void MoveToTarget()
    {
        agent.SetDestination(objective.position);
        agent.isStopped = false;
    }
}
