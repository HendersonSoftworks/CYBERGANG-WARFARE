using UnityEngine;
using UnityEngine.AI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    private void Update()
    {
        if (agent.hasPath)
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
