using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileManager
{
    static BinaryFormatter bf = null;
    public static void SaveToBinary<T>(string filePath, T data)
    {
        using (FileStream fs = File.Create(filePath))
        {
            if (bf == null) bf = new BinaryFormatter();

            bf.Serialize(fs, data);
        }

    }
    public static T LoadFormBinary<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                if (bf == null) bf = new BinaryFormatter();
                return (T)bf.Deserialize(fs);

            }
        }
        return default;
    }
}
