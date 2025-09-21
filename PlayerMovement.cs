using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    
    [Header("References")]
    [SerializeField] private CapsuleCollider capsule;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private CameraFollow cameraFollow;

    private Rigidbody rb;
    private bool jumpPressed;
    private float rotationVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Auto-find camera if not assigned
        if (cameraFollow == null)
        {
            cameraFollow = FindObjectOfType<CameraFollow>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jumpPressed = true;
    }

    void FixedUpdate()
    {
        Move();
        RotateToCamera();

        if (jumpPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = false;
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            // Move relative to camera direction (only Y rotation)
            float cameraYaw = cameraFollow.Yaw;
            Vector3 moveDir = Quaternion.Euler(0, cameraYaw, 0) * inputDir;

            Vector3 targetVel = moveDir * movementSpeed;
            Vector3 velocity = rb.velocity;
            velocity.x = targetVel.x;
            velocity.z = targetVel.z;
            rb.velocity = velocity;
        }
    }

    void RotateToCamera()
    {
        // Smoothly rotate player to face camera direction
        float targetAngle = cameraFollow.Yaw;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    bool IsGrounded()
    {
        Vector3 spherePos = capsule.bounds.center - Vector3.up * (capsule.bounds.extents.y + 0.01f);
        return Physics.CheckSphere(spherePos, 0.25f, groundMask);
    }
}