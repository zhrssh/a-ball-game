using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DevZhrssh.SaveSystem
{
    public class SaveSystem : MonoBehaviour
    {

        public void Save<T>(T data, string filename)
        {
            string jsonString = JsonUtility.ToJson(data);
            string path = Application.persistentDataPath + "/" + filename + ".json";

            using StreamWriter writer = new StreamWriter(path);
            writer.Write(jsonString);
            writer.Close();


/*          Removed because of security issues

            BinaryFormatter formatter = new BinaryFormatter();
            string newPath = Application.persistentDataPath + "/" + filename;
            FileStream stream = new FileStream(newPath, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
*/
        }

        public T Load<T>(string filename)
        {
            string path = Application.persistentDataPath + "/" + filename + ".json";
            if (File.Exists(path))
            {
                using StreamReader reader = new StreamReader(path);
                string json = reader.ReadToEnd();
                reader.Close();

                T data = JsonUtility.FromJson<T>(json);

                return data;

/*              Removed because of security issues
                
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                T data = (T)formatter.Deserialize(stream);
                stream.Close();

                return data;
*/
            }
            else
            {
                return default(T);
            }
        }

        public void DeleteData(string filename)
        {
            string path = Application.persistentDataPath + "/" + filename + ".json";
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
