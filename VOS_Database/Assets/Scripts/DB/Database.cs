// ==============================================================
// Database
//
// AUTHOR: Yang SeEun
// CREATED: 2020-06-09
// UPDATED: 2020-10-28
// ==============================================================


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
        public readonly string date;
        public readonly string time;
        public readonly WeatherConditions currentState;      //날씨상태
        public readonly float temperatures;                 //기온
        public readonly int humidity;                        //습도

        public WeatherData()
        {
        }
        public WeatherData(string _date, string _time, WeatherConditions _currentState, float _temperatures, int _humi)
        {
            date = _date;
            time = _time;
            currentState = _currentState;
            temperatures = _temperatures;
            humidity = _humi;
        }
        public WeatherData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
            this.date = inputData[count++];
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
        public readonly string date;
        public readonly string time;
        public readonly JVector3 windDirection;           //풍향
        public readonly float windSpeed;                //풍속

        public WindData()
        {
        }
        public WindData(string _date, string _time, Vector3 _windDirection, float _windSpeed)
        {
            date = _date;
            time = _time;
            windDirection = new JVector3(_windDirection);
            windSpeed = _windSpeed;
        }
        public WindData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
            this.date = inputData[count++];
            this.time = inputData[count++];
            this.windDirection = new JVector3(Database.StringToVector3(inputData[count++]));
            this.windSpeed = Convert.ToSingle(inputData[count++]);
        }
    }

    [Serializable]
    public class ShipData
    {
        public readonly int nID;
        public readonly string date;
        public readonly string time;
        public readonly int latitude;      //위도
        public readonly int longitude;     //경도
        public readonly int altitude;     //고도
        public readonly short heave;
        public readonly short northVelocity;
        public readonly short eastVelocity;
        public readonly short downvelocity;
        public readonly short roll;
        public readonly short pitch;
        public readonly ushort heading;
        public readonly short rollRate;
        public readonly short pitchRate;
        public readonly short headingRate;


        public ShipData()
        {
        }
        public ShipData(string _date, string _time, int _latitude, int _longitude, int _altitude, short _heave,
            short _northVelocity, short _eastVelocity, short _downvelocity, short _roll, short _pitch, ushort _heading,
            short _roll_rate, short _pitch_rate, short _heading_rate)
        {
            date = _date;
            time = _time;
            latitude = _latitude;
            longitude = _longitude;
            altitude = _altitude;
            heave = _heave;
            northVelocity = _northVelocity;
            eastVelocity = _eastVelocity;
            downvelocity = _downvelocity;
            roll = _roll;
            pitch = _pitch;
            heading = _heading;
            rollRate = _roll_rate;
            pitchRate = _pitch_rate;
            headingRate = _heading_rate;
        }

        public ShipData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;

            this.date = inputData[count++];
            this.time = inputData[count++];
            this.latitude = Convert.ToInt32(inputData[count++]);
            this.longitude = Convert.ToInt32(inputData[count++]);
            this.altitude = Convert.ToInt32(inputData[count++]);
            this.heave = Convert.ToInt16(inputData[count++]);
            this.northVelocity = Convert.ToInt16(inputData[count++]);
            this.eastVelocity = Convert.ToInt16(inputData[count++]);
            this.downvelocity = Convert.ToInt16(inputData[count++]);
            this.roll = Convert.ToInt16(inputData[count++]);
            this.pitch = Convert.ToInt16(inputData[count++]);
            this.heading = Convert.ToUInt16(inputData[count++]);
            this.rollRate = Convert.ToInt16(inputData[count++]);
            this.pitchRate = Convert.ToInt16(inputData[count++]);
            this.headingRate = Convert.ToInt16(inputData[count++]);
        }
    }

    [Serializable]
    public class WaveData
    {
        public readonly int nID;
        public readonly string date;
        public readonly string time;
        public readonly float waveHeight;                  //파고
        public readonly float waveSpeed;                  //파속

        public WaveData()
        {
        }
        public WaveData(string _date, string _time, float _waveHeight, float _waveSpeed)
        {
            date = _date;
            time = _time;
            waveHeight = _waveHeight;
            waveSpeed = _waveSpeed;
        }
        public WaveData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
            this.date = inputData[count++];
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
        public readonly string date;
        public readonly string time;
        public List<Pipe> pipes;

        public PipeSensorData()
        {
        }
        public PipeSensorData(string _date, string _time, List<Pipe> _pipes)
        {
            date = _date;
            time = _time;
            pipes = _pipes;

        }

    }

    [Serializable]
    public class RobotData
    {
        public readonly int nID;
        public readonly string date;
        public readonly string time;
        public readonly JVector3 position;
        public readonly JVector3 forwardVector;


        public RobotData()
        {
        }
        public RobotData(string _date, string _time, Vector3 _Pos, Vector3 _forwardVector)
        {
            date = _date;
            time = _time;
            position = new JVector3(_Pos);
            forwardVector = new JVector3(_forwardVector);
        }
        public RobotData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
            this.date = inputData[count++];
            this.time = inputData[count++];
            this.position = new JVector3(Database.StringToVector3(inputData[count++]));
            this.forwardVector = new JVector3(Database.StringToVector3(inputData[count++]));
        }
    }

    [Serializable]
    public class SonarData
    {
        public readonly int nID;
        public readonly string date;
        public readonly string time;
        public readonly float distance;
        public readonly float depth;
        public readonly JVector3 direction;

        public SonarData()
        {
        }
        public SonarData(string _date, string _time, float _distance, float _depth, Vector3 _direction)
        {
            date = _date;
            time = _time;
            distance = _distance;
            depth = _depth;
            direction = new JVector3(_direction);
        }
        public SonarData(string[] inputData, bool includeID = false)
        {
            int count = 0;
            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
            this.date = inputData[count++];
            this.time = inputData[count++];
            this.distance = Convert.ToSingle(inputData[count++]);
            this.depth = Convert.ToSingle(inputData[count++]);
            this.direction = new JVector3(Database.StringToVector3(inputData[count++]));
        }
    }

    [Serializable]
    public class Simulation
    {
        public readonly int nID;
        public readonly string date;
        public readonly string time;

        public readonly WeatherData weather;
        public readonly WindData wind;
        public readonly ShipData ship;
        public readonly WaveData wave;
        public readonly RobotData robot;
        public readonly SonarData sonar;


        public Simulation()
        {

        }

        public Simulation(WeatherData _weather,WindData _wind,ShipData _ship,
            WaveData _wave, RobotData _robot, SonarData _sonar)
        {
            weather = _weather;
            wind = _wind;
            ship = _ship;
            wave = _wave;
            robot = _robot;
            sonar = _sonar;
        }

        public Simulation(string[] inputData)
        {

            int count = 0;
            this.nID = Convert.ToInt32(inputData[count++]);
            this.date = inputData[count++];
            this.time = inputData[count++];

            weather = new WeatherData("", "",
                 (Database.WeatherConditions)Enum.Parse(typeof(Database.WeatherConditions), inputData[count++]),
                 Convert.ToSingle(inputData[count++]),
                 Convert.ToInt32(inputData[count++])
                 );

            wind = new WindData("", "",
                Database.StringToVector3(inputData[count++]),
                Convert.ToSingle(inputData[count++])
                );

            ship = new ShipData("", "",
                Convert.ToInt32(inputData[count++]), Convert.ToInt32(inputData[count++]), Convert.ToInt32(inputData[count++]),
                Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]),
                Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]), Convert.ToUInt16(inputData[count++]), Convert.ToInt16(inputData[count++]),
                Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]));

            wave = new WaveData("", "",
                Convert.ToSingle(inputData[count++]),
                Convert.ToSingle(inputData[count++])
                );

            robot = new RobotData("", "",
                Database.StringToVector3(inputData[count++]), Database.StringToVector3(inputData[count++])
                );

            sonar = new SonarData("", "",
                Convert.ToSingle(inputData[count++]),
                Convert.ToSingle(inputData[count++]),
                Database.StringToVector3(inputData[count++])
                );

        }
    }
}



