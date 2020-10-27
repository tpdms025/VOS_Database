using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBTimestamp : DBBasic
{
    public DBTimestamp()
    {
        createTableQuerys = new List<string>()
        {
            string.Format(
                     "CREATE TABLE IF NOT EXISTS Timestamp(" +
                     "ID    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                     "Date  TEXT," +
                     "Time  TEXT," +
                     "CurrentState  TEXT," +
                     "Temperatures  REAL," +
                     "Humidity  INTEGER," +

                     "WindDirection TEXT," +
                     "WindSpeed REAL," +

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
                     "HeadingRate  INTEGER,"+

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
        Database.ShipData shipData = DBManager.Inst.getShipData();
        Database.WaveData waveData = DBManager.Inst.getwaveData();
        Database.RobotData robotData = DBManager.Inst.getRobotData();
        Database.SonarData sonarData = DBManager.Inst.getSonarData();

        string sqlQuery = "INSERT INTO Timestamp (" +
            "Date,Time,CurrentState,Temperatures,Humidity," +
            "WindDirection,WindSpeed," +
            "WaveHeight,WaveSpeed," +
            "Latitude,Longitude,Altitude,Heave,NorthVelocity,EastVelocity,Downvelocity,Roll,Pitch,Heading,RollRate,PitchRate,HeadingRate," +
            "Position,ForwardVector," +
            "Distance,Depth,Direction) " +
            string.Format("VALUES('{0}','{1}','{2}',{3},{4},", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss tt"), weatherData.currentState, weatherData.temperatures, weatherData.humidity) +
            string.Format("'{0}',{1},", windData.windDirection, windData.windSpeed) +
            string.Format("{0},{1},", waveData.waveHeight, waveData.waveSpeed) +
            string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},", shipData.latitude, shipData.longitude, shipData.altitude, shipData.heave, 
            shipData.northVelocity, shipData.eastVelocity, shipData.downvelocity, shipData.roll, shipData.pitch, shipData.heading, 
            shipData.rollRate, shipData.pitchRate, shipData.headingRate) +
            string.Format("'{0}','{1}',", robotData.position, robotData.forwardVector) +
            string.Format("{0},{1},'{2}')", sonarData.distance, sonarData.depth, sonarData.direction);
             
            

        return sqlQuery;
    }


}
