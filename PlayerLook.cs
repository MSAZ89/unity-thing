using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;      // Player (Rigidbody)
    [SerializeField] private Transform cameraPivot;     // Empty pivot at player center
    [SerializeField]private float maxLookAngle = 30f;
    [SerializeField] private float minLookAngle = 0f;

    private Quaternion targetBodyRotation;
    private Quaternion targetCameraRotation;

    private float xRotation = 0f;
    public float Pitch => xRotation; // read-only property

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        targetBodyRotation = playerBody.rotation;
        targetCameraRotation = cameraPivot.localRotation;

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

        // update yaw
        targetBodyRotation *= Quaternion.Euler(0f, mouseX, 0f);

        // update pitch
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);
        targetCameraRotation = Quaternion.Euler(xRotation, 0f, 0f);
        float smoothingFactor = 10f;
        playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetBodyRotation, smoothingFactor * Time.deltaTime);
        cameraPivot.localRotation = Quaternion.Slerp(cameraPivot.localRotation, targetCameraRotation, smoothingFactor * Time.deltaTime);
    }
}
