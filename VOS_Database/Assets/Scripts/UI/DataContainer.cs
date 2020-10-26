using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    private GameObject ServerConnPopup;

    private GameObject dataWindow;
    private GameObject warningWindow;
    private GameObject serverWindow;

    private GUIDataScrollView dataScrollView;
    private GUIDataScrollView warningScrollView;
 

    private void Awake()
    {
        ServerConnPopup = GameObject.Find("UI Root").transform.Find("ServerConnPopup").gameObject;

        if (dataWindow == null)
        {
            dataWindow = transform.Find("DataWindow").gameObject;
        }
        if (warningWindow == null)
        {
            warningWindow = transform.Find("WarningWindow").gameObject;
        }
        if (serverWindow == null)
        {
            serverWindow = transform.Find("ServerWindow").gameObject;
        }

        OffAllWindow();
        dataWindow.SetActive(true);


        dataScrollView = dataWindow.transform.Find("ScrollView").GetComponent<GUIDataScrollView>();
        warningScrollView = warningWindow.transform.Find("ScrollView").GetComponent<GUIDataScrollView>();
    }

    public void OpenServerConnPopup()
    {
        if(ServerConnPopup != null)
        {
            ServerConnPopup.SetActive(true);
        }
    }

    public void TempButton()
    {
        Database.WindData windData = DBManager.Inst.getWindData();
        Database.WaveData waveData = DBManager.Inst.getwaveData();
        SetData(windData, waveData);
    }

    public void TempButton2222()
    {
        Database.WindData windData = DBManager.Inst.getWindData();
        Database.WaveData waveData = DBManager.Inst.getwaveData();
        SetWarningData(windData, waveData);
    }


    public void SetData(Database.WindData windData, Database.WaveData waveData)
    {
        if (windData != null && waveData != null)
        {
            dataScrollView.RenewalData(windData, waveData);
        }

    }
    public void SetWarningData(Database.WindData windData, Database.WaveData waveData)
    {
        if (windData != null && waveData != null)
        {
            warningScrollView.AddData(windData, waveData);
        }

    }

    private void OffAllWindow()
    {
        dataWindow.SetActive(false);
        warningWindow.SetActive(false);
        serverWindow.SetActive(false);
    }


    #region Toggle
    public void OnChangeToggle(UIToggle uIToggle)
    {    
        UIToggle cur = uIToggle;

        //if (cur.value == false) return;

        OffAllWindow();
        switch (cur.name)
        {
            case "DataToggle":
                dataWindow.SetActive(true);
                break;
            case "WarningToggle":
                warningWindow.SetActive(true);
                break;
            case "ServerToggle":
                serverWindow.SetActive(true);
                break;
        }
    }
#endregion

}
