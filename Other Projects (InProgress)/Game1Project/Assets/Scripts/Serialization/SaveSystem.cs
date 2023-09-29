using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

/*
 * https://www.youtube.com/watch?v=6uMFEM-napE
 */
public static class SaveSystem
{

    public static void SavePlayer(Player player)
    {

        PlayerData data = new PlayerData(player);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/saveData.txt", json);
        Debug.Log(json);


    }

    public static PlayerData LoadPlayer()
    {
        if(File.Exists(Application.dataPath + "/saveData.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/saveData.txt");
            PlayerData data = JsonUtility.FromJson<PlayerData>(saveString);
            return data;

        }
        return null;

        // JsonUtility.FromJson<PlayerData>(jsonString);
    }

    /*
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter  = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.data";

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();



    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "player.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save File Not Found! " + path);
            return null;
        }
    }


    public static void SaveInventory(InventoryManager inv)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "inv.data";

        FileStream stream = new FileStream(path, FileMode.Create);
        InventoryData data = new InventoryData(inv);

        formatter.Serialize(stream, data);
        stream.Close();


    }

    */
}
