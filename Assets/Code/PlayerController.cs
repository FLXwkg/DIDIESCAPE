using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public Transform cameraTransform;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Debug.Log("=== PlayerController START ===");
    }

    void Update()
    {
        if (Input.anyKey)
            Debug.Log("UNE TOUCHE EST PRESSEE");

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
            Debug.Log("Z ou W pressé !");

        if (Input.anyKeyDown)
            Debug.Log("Touche pressée : " + Input.inputString);

        float h = 0f;
        float v = 0f;

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) v = 1f;
        if (Input.GetKey(KeyCode.S)) v = -1f;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) h = -1f;
        if (Input.GetKey(KeyCode.D)) h = 1f;

        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction.magnitude >= 0.1f && cameraTransform != null)
        {
            Vector3 moveDir = cameraTransform.forward * v + cameraTransform.right * h;
            moveDir.y = 0f;
            moveDir.Normalize();
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (!controller.isGrounded)
            controller.Move(Vector3.down * 9.81f * Time.deltaTime);
    }
}