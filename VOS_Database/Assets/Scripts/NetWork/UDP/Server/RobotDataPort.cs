﻿// ==============================================================
// 로봇정보를 관리하는 포트
//
// AUTHOR: Yang SeEun
// CREATED: 2020-06-09
// UPDATED: 2020-06-09
// ==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDataPort : UDP_Server
{
    protected override void Update()
    {
        base.Update();

        time += Time.deltaTime;
        if ((time >= sendIntervalTime))
        {
            //TODO:
            string date = System.DateTime.Now.ToString("yyyy-MM-dd");
            string _time = System.DateTime.Now.ToString("HH:mm:ss tt");

            Vector3 position = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(0.0f, 5.4f);
            Vector3 forwardVector = UnityEngine.Random.insideUnitCircle;
           
            string data = string.Format("{0}^{1}^{2}^{3}", date,_time, position, forwardVector);
            sendData.Enqueue(data);
            time = 0.0f;
        }
    }

}