#region Json 사용전 버전
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[Serializable]
//public class Database
//{
//    public enum WeatherConditions { Sunny, Cloudy, Rain, Snow };
//    public static Vector3 StringToVector3(string sVector)
//    {
//        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
//        {
//            sVector = sVector.Substring(1, sVector.Length - 2);
//        }

//        string[] sArray = sVector.Split(',');

//        Vector3 result = new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]));

//        return result;
//    }


//    [Serializable]
//    public class WeatherData
//    {
//        public readonly int nID;
//        public readonly string date;
//        public readonly string time;
//        public readonly WeatherConditions currentState;      //날씨상태
//        public readonly float temperatures;                 //기온
//        public readonly int humidity;                        //습도

//        public WeatherData()
//        {
//        }
//        public WeatherData(string _date, string _time, WeatherConditions _currentState, float _temperatures, int _humi)
//        {
//            date = _date;
//            time = _time;
//            currentState = _currentState;
//            temperatures = _temperatures;
//            humidity = _humi;
//        }
//        public WeatherData(string[] inputData, bool includeID = false)
//        {
//            int count = 0;
//            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
//            this.date = inputData[count++];
//            this.time = inputData[count++];
//            this.currentState = (Database.WeatherConditions)Enum.Parse(typeof(Database.WeatherConditions), inputData[count++]);
//            this.temperatures = Convert.ToSingle(inputData[count++]);
//            this.humidity = Convert.ToInt32(inputData[count++]);
//        }
//    }

