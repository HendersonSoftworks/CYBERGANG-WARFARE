using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float timerDuration;
    [SerializeField] private float timer;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject currentPatrolPoint;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] PlayerController playerController;

    [SerializeField] float chaseDist;
    [SerializeField] float chaseSpeed;
    [SerializeField] float patrolSpeed;
    [SerializeField] bool isPlayerDetected;

    public enum MovementState { patrolling, chasing, fleeing };
    public MovementState currentState = MovementState.patrolling;

    private void Start()
    {
        timer = timerDuration;

        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (gameManager.isDead)
        {
            return;
        }

        switch (currentState)
        {
            case MovementState.patrolling:
                ManagePatrolState();

                break;
            case MovementState.chasing:
                ManageChaseState();

                break;
            case MovementState.fleeing:
                // Maybe implement fleeing? Undecided. Currently enemies are stupid.
                break;
            default:
                Debug.LogError("DEFAULT CASE HIT IN ENEMYCONTROLLER: " + gameObject.name);
                break;
        }
    }

    private void ManagePatrolState()
    {
        navMeshAgent.speed = patrolSpeed;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timerDuration;
            // change to random patrol point
            currentPatrolPoint = ChangePatrolPoint(gameManager.patrolPoints);
        }

        // move char to patrol point
        if (currentPatrolPoint != null) { MoveCharToCurrentPatrolPoint(currentPatrolPoint.transform.position); }
        
        // check if player is detected
        isPlayerDetected = PlayerIsDetected(playerController);
        if (isPlayerDetected == true) 
        { 
            currentState = MovementState.chasing; 
        }
    }

    private void ManageChaseState()
    {
        isPlayerDetected = PlayerIsDetected(playerController);
        // chase player if detected, otherwise go back to patrolling
        if (isPlayerDetected) 
        { 
            navMeshAgent.SetDestination(playerController.transform.position); 
            navMeshAgent.speed = chaseSpeed; 
        }
        else 
        { 
            currentState = MovementState.patrolling; 
        }
    }

    private void MoveCharToCurrentPatrolPoint(Vector3 patrolPointPos)
    {
        navMeshAgent.SetDestination(patrolPointPos);   
    }

    public bool PlayerIsDetected(PlayerController playerController)
    {
        float distFromPlayer;
        if (gameManager.currentMode != GameManager.Modes.first_person)
        {
            distFromPlayer = Vector3.Distance(transform.position, playerController.transform.position);

            if (distFromPlayer <= chaseDist)
            {
                return true; // player detected
            }
        }

        return false;
    }

    private GameObject ChangePatrolPoint(GameObject[] patrolPoints)
    {
        int rand = Random.Range(0, patrolPoints.Length);
        GameObject patrolPoint = patrolPoints[rand];

        return patrolPoint;
    }

    private void OnDrawGizmos()
    {
        // show distance to chase
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDist);
    }
}
