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
            string _time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
            float distance = UnityEngine.Random.Range(0.0f, 5.4f);
            float depth = UnityEngine.Random.Range(0.0f, 5.4f);
            Vector3 direction = UnityEngine.Random.insideUnitCircle;

            string data = string.Format("{0}^{1}^{2}^{3}", _time, distance, depth, direction);
            sendData.Enqueue(data);
            time = 0.0f;
        }
    }
}
