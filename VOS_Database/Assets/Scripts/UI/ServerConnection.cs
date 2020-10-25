///
/// Server Connection
///
/// AUTHOR : Kim Hangyul
/// Created : 2020-07-10
/// UPDATED : 2020-07-10
///

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct ServerValidation
{
    public GameObject gameObject;
    public string ip;
    public int port;

    public ServerValidation(GameObject ServerClient, string _ip, int _port)
    {
        gameObject = ServerClient;
        ip = _ip;
        port = _port;
    }

}

public class ServerConnection : MonoBehaviour
{
    [SerializeField] 
    private GameObject ClientStorage;
    [SerializeField]
    private static int FailedClients=0;

    public float connectingTime = 1;
    public float term = 3;

    #region Varaiables
    public WeatherDataClient weatherClient;
    public WaveDataClient waveClient;
    public WindDataClient windClient;
    public SonarDataClient sonarClient;
    private Queue<NetworkConnection> IsConnected;
    private Dictionary<DataType, ServerValidation> ServerInfo;
    private GameObject[] ServerClient;
    private UIInput ClientIP;
    private UIInput ClientPort;
    private GameObject ConnectingPopup;
    private GameObject Sub_Submit;
    private UILabel ConnectWindow_Label;

    private BoxCollider bc;
    private enum Clientenum { WeatherClient=0, WindClient, WaveClient}
    private ServerValidation serverData;
    private bool flag;
    public GameObject ServerUI;

    #endregion

    private void Awake()
    {
        flag = false;
        ServerClient = GameObject.FindGameObjectsWithTag("Login");
        ClientStorage = GameObject.Find("Clients");
        weatherClient = ClientStorage.GetComponent<WeatherDataClient>();
        waveClient = ClientStorage.GetComponent<WaveDataClient>();
        windClient = ClientStorage.GetComponent<WindDataClient>();
        //sonarClient = ClientStorage.transform.Find("SonarClient").GetComponent<SonarDataClient>();
        ServerInfo = new Dictionary<DataType, ServerValidation>();
        IsConnected = new Queue<NetworkConnection>();
        ConnectingPopup =transform.Find("ConnectingPopup").gameObject;
        ConnectWindow_Label = ConnectingPopup.transform.GetComponentInChildren<UILabel>();
        bc = gameObject.GetComponent<BoxCollider>();
        Sub_Submit = ConnectingPopup.transform.Find("ConnectWindow").transform.Find("Submit").gameObject;
        ServerUI = transform.parent.transform.parent.gameObject;
    }

    public void OnSubmit()
    {
        bc.enabled = false;
        ConnectingPopup.gameObject.SetActive(true);
        ConnectWindow_Label.text = "Connecting...";
        Sub_Submit.SetActive(false);
        
        GetInfo(); // Input 박스로부터 정보 가져오기
        Debug.Log("OnSubmit");
    }
    private void GetInfo()
    {
        for (int i = 0; i < ServerClient.Length; i++)
        {
            DataType clientDataType = ServerClient[i].GetComponent<ClientsValidation>().clientDataType;
            ClientIP = ServerClient[i].transform.Find("IP").GetComponent<UIInput>();
            ClientPort = ServerClient[i].transform.Find("Port").GetComponent<UIInput>();
            

            Debug.Log(ClientPort.value.ToString()+" : Port");
            if (ClientPort.value == "" || ClientIP.value == "") // null 값  
            {
                serverData = new ServerValidation(ServerClient[i], string.Empty,0);
                ServerInfo.Add(clientDataType, serverData);
                FailedClients++;
                Debug.Log("null");
                continue;
            }

            serverData = new ServerValidation(ServerClient[i], ClientIP.value, int.Parse(ClientPort.value));
            ServerInfo.Add(clientDataType, serverData);
        }

        InsertInfo(); // 가져온 정보 대입하기

    }

