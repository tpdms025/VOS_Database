using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class SaveFileText : MonoBehaviour, IPointerDownHandler
{
    public Text output;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

    // Broser plugin should be called in OnPointerDown.
    public void OnPointerDown(PointerEventData eventData) {
        var bytes = Encoding.UTF8.GetBytes(_data);
        DownloadFile(gameObject.name, "OnFileDownload", "sample.txt", bytes, bytes.Length);
    }

    // Called from browser
    public void OnFileDownload() {
        output.text = "File Successfully Downloaded";
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    // Listen OnClick event in standlone builds
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        // Save file with filter
        var extensionList = new[] { new ExtensionFilter("CSV 문서", "csv"),new ExtensionFilter("Text", "txt")};
        var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "VOS_Sample", extensionList);

        //var path = StandaloneFileBrowser.SaveFilePanel("Title", "", "sample", "csv");
        if (!string.IsNullOrEmpty(path))
        {
            try
            {
                //VOS_Timestamp3.sqlite 임시로 넣음.
                File.WriteAllText(path, DBManager.Inst.Export_DBTimestamp("VOS_Timestamp3.sqlite"));
            }
            catch(System.Exception ex)
            {
                //해야할 것 ***********************************************
                //경고창 띄우기!
                //TODO:
#if UNITY_EDITOR
                Debug.Log("Error : "+ ex.Message);
#endif
            }
        }
    }
#endif
}