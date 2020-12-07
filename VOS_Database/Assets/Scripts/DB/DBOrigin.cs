using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEngine;


public class DBOrigin : DBBasic
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


    public DBOrigin()
    {
        createTableQuerys = new List<string>()
        {
            //WeatherData
            string.Format(
                     "CREATE TABLE IF NOT EXISTS WeatherData(" +
                     "ID    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                     "Date  TEXT," +
                     "Time  TEXT," +
                     "CurrentState  TEXT," +
                     "Temperatures  REAL," +
                     "Humidity  INTEGER);"),
            //WindData
            string.Format(
                    "CREATE TABLE IF NOT EXISTS WindData(" +
                     "ID    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                     "Date  TEXT," +
                     "Time  TEXT," +
                     "WindDirection TEXT," +
                     "WindSpeed REAL);"),
            //ShipData
            string.Format(
                    "CREATE TABLE IF NOT EXISTS ShipData(" +
                    "ID    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                    "Date  TEXT," +
                    "Time  TEXT," +
                    "Latitude  INTEGER," +
                    "Longitude INTEGER," +
                    "Altitude  INTEGER," +
                    "Heave INTEGER," +
                    "NorthVelocity INTEGER," +
                    "EastVelocity  INTEGER," +
                    "Downvelocity  INTEGER," +
                    "Roll  INTEGER," +
                    "Pitch INTEGER," +
                    "Heading   INTEGER," +
                    "RollRate INTEGER," +
                    "PitchRate    INTEGER," +
                    "HeadingRate  INTEGER);"),
            //WaveData
            string.Format(
                    "CREATE TABLE IF NOT EXISTS WaveData(" +
                    "ID    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                     "Date  TEXT," +
                    "Time  TEXT," +
                    "WaveHeight    REAL," +
                    "WaveSpeed REAL);"),
            //RobotData
            string.Format(
                    "CREATE TABLE IF NOT EXISTS RobotData(" +
                    "ID    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                    "Date  TEXT," +
                    "TIme  TEXT," +
                    "Position  TEXT," +
                    "ForwardVector TEXT);"),
            //SonarData
            string.Format(
                    "CREATE TABLE IF NOT EXISTS SonarData(" +
                    "ID    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                     "Date  TEXT," +
                    "Time  TEXT," +
                    "Distance  REAL," +
                    "Depth REAL," +
                    "Direction TEXT);")
        };
    }

    #region DBInsert
    protected override string GetInsertQuery(int dataType = 0)
    {
        string sqlQuery = string.Empty;
        
        switch (dataType)
        {
            case 0:
                sqlQuery = string.Format("INSERT INTO WeatherData (Date,Time,CurrentState,Temperatures,Humidity) VALUES('{0}','{1}','{2}',{3},{4})", weatherData.date,weatherData.time, weatherData.currentState, weatherData.temperatures, weatherData.humidity);
                break;
            case 1:
                sqlQuery = string.Format("INSERT INTO WindData (Date,Time,WindDirection,WindSpeed) VALUES('{0}','{1}','{2}',{3})", windData.date, windData.time, windData.windDirection, windData.windSpeed);
                break;
            case 2:
                sqlQuery = string.Format("INSERT INTO ShipData (Date,Time,Latitude,Longitude,Altitude,Heave,NorthVelocity,EastVelocity,Downvelocity,Roll,Pitch,Heading,RollRate,PitchRate,HeadingRate)" +
                    "VALUES('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14})", shipData.date, shipData.time, shipData.latitude, shipData.longitude, shipData.altitude, shipData.heave, shipData.northVelocity, shipData.eastVelocity, shipData.downvelocity,
                    shipData.roll,shipData.pitch, shipData.heading, shipData.rollRate, shipData.pitchRate, shipData.headingRate);
                break;
            case 3:
                sqlQuery = string.Format("INSERT INTO WaveData (Date,Time,WaveHeight,WaveSpeed) VALUES('{0}','{1}',{2},{3})", waveData.date, waveData.time, waveData.waveHeight, waveData.waveSpeed);
                break;
            case 4:
                //배열을 데이터로 어떤식으로 넣을지 생각해봐야함
                //sqlQuery = "INSERT INTO PipeSensorData (Date,Time,CurrentSet,CurrentRate) VALUES('" + pipeSensorData.time + "'," + pipeSensorData.pipes +")";
                break;
            case 5:
                sqlQuery = string.Format("INSERT INTO RobotData (Date,Time,Position,ForwardVector) VALUES('{0}','{1}','{2}','{3}')", robotData.date, robotData.time, robotData.position, robotData.forwardVector);
                break;
            case 6:
                sqlQuery = string.Format("INSERT INTO SonarData (Date,Time,Distance,Depth,Direction) VALUES('{0}',{1}, {2},'{3}')", sonarData.date, sonarData.time, sonarData.distance, sonarData.depth, sonarData.direction);
                break;
            default:
                break;
        }
        return sqlQuery;
    }

    /// <summary>
    /// 데이터 삽입을 관리하는 메소드
    /// </summary>
    public void DBInsertThread()
    {
        while (dbConnection != null && dbConnection.State != ConnectionState.Closed)
        {
            if (!saving)
            {
                isInsertEnd = true;

                if (weatherDataQueue.Count > 0)
                {
                    for (int i = 0; i < weatherDataQueue.Count; i++)
                    {
                        weatherData = weatherDataQueue.Dequeue();
                        //InsertDB(DataType.WeatherData.GetHashCode());
                    }
                }
                if (windDataQueue.Count > 0)
                {
                    for (int i = 0; i < windDataQueue.Count; i++)
                    {
                        windData = windDataQueue.Dequeue();
                        //InsertDB(DataType.WindData.GetHashCode());
                    }
                }
                if (shipDataQueue.Count > 0)
                {
                    for (int i = 0; i < shipDataQueue.Count; i++)
                    {
                        shipData = shipDataQueue.Dequeue();
                        //InsertDB(DataType.ShipData.GetHashCode());
                    }
                }
                if (waveDataQueue.Count > 0)
                {
                    for (int i = 0; i < waveDataQueue.Count; i++)
                    {
                        waveData = waveDataQueue.Dequeue();
                        //InsertDB(DataType.WaveData.GetHashCode());
                    }
                }
                if (pipeSensorDataQueue.Count > 0)
                {
                    for (int i = 0; i < pipeSensorDataQueue.Count; i++)
                    {
                        pipeSensorData = pipeSensorDataQueue.Dequeue();
                        //InsertDB(DataType.PipeSensorData.GetHashCode());
                    }
                }
                if (robotDataQueue.Count > 0)
                {
                    for (int i = 0; i < robotDataQueue.Count; i++)
                    {
                        robotData = robotDataQueue.Dequeue();
                        //InsertDB(DataType.RobotData.GetHashCode());
                    }
                }
                if (sonarDataQueue.Count > 0)
                {
                    for (int i = 0; i < sonarDataQueue.Count; i++)
                    {
                        sonarData = sonarDataQueue.Dequeue();
                        //InsertDB(DataType.SonarData.GetHashCode());
                    }
                }
                isInsertEnd = false;
            }
            Thread.Sleep(1000);
        }
        Debug.Log("DB Insert Thread End");
    }
    #endregion
}
