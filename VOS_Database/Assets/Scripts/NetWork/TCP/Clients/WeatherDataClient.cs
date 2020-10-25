// ==============================================================
// Cracked 환경정보를 담는 클라이언트
//
// AUTHOR: Yang SeEun
// CREATED: 2020-04-27
// UPDATED: 2020-06-19
// ==============================================================



using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class WeatherDataClient : TCP_LocalClient
{
    protected override void Parsing(string _str)
    {
        base.Parsing(_str);

        string[] separator = new string[1] { "\r\n" };  //분리할 기준 문자열
        string[] _datas = _str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        string[] datas = _datas[0].Split('^');

        DBManager.Inst.DataEnqueue(new Database.WeatherData(datas));

        //string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
        //string curState = datas[0];
        //float temperatures = Convert.ToSingle(datas[1]);
        //int humidity = Convert.ToInt32(datas[2]);

        ////string -> Enum 변환
        //Database.WeatherConditions state = (Database.WeatherConditions)Enum.Parse(typeof(Database.WeatherConditions), curState);

        //DBManager.Inst.DataEnqueue(new Database.WeatherData(time, state, temperatures, humidity));

    }
}
