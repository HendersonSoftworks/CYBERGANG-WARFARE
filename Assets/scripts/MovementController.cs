using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    [SerializeField]private Camera cam;

    [SerializeField]private NavMeshAgent agent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get ray from mouse pos
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit; // this is set by Physics.Raycast()
            if (Physics.Raycast(ray, out hit))
            {
                // Move agent
                agent.SetDestination(hit.point);
            } 
        }
    }
}
