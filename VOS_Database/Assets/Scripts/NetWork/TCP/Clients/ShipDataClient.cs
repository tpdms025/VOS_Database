// ==============================================================
// 배 정보를 담는 클라이언트
//
// AUTHOR: Yang SeEun
// CREATED: 2020-10-27
// UPDATED: 2020-10-27
// ==============================================================



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDataClient : TCP_LocalClient
{
    protected override void Parsing(string _str)
    {
        base.Parsing(_str);

        string[] separator = new string[1] { "\r\n" };  //분리할 기준 문자열
        string[] _datas = _str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        string[] datas = _datas[0].Split('^');

        DBManager.Inst.DataEnqueue(new Database.ShipData(datas));

       
    }
}
