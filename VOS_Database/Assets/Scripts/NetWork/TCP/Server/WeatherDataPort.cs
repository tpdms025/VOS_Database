// ==============================================================
// 환경정보를 관리하는 포트
//
// AUTHOR: Yang SeEun
// CREATED: 2020-04-27
// UPDATED: 2020-06-19
// ==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeatherDataPort : TCP_ListenPort
{

    private void Awake()
    {
        portName = "WeatherDataPort";
    }

    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        if ((time >= sendIntervalTime) && clients.Count != 0)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string _time = DateTime.Now.ToString("HH:mm:ss tt");
            int enumCount = Enum.GetValues(typeof(Database.WeatherConditions)).Length;
            Database.WeatherConditions state = (Database.WeatherConditions)UnityEngine.Random.Range(0, enumCount - 1);
            float temperature = UnityEngine.Random.Range(-20.0f, 45.0f);
            int humidity = UnityEngine.Random.Range(0, 100);

            string data = string.Format("{0}^{1}^{2}^{3}^{4}", date,_time, state,temperature ,humidity);
            stringData.Enqueue(data);

            time = 0.0f;
        }
    }

    ////test send
    //public void Send()
    //{
    //    try
    //    {
    //        while (clients[clients.Count - 1].socket.Connected && clients[clients.Count - 1].connecting)
    //        {
    //            if (weatherData.Count != 0)
    //            {
    //                //data
    //                clients[clients.Count - 1].SendData(weatherData[0]);
    //                weatherData.RemoveAt(0);
    //            }

    //            Thread.Sleep(100);
    //        }
    //        clients[clients.Count - 1].connecting = false;
    //        Debug.Log("WeatherDataPort connect End");
    //    }
    //    catch (SocketException e)
    //    {
    //        Debug.Log("WeatherDataPort Write error : " + e.Message + " to client " + clients[clients.Count - 1].clientName);
    //    }
    //}


    //private void CloseSocket()
    //{
    //    tcpListener.Stop();
    //}

    //private void OnDestroy()
    //{
    //    CloseSocket();
    //    if (clients != null)
    //    {
    //        foreach (NetClient c in clients)
    //        {
    //            c.connecting = false;
    //            Debug.Log("connect End");
    //        }
    //    }
    //}

    //private void OnApplicationQuit()
    //{
    //    CloseSocket();
    //}
    //private void OnDisable()
    //{
    //    CloseSocket();
    //}

}
