using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void SaveGameData(StoredData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/save.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    } 


    public static StoredData LoadGameData()
    {
        string path = Application.persistentDataPath + "/save.fun";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StoredData data = formatter.Deserialize(stream) as StoredData;
            stream.Close();

            return data;
        } else
        {
            return new StoredData();
        }
    }
}
