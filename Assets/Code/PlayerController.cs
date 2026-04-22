using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float rotationSpeed = 720f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;
    private float verticalVelocity = 0f;
    private float currentSpeed;
    private float slowTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentSpeed = speed;
    }

    public void ApplySlow(float factor, float duration)
    {
        currentSpeed = speed * factor;
        slowTimer = duration;
        Debug.Log("Ralenti !");
    }

    public void ApplyBoost(float factor, float duration)
    {
        currentSpeed = speed * factor;
        slowTimer = duration;
        Debug.Log("Boost !");
    }

    void Update()
    {
        // Gestion du slow/boost timer
        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0f)
                currentSpeed = speed;
        }

        float h = 0f;
        float v = 0f;

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) v = 1f;
        if (Input.GetKey(KeyCode.S)) v = -1f;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) h = -1f;
        if (Input.GetKey(KeyCode.D)) h = 1f;

        // Gravité stable
        if (controller.isGrounded)
            verticalVelocity = -0.5f;
        else
            verticalVelocity -= 9.81f * Time.deltaTime;

        Vector3 direction = new Vector3(h, 0f, v).normalized;
        Vector3 move = Vector3.zero;

        if (direction.magnitude >= 0.1f && cameraTransform != null)
        {
            Vector3 moveDir = cameraTransform.forward * v + cameraTransform.right * h;
            moveDir.y = 0f;
            moveDir.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            move = moveDir * currentSpeed;

            if (animator != null)
                animator.SetBool("isRunning", true);
        }
        else
        {
            if (animator != null)
                animator.SetBool("isRunning", false);
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}