// ==============================================================
// DB 기본구조 (Json Version)
//
// AUTHOR: Yang SeEun
// CREATED: 2020-11-24
// UPDATED: 2020-11-26
// ==============================================================




using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DBBasic_Json
{
    readonly public string actualPath = Application.streamingAssetsPath + "/";

    protected string fullpath;
    protected string filepath;
    protected string fileName;
    protected string dataType;

    [SerializeField] protected bool saving = false;
    protected bool isInsertEnd = false;


    public void AppendData<T>(T database, DataType type = DataType.Timestamp) where T : new()
    {
        //Debug.Log(type.ToString());
        dataType = type.ToString();
        string preDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        string originFileName = string.Format("Origin_{0}_", preDate);

        InitFileInfo(Path.Combine("InternalData", "Origin", preDate), originFileName);

        string jsonData = ObjectToJson(database);
        WriteToFile(jsonData);

        Debug.Log("jsonData " + jsonData);
    }

    //public void CreateJsonFile(string createPath, string _fileName, string _jsonData)
    //{
    //    filepath = actualPath + createPath;
    //    fileName = _fileName;
    //    fullpath = Path.Combine(filepath, fileName);

    //    //try
    //    //{

    //    //}
    //    //catch(DirectoryNotFoundException e)
    //    //{
    //    //    Directory.CreateDirectory(filepath);

    //    //}
    //    //catch(Exception e)
    //    //{
    //    //    return;
    //    //}

    //    //디렉토리가 없다면 생성
    //    if (!File.Exists(filepath))
    //    {
    //        Directory.CreateDirectory(filepath);
    //    }

    //    //파일이 없다면 생성
    //    if (!File.Exists(fullpath))
    //    {
    //        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", filepath, fileName), FileMode.Create);

    //        byte[] data = Encoding.UTF8.GetBytes(_jsonData);
    //        fileStream.Write(data, 0, data.Length);
    //        fileStream.Close();
    //    }
    //}

    public void InitFileInfo(string createPath, string _fileName)
    {

        filepath = actualPath + createPath;
        fileName = _fileName + dataType + ".json";
        fullpath = Path.Combine(filepath, fileName);
    }

    public void WriteToFile(string jsonData)
    {
        //디렉토리가 없다면 생성
        if (!File.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        //파일이 있는지 확인
        if (File.Exists(fullpath))
        {
            //FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", filepath, fileName), FileMode.Open, FileAccess.Write);
            FileStream fileStream = new FileStream(fullpath, FileMode.Open, FileAccess.Write);

            byte[] data = Encoding.UTF8.GetBytes(jsonData.Substring(1));
            fileStream.Seek(-1, SeekOrigin.End);
            fileStream.Write(Encoding.UTF8.GetBytes(","), 0, Encoding.UTF8.GetBytes(",").Length);
            fileStream.Write(data, 0, data.Length);
            //fileStream.SetLength(fileStream.Position);
            fileStream.Close();
        }
        else
        {
            //파일이 없다면 파일생성
            //FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", filepath, fileName), FileMode.Create);
            FileStream fileStream = new FileStream(fullpath, FileMode.Create, FileAccess.Write);

            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        string targetPath = actualPath + loadPath;

        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", targetPath, fileName), FileMode.Open, FileAccess.Read);

        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        //return JsonUtility.FromJson<T>(jsonData);
        return JsonToObject<T>(jsonData);
    }



    private string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }

    private T JsonToObject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

}
