using UnityEngine;
using System.Collections;

// 셀 데이터를 받아서 스크롤뷰셀의 데이터를 갱신한다.
public class MessageCell : UIReuseScrollViewCell 
{
	public UILabel label;
    public GameObject connectLabel;
    public GameObject disconnectLabel;

    public override void UpdateData (IReuseCellData CellData)
	{
        MessageCellData item = CellData as MessageCellData;
		if( item == null )
			return;

		label.text = string.Format( "{0}",item.Value);
        label.fontStyle = item.fontStyle;
        label.fontSize = item.fontSize;


        OffAllLabel();
        if (item.state == NetworkConnection.Success)
        {
            connectLabel.SetActive(true);
        }
        else if (item.state == NetworkConnection.Fail || item.state == NetworkConnection.Connecting)
        {
            disconnectLabel.SetActive(true);
        }
    }

    public void OffAllLabel()
    {
        if(connectLabel != null)
        {
            connectLabel.SetActive(false);
        }
        if(disconnectLabel != null)
        {
            disconnectLabel.SetActive(false);
        }
    }
}
