using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CSVParser
{
    public CSVParser()
    {
    }
    ~CSVParser()
    {
        _reader = null;
        _Header = null;
    }

    protected FileInfo _sourceFile = null;
    protected TextReader _reader = null;
    protected string[] _Header = null;


    public virtual void Load() { }
    public virtual void Parse(string[] inputData) { }


    public void LoadFile(string fullpath)
    {
        //이전코드
        //TextAsset _txtFile = (TextAsset)Resources.Load(fullpath);
        //StringReader _reader = new StringReader(_txtFile.text);


        string _txtFile = File.ReadAllText(fullpath);
        StringReader _reader = new StringReader(_txtFile);



        int lineCount = 0;
        string inputData = _reader.ReadLine();

        while (inputData != null)
        {
            //don't realize new-line("\\n") in ngui UILabel
            inputData = inputData.Replace("\\n", "\n");
            string[] stringList = inputData.Split('^');

            if (stringList.Length == 0)
            {
                continue;
            }

            if (ParseData(stringList, lineCount) == false)
            {
                Debug.LogError("Parsing fail : " + stringList.ToString());
            }

            inputData = _reader.ReadLine();
            lineCount++;
        }
        _reader.Close();
    }
    public bool ParseData(string[] inputData, int lineCount)
    {
        if (lineCount < 1)
        {
            return true;
        }
        if (VarifyKey(inputData[0]) == false)
        {
            //Debug.Log\(//Debug.Log\( Debug.Log("VarifyKey fail : " + inputData[0]);\);\);
            return false;
        }
        
        Parse(inputData);
        return true;
    }

    public virtual bool VarifyKey(string keyValue)
    {
        return true;
    }


}