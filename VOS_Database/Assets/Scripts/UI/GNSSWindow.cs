// ==============================================================
// 지도 맵핑하는 UI
//
// AUTHOR: Yang SeEun
// CREATED: 2020-10-27
// UPDATED: 2020-10-27
// ==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GNSSWindow : MonoBehaviour
{
    public GameObject map = null;
    public Transform shipTransform;
    public Transform shipRotate;

    /// <summary>
    /// 위도,경도의 점 4개를 받아와 지도에 맵핑한다.
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <param name="point3"></param>
    /// <param name="point4"></param>
    public void Setting(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
    {
        
    }
}
