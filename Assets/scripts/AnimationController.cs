using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] Animator animator;

    private void Update()
    {
        if (agent.hasPath)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }
}
