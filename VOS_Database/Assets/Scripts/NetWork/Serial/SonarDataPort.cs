// ==============================================================
// 소리 정보를 관리하는 포트
//
// AUTHOR: Yang SeEun
// CREATED: 2020-07-09
// UPDATED: 2020-08-21
// ==============================================================



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarDataPort : SerialCOM_Server
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

            float distance = UnityEngine.Random.Range(0.0f, 5.4f);
            float depth = UnityEngine.Random.Range(0.0f, 5.4f);
            Vector3 direction = UnityEngine.Random.insideUnitCircle;

            string data = string.Format("{0}^{1}^{2}^{3}^{4}", date, _time, distance, depth, direction);
            sendData.Enqueue(data);
            time = 0.0f;
        }
    }
}
