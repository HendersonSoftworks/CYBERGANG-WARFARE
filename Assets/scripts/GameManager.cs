using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerArmy playerArmy;
    public EnemyArmy enemyArmy;


    [SerializeField] private GameObject battlePanel;
    [SerializeField] private GameObject victoryPanel;

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
        // Get player & enemy troop stats
        // Perform calc to determine losses
        // Reflect losses in stats and UI
        // Check if one party was victorious
    }

    public void EndBattle()
    {
        Debug.Log("Battle over");
    }
}
