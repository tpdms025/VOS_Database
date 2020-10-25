// ==============================================================
// DB Manager
//
// AUTHOR: Yang SeEun
// CREATED: 2020-06-18
// UPDATED: 2020-06-29
// ==============================================================

using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Threading;
using System.Data;

public enum DataType { WeatherData, WindData , WaveData , PipeSensorData, RobotData , SonarData, All =30 };

public class DBManager : MonoBehaviour
{
    private DBOrigin originData = new DBOrigin();
    private DBTimestamp timestampData = new DBTimestamp();
    private DBSimulation simulationData = new DBSimulation();

    private Thread mainThread;
    readonly public string actualPath = Application.streamingAssetsPath + "/";

    //데이터 get set 편의함수
    #region Database_GetData_Method
    public Database.WeatherData getWeatherData()
    {
        return originData.weatherData;
        //return weatherData;
    }
    public Database.WindData getWindData()
    {
        return originData.windData;
        //return windData;
    }
    public Database.WaveData getwaveData()
    {
        return originData.waveData;
        //return waveData;
    }
    public Database.PipeSensorData getPipeSensorData()
    {
        return originData.pipeSensorData;
        //return pipeSensorData;
    }
    public Database.RobotData getRobotData()
    {
        return originData.robotData;
        //return robotData;
    }
    public Database.SonarData getSonarData()
    {
        return originData.sonarData;
        //return sonarData;
    }
    #endregion

    #region Database Queue Enqueue Method
    public void DataEnqueue(Database.WeatherData data)
    {
        originData.weatherDataQueue.Enqueue(data);
        //weatherDataQueue.Enqueue(data);
    }
    public void DataEnqueue(Database.WindData data)
    {
        originData.windDataQueue.Enqueue(data);
        //windDataQueue.Enqueue(data);
    }
    public void DataEnqueue(Database.WaveData data)
    {
        originData.waveDataQueue.Enqueue(data);
        //waveDataQueue.Enqueue(data);
    }
    public void DataEnqueue(Database.PipeSensorData data)
    {
        originData.pipeSensorDataQueue.Enqueue(data);
        //pipeSensorDataQueue.Enqueue(data);
    }
    public void DataEnqueue(Database.RobotData data)
    {
        originData.robotDataQueue.Enqueue(data);
        //robotDataQueue.Enqueue(data);
    }
    public void DataEnqueue(Database.SonarData data)
    {
        originData.sonarDataQueue.Enqueue(data);
        //sonarDataQueue.Enqueue(data);
    }

    #endregion


    /// <summary>
    /// 현재 타임스탬프 데이터를 DB에 삽입한다.
    /// </summary>
    public void Set_DBTimestamp()
    {
        timestampData.InsertDB();
    }


    /// <summary>
    /// 시뮬레이션 테이블 정보들
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, TableTemplate<Database.Simulation>> SetSimulationList()
    {
        return simulationData.simulList;
    }

    #region FileName List

    /// <summary>
    /// timestamp 폴더내 파일이름들을 가져온다. (파일 확장자명도 포함)
    /// </summary>
    /// <returns></returns>
    public List<string> Get_FileNameList_Timestamp()
    {
        List<string> fileNames = new List<string>();

        DirectoryInfo directory = new DirectoryInfo(actualPath + "InternalData/Timestamp");

        foreach (var di in directory.GetFiles("*.sqlite"))
        {
            fileNames.Add(di.Name);
        }
        return fileNames;
    }

    /// <summary>
    /// 이름 순서으로 시뮬레이션 파일의 이름들을 가져온다.
    /// </summary>
    /// <returns></returns>
    public List<string> Get_FileNameList_Simulation_namesort()
    {
        List<string> fileNames = new List<string>();
        DirectoryInfo directory = new DirectoryInfo(actualPath + "SimulationData");

   
        FileInfo[] fi = new string[] { "*.txt", "*.csv" }
          .SelectMany(i => directory.GetFiles(i, SearchOption.AllDirectories))
          .ToArray();

        foreach(var v in fi)
        {
            Debug.Log(v.Name);
        }
        #region 이전버전
        //foreach (var di in directory.GetFiles("*.txt",SearchOption.AllDirectories))
        //{
        //    fileNames.Add(di.Name);
        //}
        #endregion

        return fileNames;
    }

    /// <summary>
    /// 추가한 순서로 시뮬레이션 파일의 이름들을 가져온다.
    /// </summary>
    /// <returns></returns>
    public List<string> Get_FileNameList_Simulation()
    {
        List<string> fileNames = new List<string>();

        foreach (KeyValuePair<string, TableTemplate<Database.Simulation>> item in SetSimulationList())
        {
            fileNames.Add(item.Key);
        }

        return fileNames;
    }

    #endregion


    /// <summary>
    /// Timestamp.sqlite 파일을 csv파일로 변환한다.
    /// (Export 버튼 클릭시 사용될 함수)
    /// </summary>
    /// <param name="_fileName">파일이름(확장자명도 포함)</param>
    /// <returns></returns>
    public string Export_DBTimestamp(string _fileName)
    {
        return FileToCSV(actualPath + "InternalData/Timestamp/" + _fileName, "Timestamp");
    }