//    [Serializable]
//    public class WindData
//    {
//        public readonly int nID;
//        public readonly string date;
//        public readonly string time;
//        public readonly Vector3 windDirection;           //풍향
//        public readonly float windSpeed;                //풍속

//        public WindData()
//        {
//        }
//        public WindData(string _date, string _time, Vector3 _windDirection, float _windSpeed)
//        {
//            date = _date;
//            time = _time;
//            windDirection = _windDirection;
//            windSpeed = _windSpeed;
//        }
//        public WindData(string[] inputData, bool includeID = false)
//        {
//            int count = 0;
//            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
//            this.date = inputData[count++];
//            this.time = inputData[count++];
//            this.windDirection = Database.StringToVector3(inputData[count++]);
//            this.windSpeed = Convert.ToSingle(inputData[count++]);
//        }
//    }

//    [Serializable]
//    public class ShipData
//    {
//        public readonly int nID;
//        public readonly string date;
//        public readonly string time;
//        public readonly int latitude;      //위도
//        public readonly int longitude;     //경도
//        public readonly int altitude;     //고도
//        public readonly short heave;
//        public readonly short northVelocity;
//        public readonly short eastVelocity;
//        public readonly short downvelocity;
//        public readonly short roll;
//        public readonly short pitch;
//        public readonly ushort heading;
//        public readonly short rollRate;
//        public readonly short pitchRate;
//        public readonly short headingRate;


//        public ShipData()
//        {
//        }
//        public ShipData(string _date, string _time, int _latitude, int _longitude, int _altitude, short _heave,
//            short _northVelocity, short _eastVelocity, short _downvelocity, short _roll, short _pitch, ushort _heading,
//            short _roll_rate, short _pitch_rate, short _heading_rate)
//        {
//            date = _date;
//            time = _time;
//            latitude = _latitude;
//            longitude = _longitude;
//            altitude = _altitude;
//            heave = _heave;
//            northVelocity = _northVelocity;
//            eastVelocity = _eastVelocity;
//            downvelocity = _downvelocity;
//            roll = _roll;
//            pitch = _pitch;
//            heading = _heading;
//            rollRate = _roll_rate;
//            pitchRate = _pitch_rate;
//            headingRate = _heading_rate;
//        }

//        public ShipData(string[] inputData, bool includeID = false)
//        {
//            int count = 0;
//            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;

//            this.date = inputData[count++];
//            this.time = inputData[count++];
//            this.latitude = Convert.ToInt32(inputData[count++]);
//            this.longitude = Convert.ToInt32(inputData[count++]);
//            this.altitude = Convert.ToInt32(inputData[count++]);
//            this.heave = Convert.ToInt16(inputData[count++]);
//            this.northVelocity = Convert.ToInt16(inputData[count++]);
//            this.eastVelocity = Convert.ToInt16(inputData[count++]);
//            this.downvelocity = Convert.ToInt16(inputData[count++]);
//            this.roll = Convert.ToInt16(inputData[count++]);
//            this.pitch = Convert.ToInt16(inputData[count++]);
//            this.heading = Convert.ToUInt16(inputData[count++]);
//            this.rollRate = Convert.ToInt16(inputData[count++]);
//            this.pitchRate = Convert.ToInt16(inputData[count++]);
//            this.headingRate = Convert.ToInt16(inputData[count++]);
//        }
//    }

//    [Serializable]
//    public class WaveData
//    {
//        public readonly int nID;
//        public readonly string date;
//        public readonly string time;
//        public readonly float waveHeight;                  //파고
//        public readonly float waveSpeed;                  //파속

//        public WaveData()
//        {
//        }
//        public WaveData(string _date, string _time, float _waveHeight, float _waveSpeed)
//        {
//            date = _date;
//            time = _time;
//            waveHeight = _waveHeight;
//            waveSpeed = _waveSpeed;
//        }
//        public WaveData(string[] inputData, bool includeID = false)
//        {
//            int count = 0;
//            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
//            this.date = inputData[count++];
//            this.time = inputData[count++];
//            this.waveHeight = Convert.ToSingle(inputData[count++]);
//            this.waveSpeed = Convert.ToSingle(inputData[count++]);
//        }
//    }
//    [Serializable]
//    public class PipeSensorData
//    {
//        public struct Pipe
//        {
//            public Vector3 currentSet;             //유향
//            public float currentRate;              //유속
//        }
//        public readonly string date;
//        public readonly string time;
//        public List<Pipe> pipes;

