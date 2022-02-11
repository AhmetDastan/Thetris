using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
/*
public class SaveManager//: MonoBehaviour
{
    private Player player;
    FileStream file;
    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }



    public void Save()
    {
        player.SendPlayerStatsToPlayerStatsSaveFile();

        file = new FileStream(Application.persistentDataPath + "/BestScores.dat", FileMode.OpenOrCreate);


        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, player.playerStats);
        }
        catch (SerializationException e)
        {
            Debug.LogError("save manager issue " + e.Message);
        }
        finally
        {
            file.Close();
        }

    }

    public void Load()
    {
        if (SlotManagment.slot1IsPlaying)
        {
            file = new FileStream(Application.persistentDataPath + "/PlayerSlot1.dat", FileMode.Open);
        }
        else if (SlotManagment.slot2IsPlaying)
        {
            file = new FileStream(Application.persistentDataPath + "/PlayerSlot2.dat", FileMode.Open);
        }
        else if (SlotManagment.slot3IsPlaying)
        {
            file = new FileStream(Application.persistentDataPath + "/PlayerSlot3.dat", FileMode.Open);
        }
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            player.playerStats = (PlayerStats)formatter.Deserialize(file);
            Debug.Log("loadlandin");
        }
        catch (SerializationException e)
        {
            Debug.LogError("lOAD manager issue " + e.Message);
        }
        finally
        {
            file.Close();
        }
        player.TakeToPlayerStatsFromPlayerStatsSaveFile();

    }

    public void ResetSlot1()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerSlot1.dat"))
        {
            try
            {
                File.Delete(Application.persistentDataPath + "/PlayerSlot1.dat");
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                Debug.Log("PLAYER SLOT 1 SILINDI");
            }
        }
        else Debug.Log("zaten dosya yok");
    }
}*/