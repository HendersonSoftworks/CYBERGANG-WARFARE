using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerArmy playerArmy;
    public EnemyArmy enemyArmy;


    [SerializeField] private GameObject battlePanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Text victoryText;

    [SerializeField] private Text playerPunkCount;
    [SerializeField] private Text playerMercCount;
    [SerializeField] private Text playerHackerCount;
    [SerializeField] private Text playerCyborgCount;

    [SerializeField] private Text enemyPunkCount;
    [SerializeField] private Text enemyMercCount;
    [SerializeField] private Text enemyHackerCount;
    [SerializeField] private Text enemyCyborgCount;

    public float bounds;
    
    public enum Modes { first_person, third_person}
    public Modes currentMode;

    public bool isDead = false;
    public bool isBattling = false;

    public GameObject[] playerObjs;

    public GameObject[] patrolPoints;

    private void Awake()
    {
        //playerObjs[1].SetActive(false);
    }

    private void Start()
    {
        // Set the mode to first person on startup
        //currentMode = Modes.first_person;
    }

    private void Update()
    {
        if (isBattling)
        {
            battlePanel.SetActive(true);

        }
        else
        {
            battlePanel.SetActive(false);
        }

        if (victoryPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                victoryPanel.SetActive(false);
            }
        }

        ManagePerspective();
    }

    private void ManagePerspective()
    {
        if (currentMode == Modes.first_person)
        {
            playerObjs[0].SetActive(true);
            playerObjs[1].SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            playerObjs[0].SetActive(false);
            playerObjs[1].SetActive(true);

            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SwitchCurrentMode(Modes mode)
    {
        if (mode == Modes.first_person)
        {
            mode = Modes.third_person;
        }
        else
        {
            mode = Modes.first_person;
        }
    }

    public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0;
    }

    public void SetBattleUIStats()
    {
        // Set Player counts
        playerPunkCount.text = playerArmy.punks.ToString();
        playerMercCount.text = playerArmy.mercs.ToString();
        playerHackerCount.text = playerArmy.hackers.ToString();
        playerCyborgCount.text = playerArmy.cyborgs.ToString();

        // Set Enemy counts
        enemyPunkCount.text = enemyArmy.punks.ToString();
        enemyMercCount.text = enemyArmy.mercs.ToString();
        enemyHackerCount.text = enemyArmy.hackers.ToString();
        enemyCyborgCount.text = enemyArmy.cyborgs.ToString();

    }

    public void CalcNextBattleStep()
    {
        Debug.Log("CalcNextBattleStep");

        /* How should calculation work?
         * 
         * punks == 2 strength, must attack punks and mercs if present
         * Mercs == 4 strength, must attack punks and mercs if present
         * Hackers == 4 strength, if hacker is in party, can kill cyborgs and other hackers
         * Cyborgs == 8 strength, can kill everything
         * 
         * greater totalStrength == advatange on rolls
         * greater totalCount == advantage on rolls
        */

        // Perform calc to determine losses
        // calc player advantages
        bool playerHasStrengthAdvantage = false; // determines whether player has strength advantage
        bool playerHasNumbersAdvantage = false; // determines whether player has numbers advantage
        int playerTotalStrength = playerArmy.punks * 2 + playerArmy.mercs * 4 + playerArmy.hackers * 4 + playerArmy.cyborgs * 8;
        int enemyTotalStrength = enemyArmy.punks * 2 + enemyArmy.mercs * 4 + enemyArmy.hackers * 4 + enemyArmy.cyborgs * 8;
        if (playerTotalStrength > enemyTotalStrength){ playerHasStrengthAdvantage = true; }
        if (playerArmy.totalTroops > enemyArmy.totalTroops) { playerHasNumbersAdvantage = true; }

        // Enemy losses
        if (playerHasStrengthAdvantage && playerHasNumbersAdvantage)
        {
            enemyArmy.punks -= GenerateRandomTroopLoss(3); 
            enemyArmy.mercs -= GenerateRandomTroopLoss(3);
            enemyArmy.hackers -= GenerateRandomTroopLoss(3);
            if (playerArmy.hackers > 0 || playerArmy.cyborgs > 0)
            {
                enemyArmy.cyborgs -= GenerateRandomTroopLoss(3);
            }

            playerArmy.punks -= GenerateRandomTroopLoss(1);
            playerArmy.mercs -= GenerateRandomTroopLoss(1);
            playerArmy.hackers -= GenerateRandomTroopLoss(1);
            if (enemyArmy.hackers > 0 || enemyArmy.cyborgs > 0)
            {
                playerArmy.cyborgs -= GenerateRandomTroopLoss(1);
            }
                
        }
        else if (playerHasStrengthAdvantage || playerHasNumbersAdvantage)
        {
            enemyArmy.punks -= GenerateRandomTroopLoss(2);
            enemyArmy.mercs -= GenerateRandomTroopLoss(2);
            enemyArmy.hackers -= GenerateRandomTroopLoss(2);
            if (playerArmy.hackers > 0 || playerArmy.cyborgs > 0)
            {
                enemyArmy.cyborgs -= GenerateRandomTroopLoss(2);
            }

            playerArmy.punks -= GenerateRandomTroopLoss(2);
            playerArmy.mercs -= GenerateRandomTroopLoss(2);
            playerArmy.hackers -= GenerateRandomTroopLoss(2);
            if (enemyArmy.hackers > 0 || enemyArmy.cyborgs > 0)
            {
                playerArmy.cyborgs -= GenerateRandomTroopLoss(2);
            }
        }
        else
        {
            enemyArmy.punks -= GenerateRandomTroopLoss(1);
            enemyArmy.mercs -= GenerateRandomTroopLoss(1);
            enemyArmy.hackers -= GenerateRandomTroopLoss(1);
            if (playerArmy.hackers > 0 || playerArmy.cyborgs > 0)
            {
                enemyArmy.cyborgs -= GenerateRandomTroopLoss(1);
            }

            playerArmy.punks -= GenerateRandomTroopLoss(3);
            playerArmy.mercs -= GenerateRandomTroopLoss(3);
            playerArmy.hackers -= GenerateRandomTroopLoss(3);
            if (enemyArmy.hackers > 0 || enemyArmy.cyborgs > 0)
            {
                playerArmy.cyborgs -= GenerateRandomTroopLoss(3);
            }
        }

        // Reflect losses UI
        playerArmy.EnsureCorrectTroopNumbers();
        enemyArmy.EnsureCorrectTroopNumbers();
        SetBattleUIStats();

        // Check if one party was victorious
        if (playerArmy.totalTroops <= 0)
        {
            EndBattle();
            victoryText.text = "A pathetic display!";
            victoryPanel.SetActive(true);
        }
        else if (enemyArmy.totalTroops <= 0)
        {
            EndBattle();
            victoryText.text = "You are Victorious!!";
            victoryPanel.SetActive(true);
        }
    }

    public void EndBattle()
    {
        Debug.Log("Battle over");
        isBattling = false;
    }

    public int GenerateRandomTroopLoss(int max)
    {
        return Random.Range(0, max + 1);
    }
}
