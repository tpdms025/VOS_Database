using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database
{
    public enum WeatherConditions { Sunny, Cloudy, Rain, Snow };
    public static Vector3 StringToVector3(string sVector)
    {
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        string[] sArray = sVector.Split(',');

        Vector3 result = new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]));

        return result;
    }


    [Serializable]
    public class WeatherData
    {
        public readonly int nID;
        public readonly string time;
        public readonly WeatherConditions currentState;      //날씨상태
        public readonly float temperatures;                 //기온
        public readonly int humidity;                        //습도

        public WeatherData()
        {
            time = string.Empty;
            currentState = WeatherConditions.Cloudy;
            temperatures = 27.4f;
            humidity = 50;
        }
        public WeatherData(string _time, WeatherConditions _currentState, float _temperatures, int _humi)
        {
            time = _time;
            currentState = _currentState;
            temperatures = _temperatures;
            humidity = _humi;
        }
        public WeatherData(string[] inputData , bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0 ;
            this.time = inputData[count++];
            this.currentState = (Database.WeatherConditions)Enum.Parse(typeof(Database.WeatherConditions), inputData[count++]);
            this.temperatures = Convert.ToSingle(inputData[count++]);
            this.humidity = Convert.ToInt32(inputData[count++]);
        }
    }
    [Serializable]
    public class WindData
    {
        public readonly int nID;
        public string time;
        public Vector3 windDirection;           //풍향
        public float windSpeed;                //풍속
        public WindData()
        {
            time = string.Empty;
            windDirection = Vector3.zero;
            windSpeed = 0.0f;
        }
        public WindData(string _time, Vector3 _windDirection, float _windSpeed)
        {
            time = _time;
            windDirection = _windDirection;
            windSpeed = _windSpeed;
        }
        public WindData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
            this.time = inputData[count++];
            this.windDirection = Database.StringToVector3(inputData[count++]);
            this.windSpeed = Convert.ToSingle(inputData[count++]);
        }
    }


    //중요한 데이터들
    [Serializable]
    public class WaveData
    {
        public readonly int nID;
        public string time;
        public float waveHeight;                  //파고
        public float waveSpeed;                  //파속
        public WaveData()
        {
            time = string.Empty;
            waveHeight = 1.0f;
            waveSpeed = 1.0f;


        }
        public WaveData(string _time, float _waveHeight, float _waveSpeed)
        {
            time = _time;
            waveHeight = _waveHeight;
            waveSpeed = _waveSpeed;
        }
        public WaveData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
            this.time = inputData[count++];
            this.waveHeight = Convert.ToSingle(inputData[count++]);
            this.waveSpeed = Convert.ToSingle(inputData[count++]);
        }
    }
    [Serializable]
    public class PipeSensorData
    {
        public struct Pipe
        {
            public Vector3 currentSet;             //유향
            public float currentRate;              //유속
        }
        public string time;
        public List<Pipe> pipes;

        public PipeSensorData()
        {
            time = string.Empty;
        }
        public PipeSensorData(string _time, List<Pipe> _pipes)
        {
            time = _time;
            pipes = _pipes;

        }

    }

    [Serializable]
    public class RobotData
    {
        public readonly int nID;
        public string time;
        public Vector3 position;
        public Vector3 forwardVector;


        public RobotData()
        {
            time = string.Empty;
            position = Vector3.zero;
            forwardVector = Vector3.zero;
        }
        public RobotData(string _time, Vector3 _Pos, Vector3 _forwardVector)
        {
            time = _time;
            position = _Pos;
            forwardVector = _forwardVector;
        }
        public RobotData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
            this.time = inputData[count++];
            this.position = Database.StringToVector3(inputData[count++]);
            this.forwardVector = Database.StringToVector3(inputData[count++]);
        }
    }

    [Serializable]
    public class SonarData
    {
        public readonly int nID;
        public string time;
        public float distance;
        public float depth;
        public Vector3 direction;

        public SonarData()
        {
            time = string.Empty;
            distance = 0.0f;
            depth = 0.0f;
            direction = Vector3.zero;
        }
        public SonarData(string _time, float _distance, float _depth, Vector3 _direction)
        {
            time = _time;
            distance = _distance;
            depth = _depth;
            direction = _direction;
        }
        public SonarData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
            this.time = inputData[count++];
            this.distance = Convert.ToSingle(inputData[count++]);
            this.depth = Convert.ToSingle(inputData[count++]);
            this.direction = Database.StringToVector3(inputData[count++]);
        }
    }

    [Serializable]
    public class Simulation
    {
        public readonly int nID;
        public readonly string time;

        //WeatherData
        public readonly WeatherConditions currentState;      //날씨상태
        public readonly float temperatures;                 //기온
        public readonly int humidity;                        //습도
        //WindData
        public Vector3 windDirection;           //풍향
        public float windSpeed;                //풍속
        //WaveData
        public float waveHeight;                  //파고
        public float waveSpeed;                  //파속
        //robotData
        public Vector3 position;
        public Vector3 forwardVector;
        //SonarData
        public float distance;
        public float depth;
        public Vector3 direction;

        public Simulation(string[] inputData)
        {
            int count = 0;
            this.nID = Convert.ToInt32(inputData[count++]);
            this.time = inputData[count++];
            this.currentState = (Database.WeatherConditions)Enum.Parse(typeof(Database.WeatherConditions), inputData[count++]);
            this.temperatures = Convert.ToSingle(inputData[count++]);
            this.humidity = Convert.ToInt32(inputData[count++]);
            this.windDirection = Database.StringToVector3(inputData[count++]);
            this.windSpeed = Convert.ToSingle(inputData[count++]);
            this.waveHeight = Convert.ToSingle(inputData[count++]);
            this.waveSpeed = Convert.ToSingle(inputData[count++]);
            this.position = Database.StringToVector3(inputData[count++]);
            this.forwardVector = Database.StringToVector3(inputData[count++]);
            this.distance = Convert.ToSingle(inputData[count++]);
            this.depth = Convert.ToSingle(inputData[count++]);
            this.direction = Database.StringToVector3(inputData[count++]);
        }
    }
}
