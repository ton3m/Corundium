using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    string filePath;
    int a = 100;
    public GameObject Rock;
    Rock hp;

    void Start()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
        Debug.Log(filePath);
        hp.GetComponent<Rock>();
    }

    public void Damage()
    {
        hp._maxHpRock -= 10;
    }

    public void SaveGame()
    {
        Debug.Log("����� ������ Save Game");
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        Save save = new Save();
        save.HealthStone = hp._maxHpRock;

        Debug.Log("Saving value: " + hp._maxHpRock);
        binaryFormatter.Serialize(fileStream, save);
        fileStream.Close();

        Debug.Log("Game saved at: " + filePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Save file not found!");
            return;
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Open);

        Save save = (Save)binaryFormatter.Deserialize(fileStream);
        hp._maxHpRock = save.HealthStone;

        Debug.Log("Loaded value: " + hp._maxHpRock);
        fileStream.Close();
    }
}

[System.Serializable]
public class Save
{
    public int HealthStone;

}