    /// <summary>
    /// 시뮬레이션 파일을 복사하고 테이블을 로드한다.
    /// (Import 버튼 클릭시 사용될 함수)
    /// </summary>
    /// <param name="sourcePath"></param>
    public void LoadSimulationTable(string sourcePath)
    {
        string _fileName = new FileInfo(sourcePath).Name;
        string targetPath = actualPath + "SimulationData";

        //복사
        FileCopy(sourcePath, targetPath);

        //테이블 로드
        simulationData.LoadTable(_fileName, targetPath);
    }


    /// <summary>
    /// 파일을 복사한다.
    /// </summary>
    /// <param name="sourcePath">복사할 파일의 경로 (확장자명까지)</param>
    public void FileCopy(string sourcePath, string targetPath)
    {
        if(File.Exists(sourcePath))
        {
            string fileName = new FileInfo(sourcePath).Name;
            
            //디렉토리가 없다면
            if (!File.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            string destFile = System.IO.Path.Combine(targetPath, fileName);

            System.IO.File.Copy(sourcePath, destFile, true);
        }
    }

    #region FileToCSV

    // ^ -> 콤마로 변경할 예정 (차후 데이터 들어오는거 보고)
    private readonly string separator = "^";

  
    /// <summary>
    ///  sqlite 파일을 csv 파일로 변환한다.
    /// </summary>
    /// <param name="_filepath"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    private string FileToCSV(string _filepath, string tableName)
    {
        var connectionString = @"URI=file:" + _filepath;
        var selectQuery = "SELECT * FROM " + tableName;

        var table = ReadTable(connectionString, selectQuery);
        string csvData = WriteToFile(table, false, separator);

        return csvData;
    }
    private DataTable ReadTable(string connectionString, string selectQuery)
    {
        var returnValue = new DataTable();

        var conn = new SqliteConnection(connectionString);

        try
        {
            conn.Open();
            var command = new SqliteCommand(selectQuery, conn);

            using (var adapter = new SqliteDataAdapter(command))
            {
                adapter.Fill(returnValue);
            }

        }
        catch (System.Exception ex)
        {
#if UNITY_EDITOR
            Debug.Log(ex.Message);
#endif
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        return returnValue;
    }
    private string WriteToFile(DataTable dataSource, bool firstRowIsColumnHeader = false, string _seperator = ";")
    {
        string data = string.Empty;

        int icolcount = dataSource.Columns.Count;

        if (!firstRowIsColumnHeader)
        {
            for (int i = 0; i < icolcount; i++)
            {
                data += dataSource.Columns[i];
                if (i < icolcount - 1)
                    data += _seperator;
            }
            data += "\n";
        }

        foreach (DataRow drow in dataSource.Rows)
        {
            for (int i = 0; i < icolcount; i++)
            {
                if (!System.Convert.IsDBNull(drow[i]))
                {
                    data += drow[i].ToString();
                }
                if (i < icolcount - 1)
                {
                    data += _seperator;
                }
            }
            data += "\n";
        }
        return data;
    }

    #region 이전버전 WriteToFile
    private void WriteToFile(DataTable dataSource, string fileOutputPath, bool firstRowIsColumnHeader = false, string seperator = ";")
    {
        var sw = new StreamWriter(fileOutputPath, false);

        int icolcount = dataSource.Columns.Count;

        if (!firstRowIsColumnHeader)
        {
            for (int i = 0; i < icolcount; i++)
            {
                sw.Write(dataSource.Columns[i]);
                if (i < icolcount - 1)
                    sw.Write(seperator);
            }
            sw.Write(sw.NewLine);
        }

        foreach (DataRow drow in dataSource.Rows)
        {
            for (int i = 0; i < icolcount; i++)
            {
                if (!System.Convert.IsDBNull(drow[i]))
                    sw.Write(drow[i].ToString());
                if (i < icolcount - 1)
                    sw.Write(seperator);
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }
    #endregion

    #endregion







    private static DBManager instance;
    public static DBManager Inst { get { return instance; } set { instance = value; } }


    private void Awake()
    {
        if (Inst != null)
        {
            Destroy(gameObject);
            return;
        }
        Inst = this;

        DontDestroyOnLoad(gameObject);

        StartCoroutine(Main());
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Set_DBTimestamp();
            Debug.Log("Timestamp Insert!");
        }
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            Get_FileNameList_Simulation();
        }
    }
#endif

    private IEnumerator Main()
    {
        //CreateFile
        originData.DBCreate("VOS_Data.sqlite");
        timestampData.DBCreate("InternalData/Timestamp/VOS_Timestamp2-1.sqlite");


        //Connect
        originData.DBConnect("VOS_Data.sqlite");
        timestampData.DBConnect("InternalData/Timestamp/VOS_Timestamp2-1.sqlite");


        //CreateTable
        timestampData.CreateAllTable();

        yield return null;

        mainThread = new Thread(originData.DBInsertThread);
        mainThread.Start();

    }

    
    private void OnApplicationQuit()
    {
        originData.DBDisconnect();
        timestampData.DBDisconnect();
    }

}
