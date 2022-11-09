using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Camera cam;
    [SerializeField] private float zOffset;
    [SerializeField] private Vector3 camRot;

    [SerializeField]private NavMeshAgent agent;
    [SerializeField] private Vector3 hitPoint;

    [SerializeField] private float yAngle;
    [SerializeField] private float ySpeed;

    private void Update()
    {
        ManageCamera();

        ManagePlayerMovement();
    }

    private void ManageCamera()
    {
        var camPos = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z - zOffset);

        cam.transform.position = camPos;

        // Set Y Angle when A / D keys are pressed
        if (Input.GetKey(KeyCode.D))
        {
            yAngle = yAngle + ySpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            yAngle = yAngle - ySpeed * Time.deltaTime;
        }

        cam.transform.rotation = Quaternion.Euler(camRot.x, yAngle, camRot.z);
    }

    private void ManagePlayerMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get ray from mouse pos
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit; // this is set by Physics.Raycast()

            if (Physics.Raycast(ray, out hit))
            {
                // Set hitPoint
                hitPoint = hit.point;
                // Move agent
                agent.SetDestination(hitPoint);
            }
        }
    }
}
