using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DevZhrssh.SaveSystem
{
    public class SaveSystem : MonoBehaviour
    {
        public void Save<T>(T data, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string newPath = Application.persistentDataPath + "/" + path;
            FileStream stream = new FileStream(newPath, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public T Load<T>(string path)
        {
            string newPath = Application.persistentDataPath + "/" + path;
            if (File.Exists(newPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(newPath, FileMode.Open);

                T data = (T)formatter.Deserialize(stream);
                stream.Close();

                return data;
            }
            else
            {
                return default(T);
            }
        }

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
                SaveData data = new SaveData(0, 0, 0, 0, 0, false, false, false, false, true);
                return data;
            }
        }

        public void DeleteData(string filename)
        {
            string path = Application.persistentDataPath + "/" + filename;
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
