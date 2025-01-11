using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class DataSaver
{
    public static void Serializable(string path, object data)
    {
        using(FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }

    public static T Deserializable<T>(string path)
    {
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            T data = (T) formatter.Deserialize(stream);
            return data;
        }
    }
}