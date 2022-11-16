using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundManager playerSoundManager;
    [SerializeField] private FirstPersonMovementController firstPerson;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameManager.currentMode == GameManager.Modes.first_person)
        {
            gameManager.currentMode = GameManager.Modes.third_person;
            gameManager.ResetPlayerPositions();
        }
        else if (other.tag == "Player" && gameManager.currentMode == GameManager.Modes.third_person)
        {
            gameManager.currentMode = GameManager.Modes.first_person;
            gameManager.ResetPlayerPositions();
        }
    }
}
