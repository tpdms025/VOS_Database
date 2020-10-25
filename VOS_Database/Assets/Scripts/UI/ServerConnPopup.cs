using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerConnPopup : MonoBehaviour
{
    private GameObject loginWindow;

    private void Awake()
    {
        loginWindow = GameObject.Find("SeverUI").gameObject;

        gameObject.SetActive(false);
    }
    public void OpenLoginWindow()
    {
        loginWindow.SetActive(true);
        ClosePopup();
    }

    public void ClosePopup()
    {
        this.gameObject.SetActive(false);
    }
}
