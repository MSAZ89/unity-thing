using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CapsuleCollider capsule; // child collider
    [SerializeField] private LayerMask groundMask;    

    private Rigidbody rb;
    private bool jumpPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // capture jump input
        if (Input.GetKeyDown(KeyCode.Space))
            jumpPressed = true;
    }

    void FixedUpdate()
    {
        Move();

        if (jumpPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = false; // consume the jump
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Input vector in local space
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        // Convert to world space using player yaw only
        Vector3 moveDir = transform.TransformDirection(inputDir); // transform = player body

        // Sprint
        if (Input.GetKey(KeyCode.LeftShift))
            moveDir *= 2f;

        // Apply horizontal velocity change
        Vector3 desiredVelocity = moveDir * movementSpeed;
        Vector3 velocityChange = desiredVelocity - new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    bool IsGrounded()
    {
        Vector3 spherePos = capsule.bounds.center - new Vector3(0, capsule.bounds.extents.y + 0.01f, 0);
        float radius = 0.25f;
        return Physics.CheckSphere(spherePos, radius, groundMask);
    }
}
