using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static string directory = "SaveData";
    public static string fileName = "SaveFile.dat";
    public static void Save(SaveObject so)
    {
        if (!DirectoryExists())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directory);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileSave = File.Create(GetFulPath()); 


        bf.Serialize(fileSave, so);

        fileSave.Close();
    }

    public static SaveObject Load()
    { 
        if (SaveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetFulPath(), FileMode.Open);

                SaveObject so = (SaveObject)bf.Deserialize(file);
                file.Close();
                return so;
            }
            catch (SerializationException)
            {
                Debug.Log("Failed to load file ! ");
            }
        }
        else
        {
            Debug.Log("There is no file doc ! ");
        }
        return null;
    }

    private static bool SaveExists()
    {
        return File.Exists(GetFulPath());
    }

    private static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directory);
    }

    private static string GetFulPath()
    {
        return Application.persistentDataPath + "/" + directory + "/" + fileName;
    }

    public static void DeleteFile()
    {
        if (SaveExists())
        {
            File.Delete(GetFulPath());
            Debug.Log("File was deleted");
        }
        else
        {
            Debug.Log("File could not find");
        }
    }


}
