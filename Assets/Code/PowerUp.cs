using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 3f;
    public float factor = 2f;
    public float rotateSpeed = 100f;
    public float floatHeight = 0.3f;
    public float floatSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ApplyBoost(factor, duration);
            AudioManager.instance.PlayPickup();
            Destroy(gameObject);
        }
    }
}