using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : ISaveLoadManager
{
    string filePath;

    public SaveLoadManager()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
        Debug.Log(filePath);
        //hp = Rock.GetComponent<Rock>();
    }

    public void Damage()
    {
        //hp._maxHpRock -= 10;
    }

    public void SaveData(int rockHealth)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Create);

        Save save = new Save();
        save.HealthStone = rockHealth; // Dan: I've changed saving to current rock Health from PlayerAttack script

        Debug.Log("Saving value: " + rockHealth);
        binaryFormatter.Serialize(fileStream, save);
        fileStream.Close();

        Debug.Log("Game saved at: " + filePath);
    }

    public void LoadData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Save file not found!");
            return;
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Open);

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