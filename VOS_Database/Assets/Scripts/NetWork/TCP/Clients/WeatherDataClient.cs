// ==============================================================
// 환경정보를 담는 클라이언트
//
// AUTHOR: Yang SeEun
// CREATED: 2020-04-27
// UPDATED: 2020-06-19
// ==============================================================



using System;


public class WeatherDataClient : TCP_LocalClient
{
    protected override void Parsing(string _str)
    {
        base.Parsing(_str);

        string[] separator = new string[1] { "\r\n" };  //분리할 기준 문자열
        string[] _datas = _str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        string[] datas = _datas[0].Split('^');


        //Josn Version (스크립트 이름 변경 해야함. DBManager_ver1 ->DBManager)
        //DBManager.Inst.DataEnqueue(new Database.WeatherData(datas));
        //DBManager.Inst.SetWindData(new Database.WeatherData(datas));

        //Sqlite Version
        DBManager.Inst.DataEnqueue(new Database.WeatherData(datas));

    }
}
