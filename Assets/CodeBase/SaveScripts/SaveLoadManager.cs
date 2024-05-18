using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    string filePath;
    int a = 100;
   /* public GameObject Rock;
    Rock hp;*/

    void Start()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
        Debug.Log(filePath);
        //hp.GetComponent<Rock>();
    }

    public void Damage()
    {
        //hp._maxHpRock -= 10;
        a -=10;
    }

    public void SaveGame()
    {
        Debug.Log("Вызов метода Save Game");
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        Save save = new Save();
        save.HealthStone = a; // Сохраняем текущее значение a

        Debug.Log("Saving value: " + a);
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
        a = save.HealthStone; // Загружаем значение в переменную a

        Debug.Log("Loaded value: " + a);
        fileStream.Close();
    }
}

[System.Serializable]
public class Save
{
    public int HealthStone; // Поле для хранения значения

}