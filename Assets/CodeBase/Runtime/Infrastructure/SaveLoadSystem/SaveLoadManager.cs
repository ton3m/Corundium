using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour, ISaveLoadManager
{
    private string _filePath;
    private GameObject _rock;
    
    //debug
    private int _a = 100;
    private Rock _hp;
    
    private void Start()
    {
        _filePath = Application.persistentDataPath + "/save.gamesave";
    }

    public void Damage()
    {
        //hp._maxHpRock -= 10;
    }

    public void SaveGame(int rockHealth)
    {
        Debug.Log("����� ������ Save Game");
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(_filePath, FileMode.Create);

        Save save = new Save();
        save.HealthStone = rockHealth; // Dan: I've changed saving to current rock Health from PlayerAttack script

        Debug.Log("Saving value: " + rockHealth);
        binaryFormatter.Serialize(fileStream, save);
        fileStream.Close();

        Debug.Log("Game saved at: " + _filePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(_filePath))
        {
            Debug.LogError("Save file not found!");
            return;
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(_filePath, FileMode.Open);

        Save save = (Save)binaryFormatter.Deserialize(fileStream);
        int loadedDamage = save.HealthStone;

        Debug.Log("Loaded value: " + loadedDamage);
        fileStream.Close();
    }
}

[System.Serializable]
public class Save
{
    public int HealthStone;
}