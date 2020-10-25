using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 자신만의 셀 데이터를 정의하자.
public class MessageCellData : IReuseCellData{

    public enum State { None, connect, disconnet}
	#region IReuseCellData
	public int m_Index;
	public int Index{
		get{
			return m_Index;
		}
		set{
			m_Index = value;
		}
	}
	#endregion

	// user data
	public string Value;
    public FontStyle fontStyle = FontStyle.Normal;
    public int fontSize = 20;
    public NetworkConnection state = NetworkConnection.None;

    public MessageCellData()
    {

    }
    public MessageCellData(string value, FontStyle _fontStyle = FontStyle.Normal, int _fontSize = 20, NetworkConnection _state = NetworkConnection.None)
    {
        Value = value;
        fontStyle = _fontStyle;
        fontSize = _fontSize;
        state = _state;
    }

}