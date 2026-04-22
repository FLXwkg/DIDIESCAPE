using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowDuration = 2f;
    public float slowFactor = 0.3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplySlow(slowFactor, slowDuration);
                Debug.Log("Joueur ralenti par une zone !");
            }
        }
    }
}