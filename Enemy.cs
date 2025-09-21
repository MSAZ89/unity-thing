using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyID;
    public int health = 100;

    public EnemyData GetData()
    {
        return new EnemyData
        {
            enemyID = enemyID,
            posX = transform.position.x,
            posY = transform.position.y,
            posZ = transform.position.z,
            rotX = transform.rotation.x,
            rotY = transform.rotation.y,
            rotZ = transform.rotation.z,
            rotW = transform.rotation.w,
            health = health
        };
    }

    public void ApplyData(EnemyData data)
    {
        transform.position = new Vector3(data.posX, data.posY, data.posZ);
        transform.rotation = new Quaternion(data.rotX, data.rotY, data.rotZ, data.rotW);
        health = data.health;
    }
}
