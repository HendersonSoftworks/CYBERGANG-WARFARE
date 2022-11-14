using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
}
