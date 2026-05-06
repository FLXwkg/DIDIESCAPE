using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CriminalAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform destination;
    private bool gameOver = false;
    private bool hasStartedMoving = false;

    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        yield return null;

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("Le dummy n'est PAS sur le NavMesh !");
            yield break;
        }

        agent.updateRotation = true;
        agent.angularSpeed = 0f;

        if (destination != null)
        {
            agent.SetDestination(destination.position);
        }
    }

    void Update()
    {
        if (gameOver) return;

        // Rotation du criminel
        if (agent != null && agent.isOnNavMesh && agent.velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        // Attend que le criminel bouge avant de checker la défaite
        if (agent != null && agent.isOnNavMesh && !agent.pathPending && agent.remainingDistance > 2f)
        {
            hasStartedMoving = true;
        }

        // Défaite : le criminel atteint sa destination
        if (hasStartedMoving && agent.remainingDistance < 1.5f)
        {
            GameOver(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameOver(true);
        }
    }

    void GameOver(bool playerWins)
    {
        gameOver = true;
        
        if (AudioManager.instance != null)
            AudioManager.instance.StopFootsteps();

        if (agent != null)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }

        if (animator != null)
            animator.speed = 0f;

        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().enabled = false;
            var playerAnim = player.GetComponent<Animator>();
            if (playerAnim != null)
                playerAnim.speed = 0f;
        }

        if (playerWins)
        {
            AudioManager.instance.PlayVictory();
            GameUI.ShowMessage("VICTOIRE !");
        }
        else
        {
            AudioManager.instance.PlayGameOver();
            GameUI.ShowMessage("DÉFAITE !");
        }
    }
}