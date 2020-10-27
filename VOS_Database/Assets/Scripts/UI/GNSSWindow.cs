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

    /*
     *        p1       p2
     *        *-------*
     *        |       |
     *        |       |
     *        *-------*
     *       p3       p4
     */

    public Vector2 texSize;        //텍스쳐 전체 크기
    public Vector2 texUnit;        //텍스쳐 항등원

    public Vector2 texCenter;        //텍스쳐 중심위치
    public Vector2 curTexPos;     //현재 배위치 (텍스쳐기준)

    public Vector2 curShipPos;     //현재 배위치 (local)

    private void Awake()
    {
        texSize = map.transform.GetComponent<UISprite>().localSize;
        texCenter = texSize * 0.5f;

        texUnit = new Vector2(texSize.x / 360, texSize.y / 180);
    }

    /// <summary>
    /// 위도,경도의 점 4개를 받아와 지도에 맵핑한다.
    /// </summary>
    /// <param name="point1">좌상</param>
    /// <param name="point2">우상</param>
    /// <param name="point3">좌하</param>
    /// <param name="point4">우하</param>
    public void Setting(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
    {
        //GNSS Coordinate
        Vector2 gnss_width = point2 - point1;
        Vector2 gnss_height = point1 - point3;
        Vector2 gnss_pointsCenter = point3 + gnss_width * 0.5f + gnss_height * 0.5f;

        Vector2 gnss_pointsCenter_dist = gnss_pointsCenter + new Vector2(180,90);         //범위가 -부터라서
        curTexPos = new Vector2(gnss_pointsCenter_dist.x * texUnit.x, gnss_pointsCenter_dist.y * texUnit.y);

        curShipPos = texCenter - curTexPos;  //반대방향으로 이동

        map.transform.localPosition = curShipPos;
    }
#if UNITY_EDITOR
    private void Update()
    {
        //테스트
        if (Input.GetKeyDown(KeyCode.T))
        {
            //test 10도 10도
            //x : 경도  y: 위도
            Vector2 p1 = new Vector2(-30, 90);
            Vector2 p2 = new Vector2(50, 90);
            Vector2 p3 = new Vector2(-30, -70);
            Vector2 p4 = new Vector2(50, -70);
            Setting(p1, p2, p3, p4);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //test 0도0도 정가운데점
            //x : 경도  y: 위도
            Vector2 p1 = new Vector2(-30, 90);
            Vector2 p2 = new Vector2(30, 90);
            Vector2 p3 = new Vector2(-30, -90);
            Vector2 p4 = new Vector2(30, -90);
            Setting(p1, p2, p3, p4);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //test -180도 -90도 (맨왼쪽아래점)
            //x : 경도  y: 위도
            Vector2 p1 = new Vector2(-190, -80);
            Vector2 p2 = new Vector2(-170, -80);
            Vector2 p3 = new Vector2(-190, -100);
            Vector2 p4 = new Vector2(-170, -100);
            Setting(p1, p2, p3, p4);
        }
    }
#endif
}
