using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public PlayerData GetData()
    {
        return new PlayerData
        {
            posX = rb.position.x,
            posY = rb.position.y,
            posZ = rb.position.z,
            rotX = rb.rotation.x,
            rotY = rb.rotation.y,
            rotZ = rb.rotation.z,
            rotW = rb.rotation.w
        };
    }

    public void ApplyData(PlayerData data)
    {
        rb.position = new Vector3(data.posX, data.posY, data.posZ);
        rb.rotation = new Quaternion(data.rotX, data.rotY, data.rotZ, data.rotW);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
