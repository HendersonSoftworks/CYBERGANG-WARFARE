using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float timerDuration;
    [SerializeField] private float timer;

    [SerializeField] GameManager gameManager;

    [SerializeField] GameObject currentPatrolPoint;
    [SerializeField] NavMeshAgent navMeshAgent;

    public enum movementState { patrolling, chasing, fleeing};
    public movementState currentState = movementState.patrolling;

    [SerializeField] PlayerController playerController;

    [SerializeField] float chaseDist;
    [SerializeField] bool isPlayerDetected;

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
            case movementState.patrolling:
                // set patrol point
                ManagePatrolState();
                // move char to patrol point
                if (currentPatrolPoint != null) { MoveCharToCurrentPatrolPoint(currentPatrolPoint.transform.position); }
                // check if player is detected
                isPlayerDetected = PlayerIsDetected(playerController);
                if (isPlayerDetected == true) { currentState = movementState.chasing; }
                break;
            case movementState.chasing:
                isPlayerDetected = PlayerIsDetected(playerController);
                // chase player if detected, otherwise go back to patrolling
                if (isPlayerDetected) { navMeshAgent.SetDestination(playerController.transform.position); navMeshAgent.speed = 19; }
                else { currentState = movementState.patrolling; }
                break;
            case movementState.fleeing:
                // Maybe implement fleeing? Undecided. Currently enemies are stupid.
                break;
            default:
                Debug.LogError("DEFAULT CASE HIT IN ENEMYCONTROLLER: " + gameObject.name);
                break;
        }
    }

    private void ManagePatrolState()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timerDuration;
            // change to random patrol point
            currentPatrolPoint = ChangePatrolPoint(gameManager.patrolPoints);
        }
    }

    private void MoveCharToCurrentPatrolPoint(Vector3 patrolPointPos)
    {
        navMeshAgent.SetDestination(patrolPointPos);   
    }

    public bool PlayerIsDetected(PlayerController playerController)
    {
        float distFromPlayer = Vector3.Distance(transform.position, playerController.transform.position);

        if (distFromPlayer <= chaseDist)
        {
            return true; // player detected
        }
        else
        {
            return false; // not yet detected... 
        }
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
