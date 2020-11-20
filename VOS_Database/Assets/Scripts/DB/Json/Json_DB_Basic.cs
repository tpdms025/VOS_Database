using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Json_DB_Basic
{
    protected string filepath;


    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }




    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        filepath = DBManager.Inst.actualPath + createPath;

        //try
        //{

        //}
        //catch(DirectoryNotFoundException e)
        //{
        //    Directory.CreateDirectory(filepath);

        //}
        //catch(Exception e)
        //{
        //    return;
        //}

        //디렉토리가 없다면 생성
        if (!File.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        //파일이 없다면 생성
        if (!File.Exists(filepath))
        {
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", filepath, fileName), FileMode.Create);

            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }



    public T LoadJsonFile<T> (string loadPath,string fileName)
    {
        filepath = DBManager.Inst.actualPath + loadPath;

        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);

        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }

}
