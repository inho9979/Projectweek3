using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
   public static void SaveData (PlayerStatData playerData, MapStageData stageData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Save.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerStageData data = new PlayerStageData(playerData, stageData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerStageData LoadData()
    {
        string path = Application.persistentDataPath + "/Save.Fun");
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerStageData data = formatter.Deserialize(stream) as PlayerStageData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }
}
