using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad  {

    public static void Save(bool[] levels, int Gold)
    {
        BinaryFormatter br = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        SaveData data = new SaveData(levels,Gold);
        br.Serialize(stream,data);
        stream.Close();

    }
    
    
    public static SaveData Load()
    {
       
        if(File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter br = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);
            SaveData data = br.Deserialize(stream)as SaveData;
            
            stream.Close();
            return data;
        }

        return null;
       

    }



}
[Serializable]
public class SaveData
{
    public int gold=0;
    public bool[] savedLevels;
    
    public SaveData(bool[] levels, int Gold)//ayri 2 constracterda kaydedemeyiz cunku diger  deger null olarak kaydolur oyzude beraber kaydediyoruz leevels ve gold u
    {
        savedLevels = levels;
        gold = Gold;
    }
}
