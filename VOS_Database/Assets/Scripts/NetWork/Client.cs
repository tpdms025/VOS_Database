using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [Tooltip("Debug용 변수이다.")]
    public DataType _type;
    public NetworkConnection state = NetworkConnection.None;

    //info
    public string ip = string.Empty; //175.115.182.120
    public int port = 0;
}
