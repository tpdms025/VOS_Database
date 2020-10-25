using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDataClient : UDP_Client
{
    protected override void Parsing(string _str)
    {
        base.Parsing(_str);

        string[] separator = new string[1] { "\r\n" };  //분리할 기준 문자열
        string[] _datas = _str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        string[] datas = _datas[0].Split('^');

        DBManager.Inst.DataEnqueue(new Database.WaveData(datas));

        
        //string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
        //float waveHeight = Convert.ToSingle(datas[0]);
        //float waveSpeed = Convert.ToSingle(datas[1]);

        //DBManager.Inst.DataEnqueue(new Database.WaveData(time, waveHeight, waveSpeed));

    }
}
