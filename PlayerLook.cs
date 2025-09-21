using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;      // Player (Rigidbody)
    [SerializeField] private Transform cameraPivot;     // Empty pivot at player center
    [SerializeField]private float maxLookAngle = 30f;
    [SerializeField] private float minLookAngle = 0f;
    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        // Initialize xRotation to match pivot's current local X rotation
        xRotation = cameraPivot.localEulerAngles.x;
        
        // Unity returns 0-360, convert >180 to negative
        if (xRotation > 180f)
            xRotation -= 360f;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // yaw (left/right)
        playerBody.Rotate(Vector3.up * mouseX);

        // pitch (up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);
        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
