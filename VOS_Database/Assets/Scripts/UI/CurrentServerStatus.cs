using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentServerStatus : MonoBehaviour
{
    public List<Client> clientStorage;

    public GameObject client;
    public Client weatherClient; 
    public Client windClient; 
    public Client waveClient;

    public GUIDataScrollView scrollView;

    private void Awake()
    {
        client = GameObject.FindGameObjectWithTag("Clients");
        weatherClient = client.GetComponent<WeatherDataClient>();
        windClient = client.GetComponent<WindDataClient>();
        waveClient = client.GetComponent<WaveDataClient>();

        scrollView = transform.Find("ScrollView").GetComponent<GUIDataScrollView>();
    }

    private void Start()
    {
        clientStorage.Add(weatherClient);
        clientStorage.Add(windClient);
        clientStorage.Add(waveClient);
    }

    private void OnEnable()
    {
        
        StartCoroutine(CheckServerStatus());
    }

    IEnumerator CheckServerStatus()
    {
        yield return null;

        while (true)
        {
            // Call ConnectWindow Func(clinetStorage);
            if(clientStorage !=null)
            scrollView.RenewalData(clientStorage);
            yield return null;
            
        }
    }
}
