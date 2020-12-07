// ==============================================================
// Cracked 클라이언트 구조
//
// AUTHOR: Yang SeEun
// CREATED: 2020-05-11
// UPDATED: 2020-06-19
// ==============================================================


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public enum NetworkConnection { None, Connecting, Success, Fail };

public class TCP_LocalClient : Client
{
    [SerializeField] protected bool socketReady = false;
    protected TcpClient socket;
    protected NetworkStream stream;

    //receive
    protected byte[] receiveBuffer;
    protected int dataBufferSize = 4096;
    protected Thread receiveThread;

    private string stringData;
    public string StringData
    {
        get { return stringData; }
        set
        {
            stringData = value;
            Parsing(stringData);
        }
    }

    #region Parsing

    protected virtual void Parsing(string _str)
    {
    }
    #endregion

    #region TcpClient
    public void ConnectedToServer()
    {
        //이미 연결했다면 무시
        if (socketReady) return;

        //if (hostIP == string.Empty || port == 0)
        //{
        //    state = NetworkConnection.Fail;
        //    return;
        //}

        socket = new TcpClient();
        Connect();
    }

    private void Connect()
    {
        try
        {
            state = NetworkConnection.Connecting;

            socket.BeginConnect(ip, port, ConnectCallback, socket);
        }
        catch (Exception ex)
        {
#if UNITY_EDITOR
            Debug.Log(_type.ToString() + " Connect error : " + ex.Message);
#endif
            state = NetworkConnection.Fail;
        }
    }


    private void ConnectCallback(IAsyncResult _result)
    {
        try
        {

            socket.EndConnect(_result);

            state = NetworkConnection.Success;
#if UNITY_EDITOR
            Debug.Log(_type.ToString() + " connect success");
#endif
            if (!socket.Connected)
            {
#if UNITY_EDITOR
                Debug.Log(_type.ToString() + " not connect");
#endif
                state = NetworkConnection.Fail;
                return;
            }

            //Connect후로 꼭 넣기
            socketReady = true;

            //receiveThread = new Thread(new ThreadStart(Receive));
            //receiveThread.IsBackground = false;
            //receiveThread.Start();


            stream = socket.GetStream();

            receiveBuffer = new byte[dataBufferSize];
            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }
        catch (SocketException ex)
        {
#if UNITY_EDITOR
            Debug.Log(_type.ToString() + " Connect error : " + ex.Message);
#endif
            CloseSocket();
        }
    }


    private void ReceiveCallback(IAsyncResult _result)
    {
        try
        {
            int bytelength = stream.EndRead(_result);
            if (bytelength <= 0)
            {
                Debug.Log(_type.ToString() + " byte length = " + bytelength);
                CloseSocket();
                return;

            }
            byte[] data = new byte[bytelength];
            Array.Copy(receiveBuffer, data, bytelength);

            StringData = Encoding.ASCII.GetString(data);

#if UNITY_EDITOR
            Debug.Log(_type.ToString() + " Receive : " + StringData);
#endif

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.Log(_type.ToString() + " Receive Error" + e.Message);
#endif
            CloseSocket();
        }
    }

    #region 이전버전 Receive
    private void Receive()
    {
        int bytelength;

        stream = socket.GetStream();

        try
        {
            //접속이 연결되어 있으면
            while (socket.Connected && (bytelength = stream.Read(receiveBuffer, 0, dataBufferSize)) > 0)
            {
                var incommingData = new byte[bytelength];
                Array.Copy(receiveBuffer, 0, incommingData, 0, bytelength);
                string data3 = Encoding.ASCII.GetString(incommingData);
#if UNITY_EDITOR
                Debug.Log("receive: " + data3);
#endif
            }
#if UNITY_EDITOR
            Debug.Log("Client read End");
#endif
        }
        catch (Exception e)
        {
#if UNITY_EDITOR
            Debug.Log("Receive Error" + e.Message);
#endif
        }
    }

    #endregion

    
    private void CloseSocket()
    {
        if (socketReady)
        {
            stream.Close();
            socket.Close();
        }

        socketReady = false;
        state = NetworkConnection.Fail;
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }
    private void OnDisable()
    {
        CloseSocket();
    }
    private void OnDestroy()
    {
        CloseSocket();
    }
    #endregion
}
