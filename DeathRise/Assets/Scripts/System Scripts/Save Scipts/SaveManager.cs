using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public  class SaveManager 
{
    public static string directory = "SaveData";
    public static string fileName = "SaveFile.dat";
    public static void Save(SaveObject so)
    {
        FileStream fileSave = new FileStream(GetFulPath(), FileMode.OpenOrCreate); 
        BinaryFormatter bf = new BinaryFormatter();

        Debug.Log("actim");

        bf.Serialize(fileSave, so);
        Debug.Log("kaydettim");

        fileSave.Close();
        Debug.Log("dosya save etti");
        
       /* using (var file = File.Open(GetFulPath(), FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(file, so);
        }*/
    }

    public static SaveObject Load()  
    {
        if (SaveExists())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(GetFulPath(), FileMode.Open);
            if(file.Length > 0)
            {
                SaveObject so = (SaveObject)bf.Deserialize(file);
                Debug.Log("save is succesful");
                return so;
            }
            Debug.Log("file is empty");
            file.Close();
        }
        
     /*   if (SaveExists())
        { 
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetFulPath(), FileMode.Open);
                SaveObject so = (SaveObject)bf.Deserialize(file);
                file.Close();  
                return so;
            }
            catch(SerializationException)
            {
                Debug.Log("Failed to load file ! ");
            } 
        }
        else
        {
            Debug.Log("There is no file doc ! "); 
        }*/
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

    public  static void DeleteFile()
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
