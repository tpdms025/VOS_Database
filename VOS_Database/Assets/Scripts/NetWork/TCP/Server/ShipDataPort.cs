// ==============================================================
//  배 정보를 관리하는 포트
//
// AUTHOR: Yang SeEun
// CREATED: 2020-10-27
// UPDATED: 2020-10-27
// ==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDataPort : TCP_ListenPort
{
    private void Awake()
    {
        portName = "ShipDataPort";
    }

    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        if ((time >= sendIntervalTime) && clients.Count != 0)
        {
            string date = System.DateTime.Now.ToString("yyyy-MM-dd");
            string _time = System.DateTime.Now.ToString("HH:mm:ss tt");

            int latitude = UnityEngine.Random.Range(35, 40);  
            int longitude = UnityEngine.Random.Range(124, 128);     
            int altitude = UnityEngine.Random.Range(-180, 180);    
            short heave = (short)UnityEngine.Random.Range(-200, 200);
            short northVelocity = (short)UnityEngine.Random.Range(-200,200);
            short eastVelocity = (short)UnityEngine.Random.Range(-200, 200);
            short downvelocity = (short)UnityEngine.Random.Range(-200, 200);
            short roll = (short)UnityEngine.Random.Range(-180, 180);
            short pitch = (short)UnityEngine.Random.Range(-180, 180);
            ushort heading = (ushort)UnityEngine.Random.Range(0, 180);
            short rollRate = (short)UnityEngine.Random.Range(-180, 180);
            short pitchRate = (short)UnityEngine.Random.Range(-180, 180);
            short headingRate = (short)UnityEngine.Random.Range(-180, 180);


            string data = string.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}^{13}^{14}", 
                date,_time, latitude, longitude, altitude, heave, northVelocity, eastVelocity, downvelocity, roll, pitch, heading,
                rollRate, rollRate, pitchRate, headingRate);
            stringData.Enqueue(data);

            time = 0.0f;
        }
    }
}
