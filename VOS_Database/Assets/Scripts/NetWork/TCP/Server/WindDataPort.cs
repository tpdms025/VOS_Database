using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDataPort : TCP_ListenPort
{
    private void Awake()
    {
        portName = "WindDataPort";
    }

    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        if ((time >= sendIntervalTime) && clients.Count != 0)
        {
            string _time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
            Vector3 windDirection = Random.insideUnitCircle;
            float windSpeed = UnityEngine.Random.Range(0.0f, 30.0f);

            string data = string.Format("{0}^{1}^{2}", _time,windDirection,windSpeed);
            stringData.Enqueue(data);

            time = 0.0f;
        }
    }
}
