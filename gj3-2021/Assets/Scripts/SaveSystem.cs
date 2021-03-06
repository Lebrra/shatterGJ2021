using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(GameData game)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "GameData.neat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, game);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "GameData.neat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No file found in " + path);
            return null;
        }
    }
}

public class GameData
{
    public LevelData[] levels;

    public GameData()
    {
        levels = new LevelData[3];  //update this
        for(int i = 0; i < levels.Length; i++)
        {
            levels[i] = new LevelData();
        }
        levels[0].unlocked = true;
    }
}

public class LevelData
{
    public bool unlocked;
    public int collectibleCount;
    public float finishTime;

    public LevelData()
    {
        unlocked = false;
        collectibleCount = 0;
        finishTime = -1;
    }
}