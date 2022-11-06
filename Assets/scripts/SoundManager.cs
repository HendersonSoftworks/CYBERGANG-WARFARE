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
            audioSource.UnPause();
            //audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
