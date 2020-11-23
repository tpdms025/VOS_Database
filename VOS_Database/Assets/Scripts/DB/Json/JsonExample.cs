using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonExample : MonoBehaviour
{
    readonly public string actualPath = Application.streamingAssetsPath + "/";
    private List<JTestClass> JsonInfoList = new List<JTestClass>();

    void Start()
    {
        JTestClass jtc = new JTestClass(true);
        string jsonData = ObjectToJson(jtc);

        CreateJsonFile(actualPath, "JsonTest1", jsonData);
        Debug.Log(jsonData);

        //var jtc2 = JsonToOject<JTestClass>(jsonData);
        //jtc2.Print();
    }

    string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    T JsonToOject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }



    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);

        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);

        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }

    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
           

        }
    }
}
