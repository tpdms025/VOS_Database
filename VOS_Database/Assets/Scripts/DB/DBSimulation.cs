using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DBSimulation 
{
    /// <summary>
    /// key : 날짜
    /// value : 테이블
    /// </summary>
    public Dictionary<string, TableTemplate<Database.Simulation>> simulList = new Dictionary<string, TableTemplate<Database.Simulation>>();
   
    public void LoadTable(string fileName, string targetPath)
    {
        string fullpath = System.IO.Path.Combine(targetPath, fileName);
        TableTemplate<Database.Simulation> m_simulationTable = new TableTemplate<Database.Simulation>(fullpath);
        m_simulationTable.Load();

        simulList.Add(fileName, m_simulationTable);
    }

    /*
    public DBSimulation()
    {
        createTableQuerys = new List<string>()
        {
            string.Format(
                     "CREATE TABLE IF NOT EXISTS Simulation(" +
                     "ID    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                     "Time  TEXT," +
                     "CurrentState  TEXT," +
                     "Temperatures  REAL," +
                     "Humidity  INTEGER," +
                     "WindDirection TEXT," +
                     "WindSpeed REAL," +
                     "WaveHeight    REAL," +
                     "WaveSpeed REAL," +
                     "Position  TEXT," +
                     "ForwardVector TEXT," +
                     "Distance  REAL," +
                     "Depth REAL," +
                     "Direction TEXT);")
        };
    }

    protected override string GetInsertQuery(int dataType = 0)
    {
        Database.WeatherData weatherData = DBManager.Inst.getWeatherData();
        Database.WindData windData = DBManager.Inst.getWindData();
        Database.WaveData waveData = DBManager.Inst.getwaveData();
        Database.RobotData robotData = DBManager.Inst.getRobotData();
        Database.SonarData sonarData = DBManager.Inst.getSonarData();

        string sqlQuery = string.Format("INSERT INTO Simulation (" +
           "Time,CurrentState,Temperatures,Humidity," +
            "WindDirection,WindSpeed," +
            "WaveHeight,WaveSpeed," +
            "Position,ForwardVector," +
            "Distance,Depth,Direction) " +
            "VALUES('{0}','{1}',{2},{3}," +
            "'{4}',{5}," +
            "{6},{7}," +
            "'{8}','{9}'," +
            "{10},{11},'{12}')",
             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt"), weatherData.currentState, weatherData.temperatures, weatherData.humidity,
             windData.windDirection, windData.windSpeed,
             waveData.waveHeight, waveData.waveSpeed,
             robotData.position, robotData.forwardVector,
             sonarData.distance, sonarData.depth, sonarData.direction);

        return sqlQuery;
    }
    */
}
