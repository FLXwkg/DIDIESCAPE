using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CriminalAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform destination;

    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        yield return null;

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("Le dummy n'est PAS sur le NavMesh ! Position : " + transform.position);
            yield break;
        }

        if (destination != null)
        {
            agent.SetDestination(destination.position);
        }
    }

    void Update()
    {
        if (animator != null && agent != null)
        {
            // Active l'animation quand le dummy bouge
            animator.SetBool("isRunning", agent.velocity.magnitude > 0.1f);
        }
    }
}