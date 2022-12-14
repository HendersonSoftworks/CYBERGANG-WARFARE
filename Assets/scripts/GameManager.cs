using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject battlePanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject enemyOverlayPanel;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Text victoryText;
    [SerializeField] private Text enemyOverlayText;

    [SerializeField] private Text playerPunkCount;
    [SerializeField] private Text playerMercCount;
    [SerializeField] private Text playerHackerCount;
    [SerializeField] private Text playerCyborgCount;

    [SerializeField] private Text enemyPunkCount;
    [SerializeField] private Text enemyMercCount;
    [SerializeField] private Text enemyHackerCount;
    [SerializeField] private Text enemyCyborgCount;

    [SerializeField] private Text PunkHud;
    [SerializeField] private Text MercHud;
    [SerializeField] private Text HackerHud;
    [SerializeField] private Text CyborgHud;
    [SerializeField] private Text strengthHud;
    [SerializeField] private Text creditsHud;

    [SerializeField] private bool playerHasStrengthAdvantage = false; // determines whether player has strength advantage
    [SerializeField] private bool playerHasNumbersAdvantage = false; // determines whether player has numbers advantage

    [SerializeField] AudioSource playerSoundManager;
    [SerializeField] public AudioSource musicManager;
    [SerializeField] AudioSource soundFXManager;
    [SerializeField] AudioClip clickClip;
    [SerializeField] public AudioClip safeClip;
    [SerializeField] public AudioClip battleClip;
    [SerializeField] AudioClip victoryClip;
    [SerializeField] AudioClip defeatClip;

    public PlayerArmy playerArmy;
    public EnemyArmy enemyArmy;

    public float playerStrength;

    public float bounds;
    
    public enum Modes { first_person, third_person}
    public Modes currentMode;

    public bool isDead = false;
    public bool isBattling = false;

    public GameObject[] playerObjs;

    public GameObject[] patrolPoints;

    public GameObject cyberlord;

    private void Awake()
    {
        playerObjs[1].SetActive(false);

        SlowMoActive(false);
    }

    private void Start()
    {
        // Set the mode to first person on startup
        currentMode = Modes.first_person;
    }

    private void Update()
    {
        if (cyberlord == null)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("win");
        }

        playerArmy.EnsureCorrectTroopNumbers();
        playerStrength = (playerArmy.punks * 2 + playerArmy.mercs * 4 + playerArmy.hackers * 4 + playerArmy.cyborgs * 8) + (playerArmy.totalTroops / 4);

        // Handle In-Game Menu
        if (Input.GetKeyDown(KeyCode.P))
        {
            togglePauseMenu();
        }

        // Slow mo effecr
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1)
            {
                SlowMoActive(true);
            }
            else
            {
                SlowMoActive(false);
            }
        }

        if (isBattling)
        {
            musicManager.clip = battleClip;
            if (!musicManager.isPlaying)
            {
                musicManager.Play();
            }

            battlePanel.SetActive(true);
            hudPanel.SetActive(false);
            enemyOverlayPanel.SetActive(false);

            SlowMoActive(false);
            Time.timeScale = 0;
        }
        else
        {
            musicManager.clip = safeClip;
            if (!musicManager.isPlaying)
            {
                musicManager.Play();
            }

            battlePanel.SetActive(false);
            hudPanel.SetActive(true);

            if (currentMode == Modes.third_person)
            {
                ManageEnemyOverlay();
            }
        }

        if (victoryPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                victoryPanel.SetActive(false);
                UnPauseGame();
            }
        }

        ManagePerspective();

        // Update HUD
        PunkHud.text = "punks x " + playerArmy.punks.ToString();
        MercHud.text = "mercs x " + playerArmy.mercs.ToString();
        HackerHud.text = "hackers x " + playerArmy.hackers.ToString();
        CyborgHud.text = "cyborgs x " + playerArmy.cyborgs.ToString();
        strengthHud.text = "strength: " + playerStrength.ToString();
        creditsHud.text = "Credits: " + playerArmy.credits.ToString();
    }

    private void SlowMoActive(bool active)
    {
        if (active)
        {
            Time.timeScale = 0.10f;
            playerSoundManager.pitch = 0.2f;
            musicManager.pitch = 0.35f;
        }
        else
        {
            Time.timeScale = 1;
            playerSoundManager.pitch = 0.8f;
            musicManager.pitch = 1;
        }
    }

    private void ManageEnemyOverlay()
    {
        // Get ray from mouse pos
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; // this is set by Physics.Raycast()
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag == "enemy")
            {
                enemyOverlayPanel.SetActive(true);
                string text = "Name: " + hit.collider.name + "\nStrength: " + hit.collider.GetComponent<EnemyArmy>().enemyStrength + "\nBounty: " + hit.collider.GetComponent<EnemyArmy>().rewardAmount;
                enemyOverlayText.text = text;

                enemyOverlayPanel.transform.position = Input.mousePosition;
            }
            else
            {
                enemyOverlayPanel.SetActive(false);
            }
        }
    }

    private void ManagePerspective()
    {
        if (currentMode == Modes.first_person)
        {
            playerObjs[0].SetActive(true);
            playerObjs[1].SetActive(false);

            if (!pausePanel.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
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

    public void UnPauseGame()
    {
        Time.timeScale = 1;
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
        /* How should calculation work?
         * 
         * punks == 2 strength
         * Mercs == 4 strength
         * Hackers == 4 strength, allows killing cyborgs
         * Cyborgs == 8 strength, allows killing cyborgs
         * 
         * greater totalStrength == advatange on rolls
         * greater totalCount == advantage on rolls
        */

        // Perform calc to determine losses
        // calc player advantages
        int playerTotalStrength = (playerArmy.punks * 2 + playerArmy.mercs * 4 + playerArmy.hackers * 4 + playerArmy.cyborgs * 8) * playerArmy.totalTroops;
        int enemyTotalStrength = (enemyArmy.punks * 2 + enemyArmy.mercs * 4 + enemyArmy.hackers * 4 + enemyArmy.cyborgs * 8) * enemyArmy.totalTroops;
        

        if (playerTotalStrength > enemyTotalStrength){ playerHasStrengthAdvantage = true; }
        if (playerArmy.totalTroops > enemyArmy.totalTroops) { playerHasNumbersAdvantage = true; }

        // Calculate losses
        CalculateBattleLosses();

        // Check if one party was victorious
        if (playerArmy.totalTroops <= 0)
        {
            EndBattle(false);
            ResetPlayerPositions();
            victoryText.text = "A pathetic display!";
            victoryPanel.SetActive(true);

            // Partial game reset
            playerArmy.punks = 1;
            playerArmy.credits = 100;
            currentMode = Modes.first_person;
            ResetPlayerPositions();

        }
        else if (enemyArmy.totalTroops <= 0)
        {
            EndBattle(true);
            enemyArmy.DestroyArmy();
            victoryText.text = "You are Victorious!!";
            victoryPanel.SetActive(true);
        }

        // Reflect losses UI
        playerArmy.EnsureCorrectTroopNumbers();
        enemyArmy.EnsureCorrectTroopNumbers();
        SetBattleUIStats();

        soundFXManager.PlayOneShot(clickClip);
    }

    public void EndBattle(bool victory)
    {
        isBattling = false;

        if (victory)
        {
            SlowMoActive(true);
            soundFXManager.PlayOneShot(victoryClip);
        }
        else
        {
            soundFXManager.PlayOneShot(defeatClip);
        }
    }

    public int GenerateRandomTroopLoss(int max)
    {
        return Random.Range(0, max + 1);
    }

    public void CalculateBattleLosses()
    {
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
    }

    public void ResetPlayerPositions()
    {
        playerObjs[0].transform.position = new Vector3(0, 0, 23.5f);
        playerObjs[1].transform.position = new Vector3(0, 0, 40);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("title");
    }

    public void togglePauseMenu()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            SlowMoActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            pausePanel.SetActive(true);
            SlowMoActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
