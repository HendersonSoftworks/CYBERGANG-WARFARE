using UnityEngine;
using UnityEngine.AI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] private FirstPersonMovementController firstPerson;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        if (agent.hasPath & agent != null & gameManager.currentMode == GameManager.Modes.third_person)
        {
            ManageSound();
        }
        else if (gameManager.currentMode == GameManager.Modes.first_person && (firstPerson.x != 0 || firstPerson.z != 0))
        {
            ManageSound();
        }
        else
        {
            PauseSound();
        }
    }

    private void ManageSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        audioSource.clip = audioClips[0];
        ResumeSound();
    }

    public void PauseSound()
    {
        audioSource.Pause();
    }

    public void ResumeSound()
    {
        audioSource.UnPause();
    }
}
