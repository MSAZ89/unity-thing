using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public PlayerData player;
    public int score;
    public List<EnemyData> enemies = new List<EnemyData>();
    public List<ItemData> items = new List<ItemData>();
    public List<PhysicsObjectData> physicsObjects = new List<PhysicsObjectData>();
}

[Serializable]
public class PlayerData
{
    public float posX, posY, posZ;
    public float rotX, rotY, rotZ, rotW;
}

[Serializable]
public class EnemyData
{
    public string enemyID; // optional unique identifier
    public float posX, posY, posZ;
    public float rotX, rotY, rotZ, rotW;
    public int health;
}

[Serializable]
public class ItemData
{
    public string itemID;
    public float posX, posY, posZ;
}

[System.Serializable]
public class PhysicsObjectData
{
    public string objectID; // unique name or ID
    public float posX, posY, posZ;
    public float rotX, rotY, rotZ, rotW;
    public float velX, velY, velZ; // optional: velocity
    public float angVelX, angVelY, angVelZ; // optional: angular velocity
}