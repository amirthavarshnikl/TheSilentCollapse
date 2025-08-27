using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;
    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize();

        moveInput = (forward * moveZ + right * moveX).normalized;

        // Rotate towards movement direction
        if (moveInput.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void FixedUpdate()
    {
        Vector3 moveVelocity = moveInput * moveSpeed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}


