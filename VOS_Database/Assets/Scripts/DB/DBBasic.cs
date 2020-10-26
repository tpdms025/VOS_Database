using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using UnityEngine;


public class DBBasic 
{
    protected string filepath;
    public string tableName = string.Empty;

    protected IDbConnection dbConnection;
    protected IDbCommand dbCmd;

    [SerializeField] protected bool saving = false;
    protected bool isInsertEnd = false;

    #region SqlQuery

    protected List<string> createTableQuerys;

    protected virtual string GetInsertQuery(int dataType = 0)
    {
        return string.Empty;
    }

    #endregion

    #region DB Create

    /// <summary>
    /// 데이터베이스 생성
    /// </summary>
    /// <param name="path">생성할 파일경로 (로컬) </param>
    /// <returns></returns>
    public void DBCreate(string path)
    {
        filepath = DBManager.Inst.actualPath + path;
        string targetPath = filepath.Substring(0, filepath.Length - new FileInfo(filepath).Name.Length);

        //디렉토리가 없다면 생성
        if (!File.Exists(targetPath))
        {
            System.IO.Directory.CreateDirectory(targetPath);
        }

        //파일이 없다면 생성
        if (!System.IO.File.Exists(filepath))
        {
            SQLiteConnection.CreateFile(filepath);
        }

        //yield return null;
    }
    #endregion

    #region CreateTable

    public void CreateTable(int dataType)
    { 
        string sqlQuery = createTableQuerys[dataType];

        dbCmd = dbConnection.CreateCommand();

        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteReader();                  //명령어를 Connection 통해 보내고 SqlDataReader 바인딩

        //닫기
        dbCmd.Dispose();
    }
    public void CreateAllTable()
    {
        foreach (string query in createTableQuerys)
        {
            dbCmd = dbConnection.CreateCommand();

            dbCmd.CommandText = query;
            dbCmd.ExecuteReader();                  //명령어를 Connection 통해 보내고 SqlDataReader 바인딩
        }

        //닫기
        dbCmd.Dispose();
    }


    #endregion

    #region DB Connect/DisConnect

    /// <summary>
    /// DB 연결
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public void DBConnect(string path)
    {
        filepath = DBManager.Inst.actualPath + path;

        ////using을 사용함으로써 비정상적인 예외가 발생할 경우에도 반드시 파일을 닫히도록 할 수 있다.
        string connectionString = "URI=file:" + filepath;

        if (dbConnection == null)
        {
            dbConnection = new SqliteConnection(connectionString);
#if UNITY_EDITOR
            Debug.Log("[" + path + "] Create dbConnection");
#endif
        }

        try
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                //열기
                dbConnection.Open();
#if UNITY_EDITOR
                Debug.Log("[" + path + "] db Connect!");
#endif
            }
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.Log("DBConnect Error : " + e.Message);
#endif
        }

        //yield return null;
    }

    public void DBDisconnect()
    {
        if (dbConnection != null)
        {
            if (dbConnection.State != ConnectionState.Closed)
            {
                dbConnection.Close();
            }
            dbConnection = null;
        }

    }
    #endregion

    #region DB Insert

    public void InsertDB(int dataType =0)
    {
        dbCmd = dbConnection.CreateCommand();

        string sqlQuery = GetInsertQuery(dataType);

        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteReader();                  //명령어를 Connection 통해 보내고 SqlDataReader 바인딩

        //닫기
        dbCmd.Dispose();

    }


    #endregion

    #region 사용 미정 Save
    public IEnumerator SaveDB()
    {
        //using을 사용함으로써 비정상적인 예외가 발생할 경우에도 반드시 파일을 닫히도록 할 수 있다.
        string connectionString = "URI=file:" + filepath;

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())  //EnterSqL에 명령 할 수 있다.
            {

                string sqlQuery = string.Empty;
                //string sqlQuery = "UPDATE ItemTable SET waveHeight=" + weatherDataList[i].waveHeight + ",waveSpeed=" + weatherDataList[i].waveSpeed + ",currentSet'" + weatherDataList + "' WHERE ID = " + weatherDataList[i].ID;
                Debug.Log(sqlQuery);
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteReader();


            }
        }
        yield return null;

    }

    #endregion

    #region Load

    private IEnumerator LoadDB(DataType dataType)
    {
        saving = true;

        //Insert중이면 대기
        while (isInsertEnd) { }

        dbCmd = dbConnection.CreateCommand();

        string sqlQuery = "SELECT * FROM " + dataType.ToString();
#if UNITY_EDITOR
        Debug.Log("LoadDB Query is : " + sqlQuery);
#endif
        dbCmd.CommandText = sqlQuery;
        using (IDataReader reader = dbCmd.ExecuteReader())  // 테이블에 있는 데이터들이 들어간다.
        {
            while (reader.Read())
            {
                //TODO :
                SetReader(reader, dataType);
                yield return null;
            }
            reader.Close();
            saving = false;
        }
        dbCmd.Dispose();

        yield return null;

    }

    private void SetReader(IDataReader reader, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.WeatherData:
                Debug.Log(reader.GetInt32(0) + "  " + reader.GetString(1) + "  " + reader.GetString(2) + "  " + reader.GetFloat(3) + "  " + reader.GetInt32(4));
                break;
            case DataType.WindData:
                Debug.Log(reader.GetInt32(0) + "  " + reader.GetString(1) + "  " + reader.GetString(2) + "  " + reader.GetFloat(3));
                break;
            case DataType.WaveData:
                Debug.Log(reader.GetInt32(0) + "  " + reader.GetString(1) + "  " + reader.GetFloat(2) + "  " + reader.GetFloat(3));
                break;
            case DataType.PipeSensorData:
                //배열을 데이터로 어떤식으로 넣을지 생각해봐야함
                //sqlQuery = "INSERT INTO PipeSensorData (Time,CurrentSet,CurrentRate) VALUES('" + pipeSensorData.time + "'," + pipeSensorData.pipes +")";
                break;
            case DataType.RobotData:
                Debug.Log(reader.GetInt32(0) + "  " + reader.GetString(1) + "  " + reader.GetString(2));
                break;
            case DataType.SonarData:
                Debug.Log(reader.GetInt32(0) + "  " + reader.GetString(1) + "  " + reader.GetFloat(2) + "  " + reader.GetFloat(3) + "  " + reader.GetString(4));
                break;
            default:
                break;
        }
    }

    #endregion

   
    #region Delete

    public void DeleteDB(string tableName) 
    {
        dbCmd = dbConnection.CreateCommand();

        string sqlQuery = "DELETE FROM " + tableName;
      
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteReader();

        //닫기
        dbCmd.Dispose();
    }

    public void DeleteDB(string tableName, int id)
    {
        dbCmd = dbConnection.CreateCommand();

        string sqlQuery = "DELETE FROM " + tableName + " WHERE ID = " + id;
       
        dbCmd.CommandText = sqlQuery;
        dbCmd.ExecuteReader();

        //닫기
        dbCmd.Dispose();
    }

    #endregion

}
