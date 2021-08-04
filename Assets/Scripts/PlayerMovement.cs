using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera Camera = null;
    private NavMeshAgent Agent;
    [SerializeField]
    private LayerMask LayerMask;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit Hit, float.MaxValue, LayerMask.value))
            {
                Agent.SetDestination(Hit.point);
            }
        }

        if (Vector3.Distance(Agent.transform.position, Agent.destination) < 0.01f)
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit Hit, float.MaxValue, LayerMask.value))
            {
                Agent.transform.LookAt(Hit.point);
            }
        }
    }
}
