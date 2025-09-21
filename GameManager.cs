using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public int score;
    private List<PhysicsObject> physicsObjects;

    private void Awake()
    {
        physicsObjects = new List<PhysicsObject>(FindObjectsOfType<PhysicsObject>());
    }

    private void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        GameData data = new GameData();

        // Player
        data.player = player.GetData();

        // Score
        data.score = score;

        // Enemies
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy e in allEnemies)
            data.enemies.Add(e.GetData());

        // Items
        Item[] allItems = FindObjectsOfType<Item>();
        foreach (Item i in allItems)
            data.items.Add(i.GetData());

        foreach (var obj in physicsObjects)
            data.physicsObjects.Add(obj.GetData());

        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();

        // Player
        if (data.player != null)
            player.ApplyData(data.player);

        // Score
        score = data.score;

        // Enemies
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        foreach (Enemy e in allEnemies)
        {
            EnemyData enemyData = data.enemies.Find(x => x.enemyID == e.enemyID);
            if (enemyData != null)
                e.ApplyData(enemyData);
        }

        // Items
        Item[] allItems = FindObjectsOfType<Item>();
        foreach (Item i in allItems)
        {
            ItemData itemData = data.items.Find(x => x.itemID == i.itemID);
            if (itemData != null)
                i.ApplyData(itemData);
        }

        // Physics Objects
        foreach (var obj in physicsObjects)
        {
            var objData = data.physicsObjects.Find(x => x.objectID == obj.objectID);
            if (objData != null)
                obj.ApplyData(objData);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            SaveGame();
        if (Input.GetKeyDown(KeyCode.F9))
            LoadGame();
    }
}
