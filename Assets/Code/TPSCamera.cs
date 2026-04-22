using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -5f);
    public float sensitivity = 3f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        rotationY = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -20f, 60f);

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}