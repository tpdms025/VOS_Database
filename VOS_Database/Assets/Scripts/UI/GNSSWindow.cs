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
    public GameObject icon_ship = null;

    [SerializeField] private Vector2 texSize;        //텍스쳐 전체 크기
    [SerializeField] private Vector2 texCenter;        //텍스쳐 중심위치
    [SerializeField] private Vector2 texUnit;        //텍스쳐 항등원


    /* Vector point (경도,위도)
     * 
     *        p1       p2
     *        *-------*
     *        |       |
     *        |       |
     *        *-------*
     *       p3       p4
     */

    [SerializeField] private List<Vector2> gnssPoints = new List<Vector2>();        //텍스쳐의 gnss 점4개 (위도, 경도)
    [SerializeField] private Vector2 gnssDist;                                     //텍스쳐의 gnss 점4개의 폭,길이 (위도, 경도)
    [SerializeField] private Vector2 gnssCenter;                                   //텍스쳐의 gnss 점4개의 중심 점

    [SerializeField] private Vector2 pointsCenter;                                 //받아온 점 4개의 중심 점
    [SerializeField] private Vector2 curTexPos;                                    //현재 배위치 (texture space)

    [SerializeField] private Vector2 curShipPos;                                   //현재 배위치 (local space)
    [SerializeField] private Vector2 targetDirection = Vector2.up;                 //도착지점의 방향

    private void Awake()
    {
        gnssPoints.Add(new Vector2(124, 43));
        gnssPoints.Add(new Vector2(132, 43));
        gnssPoints.Add(new Vector2(124, 33));
        gnssPoints.Add(new Vector2(132, 33));
        //Init();

        gnssDist = gnssPoints[1] - gnssPoints[2];
        gnssCenter = gnssPoints[2] + gnssDist * 0.5f;


        map.transform.localPosition = Vector3.zero;
        texSize = map.transform.GetComponent<UISprite>().localSize;
        texCenter = texSize * 0.5f;


        texUnit = texSize / gnssDist;
        //texUnit = new Vector2(texSize.x / 360, texSize.y / 180);

    }

    //임시 (디폴트값)
    private void Init()
    {
        gnssPoints.Add(new Vector2(-180, 90));
        gnssPoints.Add(new Vector2(180, 90));
        gnssPoints.Add(new Vector2(-180, -90));
        gnssPoints.Add(new Vector2(180, -90));
    }



    /// <summary>
    /// 맵 이동하기
    /// </summary>
    /// <param name="_latitude"> 위도</param>
    /// <param name="_longitude"> 경도</param>
    public void MoveMap(int _latitude, int _longitude)
    {
        BuildTexture(new Vector2(_longitude, _latitude));

        //맵 지도 이동
        curShipPos = texCenter - curTexPos;              //반대방향으로 이동
        map.transform.localPosition = curShipPos;

        //배 아이콘 회전
        //targetDirection = -curShipPos;
        //icon_ship.transform.localEulerAngles = Quaternion.FromToRotation(Vector3.up,new Vector3(targetDirection.x,targetDirection.y,0)).eulerAngles;
    }

    /// <summary>
    /// 배 회전하기
    /// </summary>
    /// <param name="_roll"></param>
    /// <param name="_pitch"></param>
    public void RotationShip(short _roll, short _pitch)
    {
        Vector3 dirction = new Vector3(_pitch, 0, _roll);

        icon_ship.transform.localEulerAngles = Quaternion.FromToRotation(Vector3.up, dirction).eulerAngles;
    }


    #region Private

    /// <summary>
    /// 위도,경도의 점 4개를 받아와 지도에 맵핑한다.
    /// </summary>
    /// <param name="point1">좌상</param>
    /// <param name="point2">우상</param>
    /// <param name="point3">좌하</param>
    /// <param name="point4">우하</param>
    private void BuildTexture(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
    {
        //GNSS Coordinate
        Vector2 gnss_width = point2 - point1;
        Vector2 gnss_height = point1 - point3;
        pointsCenter = point3 + (gnss_width + gnss_height) * 0.5f;
       

        Vector2 gnss_pos_local = pointsCenter - gnssCenter + (gnssDist * 0.5f);
        curTexPos = new Vector2(gnss_pos_local.x * texUnit.x, gnss_pos_local.y * texUnit.y);

        //맵 지도 이동
        curShipPos = texCenter - curTexPos;              //반대방향으로 이동
        map.transform.localPosition = curShipPos;

        //배 아이콘 회전
        //targetDirection = -curShipPos;
        //icon_ship.transform.localEulerAngles = Quaternion.FromToRotation(Vector3.up,new Vector3(targetDirection.x,targetDirection.y,0)).eulerAngles;
    }

    private void BuildTexture(Vector2 point)
    {
        //GNSS Coordinate
        Vector2 gnss_pos_local = point - gnssCenter + (gnssDist * 0.5f);
        curTexPos = new Vector2(gnss_pos_local.x * texUnit.x, gnss_pos_local.y * texUnit.y);
    }

  

#if UNITY_EDITOR
    private void Update()
    {
        //테스트
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            //test 128도 38도
            //x : 경도  y: 위도
            Vector2 p1 = new Vector2(128, 38);
            BuildTexture(p1);

            //Vector2 p1 = new Vector2(124, 43);
            //Vector2 p2 = new Vector2(132, 43);
            //Vector2 p3 = new Vector2(124, 33);
            //Vector2 p4 = new Vector2(132, 33);
            //BuildTexture(p1, p2, p3, p4);
        }
       
        if(Input.GetKeyDown(KeyCode.Keypad8))
        {

            //test 126도 40.5도
            //x : 경도  y: 위도
            Vector2 p1 = new Vector2(126, 40.5f);
            BuildTexture(p1);

            //Vector2 p1 = new Vector2(124, 43);
            //Vector2 p2 = new Vector2(128, 43);
            //Vector2 p3 = new Vector2(124, 38);
            //Vector2 p4 = new Vector2(128, 38);
            //BuildTexture(p1, p2, p3, p4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {

            //test 124도 43도
            //x : 경도  y: 위도
            Vector2 p1 = new Vector2(124, 43);
            BuildTexture(p1);

            //Vector2 p1 = new Vector2(123, 44);
            //Vector2 p2 = new Vector2(125, 44);
            //Vector2 p3 = new Vector2(123, 42);
            //Vector2 p4 = new Vector2(125, 42);
            //BuildTexture(p1, p2, p3, p4);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //test 0도0도
            //x : 경도  y: 위도
            Vector2 p1 = new Vector2(0, 0);
            BuildTexture(p1);

            //Vector2 p1 = new Vector2(-30, 90);
            //Vector2 p2 = new Vector2(30, 90);
            //Vector2 p3 = new Vector2(-30, -90);
            //Vector2 p4 = new Vector2(30, -90);
            //BuildTexture(p1, p2, p3, p4);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            //test -180도 -90도 (맨왼쪽아래점)
            //x : 경도  y: 위도
            Vector2 p1 = new Vector2(-180, -90);
            BuildTexture(p1);

            //Vector2 p1 = new Vector2(-190, -80);
            //Vector2 p2 = new Vector2(-170, -80);
            //Vector2 p3 = new Vector2(-190, -100);
            //Vector2 p4 = new Vector2(-170, -100);
            //BuildTexture(p1, p2, p3, p4);
        }
    }
#endif
    #endregion
}