//        public PipeSensorData()
//        {
//        }
//        public PipeSensorData(string _date, string _time, List<Pipe> _pipes)
//        {
//            date = _date;
//            time = _time;
//            pipes = _pipes;

//        }

//    }

//    [Serializable]
//    public class RobotData
//    {
//        public readonly int nID;
//        public readonly string date;
//        public readonly string time;
//        public readonly Vector3 position;
//        public readonly Vector3 forwardVector;


//        public RobotData()
//        {
//        }
//        public RobotData(string _date, string _time, Vector3 _Pos, Vector3 _forwardVector)
//        {
//            date = _date;
//            time = _time;
//            position = _Pos;
//            forwardVector = _forwardVector;
//        }
//        public RobotData(string[] inputData, bool includeID = false)
//        {
//            int count = 0;
//            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
//            this.date = inputData[count++];
//            this.time = inputData[count++];
//            this.position = Database.StringToVector3(inputData[count++]);
//            this.forwardVector = Database.StringToVector3(inputData[count++]);
//        }
//    }

//    [Serializable]
//    public class SonarData
//    {
//        public readonly int nID;
//        public readonly string date;
//        public readonly string time;
//        public readonly float distance;
//        public readonly float depth;
//        public readonly Vector3 direction;

//        public SonarData()
//        {
//        }
//        public SonarData(string _date, string _time, float _distance, float _depth, Vector3 _direction)
//        {
//            date = _date;
//            time = _time;
//            distance = _distance;
//            depth = _depth;
//            direction = _direction;
//        }
//        public SonarData(string[] inputData, bool includeID = false)
//        {
//            int count = 0;
//            this.nID = (includeID == true) ? Convert.ToInt32(inputData[count++]) : 0;
//            this.date = inputData[count++];
//            this.time = inputData[count++];
//            this.distance = Convert.ToSingle(inputData[count++]);
//            this.depth = Convert.ToSingle(inputData[count++]);
//            this.direction = Database.StringToVector3(inputData[count++]);
//        }
//    }

//    [Serializable]
//    public class Simulation
//    {
//        public readonly int nID;
//        public readonly string date;
//        public readonly string time;

//        public readonly WeatherData weather;
//        public readonly WindData wind;
//        public readonly ShipData ship;
//        public readonly WaveData wave;
//        public readonly RobotData robot;
//        public readonly SonarData sonar;


//        public Simulation()
//        {

//        }

//        public Simulation(WeatherData _weather, WindData _wind, ShipData _ship,
//            WaveData _wave, RobotData _robot, SonarData _sonar)
//        {
//            weather = _weather;
//            wind = _wind;
//            ship = _ship;
//            wave = _wave;
//            robot = _robot;
//            sonar = _sonar;
//        }

//        public Simulation(string[] inputData)
//        {

//            int count = 0;
//            this.nID = Convert.ToInt32(inputData[count++]);
//            this.date = inputData[count++];
//            this.time = inputData[count++];

//            weather = new WeatherData("", "",
//                 (Database.WeatherConditions)Enum.Parse(typeof(Database.WeatherConditions), inputData[count++]),
//                 Convert.ToSingle(inputData[count++]),
//                 Convert.ToInt32(inputData[count++])
//                 );

//            wind = new WindData("", "",
//                Database.StringToVector3(inputData[count++]),
//                Convert.ToSingle(inputData[count++])
//                );

//            ship = new ShipData("", "",
//                Convert.ToInt32(inputData[count++]), Convert.ToInt32(inputData[count++]), Convert.ToInt32(inputData[count++]),
//                Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]),
//                Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]), Convert.ToUInt16(inputData[count++]), Convert.ToInt16(inputData[count++]),
//                Convert.ToInt16(inputData[count++]), Convert.ToInt16(inputData[count++]));

//            wave = new WaveData("", "",
//                Convert.ToSingle(inputData[count++]),
//                Convert.ToSingle(inputData[count++])
//                );

//            robot = new RobotData("", "",
//                Database.StringToVector3(inputData[count++]), Database.StringToVector3(inputData[count++])
//                );

//            sonar = new SonarData("", "",
//                Convert.ToSingle(inputData[count++]),
//                Convert.ToSingle(inputData[count++]),
//                Database.StringToVector3(inputData[count++])
//                );

//        }
//    }
//}

#endregion