    private void InsertInfo()
    {

        weatherClient.ip = ServerInfo[DataType.WeatherData].ip;
        weatherClient.port = ServerInfo[DataType.WeatherData].port;
        weatherClient.ConnectedToServer();

        windClient.ip = ServerInfo[DataType.WindData].ip;
        windClient.port = ServerInfo[DataType.WindData].port;
        windClient.ConnectedToServer();

        waveClient.ip = ServerInfo[DataType.WaveData].ip;
        waveClient.port = ServerInfo[DataType.WaveData].port;
        waveClient.ConnectedToServer();

        /*sonarClient.ip = ServerInfo[DataType.SonarData].ip;
        sonarClient.port = ServerInfo[DataType.SonarData].port;
        sonarClient.ConnectedToServer();*/


        StartCoroutine(CheckConnection()); // 실질적으로 서버와 데이터 값 비교하기
    }


    IEnumerator CheckConnection()
    {
        yield return new WaitForSeconds(connectingTime);
        while (true)
        {
            IsConnected.Enqueue(weatherClient.state);
            IsConnected.Enqueue(waveClient.state);
            IsConnected.Enqueue(windClient.state);

            if(weatherClient.state == NetworkConnection.Success)
            {
                ServerInfo[DataType.WeatherData].gameObject.transform.Find("Outline").gameObject.SetActive(false);
            }

            if(windClient.state == NetworkConnection.Success)
            {
                ServerInfo[DataType.WindData].gameObject.transform.Find("Outline").gameObject.SetActive(false);
            }

            if(waveClient.state == NetworkConnection.Success)
            {
                ServerInfo[DataType.WaveData].gameObject.transform.Find("Outline").gameObject.SetActive(false);
            }
            /*if(sonarClient.state == NetworkConnection.Success)
            {
                ServerInfo[DataType.SonarData].gameObject.transform.Find("Outline").gameObject.SetActive(false);
            }*/
            

            if (!IsConnected.Contains(NetworkConnection.Connecting))
            {
                if (weatherClient.state == NetworkConnection.Fail)
                {
                    ServerInfo[DataType.WeatherData].gameObject.transform.Find("Outline").gameObject.SetActive(true);
                    if(ServerInfo[DataType.WeatherData].ip != string.Empty || ServerInfo[DataType.WeatherData].port != 0)
                    {
                        FailedClients++;
                    }
                    
                }
                if (windClient.state == NetworkConnection.Fail)
                {
                    ServerInfo[DataType.WindData].gameObject.transform.Find("Outline").gameObject.SetActive(true);
                    if (ServerInfo[DataType.WindData].ip != string.Empty || ServerInfo[DataType.WindData].port != 0)
                    {
                        FailedClients++;
                    }
                }
                if (waveClient.state == NetworkConnection.Fail)
                {
                    ServerInfo[DataType.WaveData].gameObject.transform.Find("Outline").gameObject.SetActive(true);
                    if (ServerInfo[DataType.WaveData].ip != string.Empty || ServerInfo[DataType.WaveData].port != 0)
                    {
                        FailedClients++;
                    }
                }

                /*if (sonarClient.state == NetworkConnection.Fail) // FailedClients 에 포함되나 Success 시킴
                {
                    ServerInfo[DataType.SonarData].gameObject.transform.Find("Outline").gameObject.SetActive(true);
                    if (ServerInfo[DataType.SonarData].ip != string.Empty || ServerInfo[DataType.SonarData].port != 0)
                    {
                        FailedClients++;
                    }
                }*/
                break; 
            }
            IsConnected.Clear();

            yield return null;
        }

        if (!IsConnected.Contains(NetworkConnection.Fail))
        {
            ConnectWindow_Label.text = "Success";
            ConnectWindow_Label.color = Color.green; // 색 : Lime green
            Sub_Submit.SetActive(false);
            Invoke("ConnectionSuccess",2);
            flag = true;
        }
        else
        {
            ConnectWindow_Label.text = "Failed \n" + FailedClients.ToString();
            ConnectWindow_Label.color = Color.red; // 색 : Crimson
            Sub_Submit.SetActive(true);
            flag = false;
        }
        IsConnected.Clear();

        yield return null;
    }

    public void ConnectionSuccess()
    {
        ServerUI.SetActive(false);
        ConnectingPopup.SetActive(false);
        ConnectWindow_Label.color = Color.white;
        bc.enabled = true;
        ServerInfo.Clear();
    }

    public void Sub_OnSubmit()
    {
        Debug.Log("Clicked");
        ConnectingPopup.gameObject.SetActive(false);
        ConnectWindow_Label.color = Color.white;
        bc.enabled = true;
        ServerInfo.Clear();
        FailedClients = 0;
    }


}
