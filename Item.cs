using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemID;

    public ItemData GetData()
    {
        return new ItemData
        {
            itemID = itemID,
            posX = transform.position.x,
            posY = transform.position.y,
            posZ = transform.position.z
        };
    }

    public void ApplyData(ItemData data)
    {
        transform.position = new Vector3(data.posX, data.posY, data.posZ);
    }
}
