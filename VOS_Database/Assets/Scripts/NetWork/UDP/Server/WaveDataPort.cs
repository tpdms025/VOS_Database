using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDataPort : UDP_Server
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

            float waveHeight = UnityEngine.Random.Range(0.0f, 2.3f);
            float waveSpeed = UnityEngine.Random.Range(0.0f, 2.3f);

            string data = string.Format("{0}^{1}^{2}^{3}", date,_time, waveHeight, waveSpeed);
            sendData.Enqueue(data);
            time = 0.0f;
        }
    }
}
