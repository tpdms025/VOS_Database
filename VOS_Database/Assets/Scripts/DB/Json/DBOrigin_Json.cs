using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DBOrigin_Json : DBBasic_Json
{
    public Database.WeatherData weatherData = new Database.WeatherData();
    public Database.WindData windData = new Database.WindData();
    public Database.ShipData shipData = new Database.ShipData();
    public Database.WaveData waveData = new Database.WaveData();
    public Database.PipeSensorData pipeSensorData = new Database.PipeSensorData();
    public Database.RobotData robotData = new Database.RobotData();
    public Database.SonarData sonarData = new Database.SonarData();

    public Queue<Database.WeatherData> weatherDataQueue = new Queue<Database.WeatherData>();
    public Queue<Database.WindData> windDataQueue = new Queue<Database.WindData>();
    public Queue<Database.ShipData> shipDataQueue = new Queue<Database.ShipData>();
    public Queue<Database.WaveData> waveDataQueue = new Queue<Database.WaveData>();
    public Queue<Database.PipeSensorData> pipeSensorDataQueue = new Queue<Database.PipeSensorData>();
    public Queue<Database.RobotData> robotDataQueue = new Queue<Database.RobotData>();
    public Queue<Database.SonarData> sonarDataQueue = new Queue<Database.SonarData>();

    /// <summary>
    /// 데이터 삽입을 관리하는 메소드
    /// </summary>
    public void DBInsertThread()
    {
        while (DBManager_vr1.Inst.applicationQuit.Equals(false))
        {

            if (!saving)
            {
                isInsertEnd = true;

                if (weatherDataQueue.Count > 0)
                {
                    InsertDB(weatherDataQueue);
                    weatherDataQueue.Clear();
                }
                if (windDataQueue.Count > 0)
                {
                    InsertDB(windDataQueue);
                    windDataQueue.Clear();
                }
                if (shipDataQueue.Count > 0)
                {
                    InsertDB(shipDataQueue);
                    shipDataQueue.Clear();
                }
                if (waveDataQueue.Count > 0)
                {
                    InsertDB(waveDataQueue);
                    waveDataQueue.Clear();
                }
                if (pipeSensorDataQueue.Count > 0)
                {
                    InsertDB(pipeSensorDataQueue);
                    pipeSensorDataQueue.Clear();
                }
                if (robotDataQueue.Count > 0)
                {
                    InsertDB(robotDataQueue);
                    robotDataQueue.Clear();
                }
                if (sonarDataQueue.Count > 0)
                {
                    InsertDB(sonarDataQueue);
                    sonarDataQueue.Clear();
                }
                isInsertEnd = false;
            }
            Thread.Sleep(1000);
        }
#if UNITY_EDITOR
        Debug.Log("DB Insert Thread End");
#endif
    }
}
