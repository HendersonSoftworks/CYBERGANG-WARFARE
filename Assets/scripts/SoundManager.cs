using UnityEngine;
using UnityEngine.AI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] FirstPersonMovementController firstPerson;

    private void Update()
    {
        if ((agent.hasPath & agent != null) || (firstPerson.x != 0 || firstPerson.z != 0))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            audioSource.clip = audioClips[0];
            ResumeSound();
        }
        else
        {
            PauseSound();
        }
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
