using UnityEngine;
using UnityEngine.AI;

public class PlayerArmy : ArmyManager
{
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource musicManager;

    // Update is called once per frame
    void Update()
    {
        EnsureCorrectTroopNumbers();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);

        if (other.gameObject.tag == "enemy")
        {
            // Enter Battle Mode
            EnterBattleMode(other.gameObject);
        }
    }

    protected override void EnterBattleMode(GameObject enemyObject)
    {
        // Clears path of Player agent to stop movement and sound
        gameObject.GetComponent<NavMeshAgent>().ResetPath();

        // Pause the game
        gameManager.PauseGame();

        // Send Enemy to GameManager
        gameManager.enemyArmy = enemyObject.GetComponent<EnemyArmy>();

        // Set Initial Stats
        gameManager.SetBattleUIStats();

        // Logic to open up Battle UI and simulate battle
        gameManager.isBattling = true;
    }
}
