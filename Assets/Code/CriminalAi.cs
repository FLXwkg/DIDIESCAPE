using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CriminalAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform destination;

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
        if (agent != null && agent.isOnNavMesh && agent.velocity.magnitude > 0.1f)
        {
            // Tourne le personnage dans la direction du mouvement
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}