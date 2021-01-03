using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SC_SaveFile {

    public static SC_SaveFile save;

    public int currentProgress;

    public int currentProgressStep;

    public static void Save () {

        BinaryFormatter bf = new BinaryFormatter ();
        FileStream file = File.Create (Application.persistentDataPath + "/gamesave.save");
        bf.Serialize (file, save);
        file.Close ();

    }

    public static void SetSave () {

        BinaryFormatter bf = new BinaryFormatter ();
        FileStream file;

        if (File.Exists (Application.persistentDataPath + "/gamesave.save")) {

            file = File.Open (Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            save = (SC_SaveFile) bf.Deserialize (file);            
            
        } else {

            save = new SC_SaveFile ();
            
            file = File.Create (Application.persistentDataPath + "/gamesave.save");
            bf.Serialize (file, save);

        }

        file.Close ();

    }

    /*public void SaveValue (string id, Object value) {

        BinaryFormatter bf = new BinaryFormatter ();

        if (File.Exists (Application.persistentDataPath + "/gamesave.save")) {
            
            FileStream file = File.Open (Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SC_SaveFile s = (SC_SaveFile) bf.Deserialize (file);

            typeof (SC_SaveFile).GetField (id).SetValue (s, value);
            Debug.Log (s.currentProgress);            

            file.Close ();           

        } else {

            SC_SaveFile s = new SC_SaveFile ();

            FileStream file = File.Create (Application.persistentDataPath + "/gamesave.save");
            bf.Serialize (file, s);
            file.Close ();

            SaveValue (id, value);

        }

    }*/

}
