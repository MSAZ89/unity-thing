using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public string objectID; // unique ID per object

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public PhysicsObjectData GetData()
    {
        return new PhysicsObjectData
        {
            objectID = objectID,
            posX = transform.position.x,
            posY = transform.position.y,
            posZ = transform.position.z,
            rotX = transform.rotation.x,
            rotY = transform.rotation.y,
            rotZ = transform.rotation.z,
            rotW = transform.rotation.w,
            velX = rb.velocity.x,
            velY = rb.velocity.y,
            velZ = rb.velocity.z,
            angVelX = rb.angularVelocity.x,
            angVelY = rb.angularVelocity.y,
            angVelZ = rb.angularVelocity.z
        };
    }

    public void ApplyData(PhysicsObjectData data)
    {
        transform.position = new Vector3(data.posX, data.posY, data.posZ);
        transform.rotation = new Quaternion(data.rotX, data.rotY, data.rotZ, data.rotW);

        rb.velocity = new Vector3(data.velX, data.velY, data.velZ);
        rb.angularVelocity = new Vector3(data.angVelX, data.angVelY, data.angVelZ);
    }
}
