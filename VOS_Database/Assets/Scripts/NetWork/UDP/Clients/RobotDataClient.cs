using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDataClient : UDP_Client
{
    protected override void Parsing(string _str)
    {
        base.Parsing(_str);

        string[] separator = new string[1] { "\r\n" };  //분리할 기준 문자열
        string[] _datas = _str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        string[] datas = _datas[0].Split('^');

        DBManager.Inst.DataEnqueue(new Database.RobotData(datas));

        //string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
        //Vector3 position = StringToVector3(datas[0]);
        //Vector3 forwardVector = StringToVector3(datas[1]);

        //DBManager.Inst.DataEnqueue(new Database.RobotData(time, position, forwardVector));

    }
}
