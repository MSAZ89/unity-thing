using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path => Path.Combine(Application.persistentDataPath, "gamedata.json");

    public static void SaveGame(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(path, json);
        Debug.Log($"Game saved to {path}");
    }

    public static GameData LoadGame()
    {
        if (!File.Exists(path))
            return new GameData(); // empty game if no file

        string json = File.ReadAllText(path);
        GameData data = JsonUtility.FromJson<GameData>(json);
        Debug.Log("Game loaded");
        return data;
    }
}
