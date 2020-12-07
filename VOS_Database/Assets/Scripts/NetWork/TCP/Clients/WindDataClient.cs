﻿// ==============================================================
// 바람정보를 담는 클라이언트
//
// AUTHOR: Yang SeEun
// CREATED: 2020-04-27
// UPDATED: 2020-06-19
// ==============================================================



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDataClient : TCP_LocalClient
{
    protected override void Parsing(string _str)
    {
        base.Parsing(_str);

        string[] separator = new string[1] { "\r\n" };  //분리할 기준 문자열
        string[] _datas = _str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        string[] datas = _datas[0].Split('^');


        //Josn Version (스크립트 이름 변경 해야함. DBManager_ver1 ->DBManager)
        //DBManager.Inst.DataEnqueue(new Database.WindData(datas));
        //DBManager.Inst.SetWindData(new Database.WindData(datas));

        //Sqlite Version
        DBManager.Inst.DataEnqueue(new Database.WindData(datas));
    }
}
