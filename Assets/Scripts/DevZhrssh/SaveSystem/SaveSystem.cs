using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DevZhrssh.SaveSystem
{
    public class SaveSystem : MonoBehaviour
    {
        public void Save(SaveData save)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/save.game"; // this is the save file
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, save);
            stream.Close();
        }

        public SaveData Load()
        {
            string path = Application.persistentDataPath + "/save.game";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                SaveData data = formatter.Deserialize(stream) as SaveData;
                stream.Close();

                return data;
            }
            else
            {
                SaveData data = new SaveData(0, 0, 0, 0, 0, 0);
                return data;
            }
        }

        public void DeleteData()
        {
            string path = Application.persistentDataPath + "/save.game";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Debug.LogError("Save File Not Found!");
                return;
            }
        }
    }
}
