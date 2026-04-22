using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float rotationSpeed = 720f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            controller.Move(moveDir * speed * Time.deltaTime);

            if (animator != null)
                animator.SetBool("isRunning", true);
        }
        else
        {
            if (animator != null)
                animator.SetBool("isRunning", false);
        }

        if (!controller.isGrounded)
            controller.Move(Vector3.down * 9.81f * Time.deltaTime);
    }
}