using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CriminalAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform destination;
    public float catchDistance = 1.5f;
    private bool gameOver = false;
    private bool hasStartedMoving = false;

    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();

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

        // Défaite : le criminel atteint sa destination
        if (agent != null && agent.isOnNavMesh && !agent.pathPending && agent.remainingDistance > 1f)
        {
            hasStartedMoving = true;
        }

        if (hasStartedMoving && agent.remainingDistance <= agent.stoppingDistance)
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
        agent.isStopped = true;

        if (playerWins)
            GameUI.ShowMessage("VICTOIRE !");
        else
            GameUI.ShowMessage("DÉFAITE !");

        // Arrête aussi le joueur
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            player.GetComponent<PlayerController>().enabled = false;
    }
}