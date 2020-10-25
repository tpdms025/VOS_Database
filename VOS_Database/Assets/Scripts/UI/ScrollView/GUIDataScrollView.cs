using UnityEngine;
using System.Collections.Generic;

using System.Collections;
using System.Linq;

// 메인 클래스. 여기서 grid에 데이터를 추가시켜준다.
public class GUIDataScrollView : MonoBehaviour 
{
    public int count;
    private UIReuseGrid grid;

    public UIReuseGrid Grid
    {
        get
        {
            return grid;
        }
    }

    void Awake()
    {
		grid = GetComponentInChildren<UIReuseGrid>();
    }

	void Start () 
    {
        //for (int i = 0; i < count; ++i)
        //{
        //    MessageCellData cell = new MessageCellData();
        //    cell.Index = i;
        //    cell.Value = string.Empty;

        //    grid.AddItem(cell, false);
        //}
        //grid.UpdateAllCellData();
    }

    public void ClearData()
    {
        grid.ClearItem(false);
    }

    /// <summary>
    /// ServerConn의 데이터 갱신
    /// </summary>
    /// <param name="clients"></param>
    public void RenewalData(List<Client> clients)
    {
        List<MessageCellData> dataCells = new List<MessageCellData>();

        dataCells.Add(new MessageCellData("[Connection status]", FontStyle.Bold,25));

        foreach (Client c in clients)
        {
            dataCells.Add(new MessageCellData(string.Format("{0} Server", c._type), FontStyle.Normal,20, c.state));
        }
        grid.ChangeItem(dataCells.Cast<IReuseCellData>().ToList());
        grid.UpdateAllCellData();
    }

    /// <summary>
    /// DataWindow의 데이터 갱신
    /// </summary>
    /// <param name="windData"></param>
    /// <param name="waveData"></param>
    public void RenewalData(Database.WindData windData, Database.WaveData waveData)
    {
        List<MessageCellData> dataCells = new List<MessageCellData>();
        dataCells.Add(new MessageCellData("< WindData >", FontStyle.Bold, 25));
        dataCells.Add(new MessageCellData(string.Format("Time :{0}", windData.time)));
        dataCells.Add(new MessageCellData(string.Format("Direction :{0}", windData.windDirection)));
        dataCells.Add(new MessageCellData(string.Format("Speed :{0}", windData.windSpeed)));

        dataCells.Add(new MessageCellData("< WaveData >", FontStyle.Bold, 25));
        dataCells.Add(new MessageCellData(string.Format("Time :{0}", waveData.time)));
        dataCells.Add(new MessageCellData(string.Format("Height :{0}", waveData.waveHeight)));
        dataCells.Add(new MessageCellData(string.Format("Speed :{0}", waveData.waveSpeed)));


        grid.ChangeItem(dataCells.Cast<IReuseCellData>().ToList());
        grid.UpdateAllCellData();

    }

    /// <summary>
    /// 데이터 추가
    /// </summary>
    /// <param name="windData"></param>
    /// <param name="waveData"></param>
    public void AddData(Database.WindData windData, Database.WaveData waveData)
    {
        List<MessageCellData> dataCells = new List<MessageCellData>();
        dataCells.Add(new MessageCellData("< WindData >", FontStyle.Bold,25));
        dataCells.Add(new MessageCellData(string.Format("Time :{0}", windData.time)));
        dataCells.Add(new MessageCellData(string.Format("Direction :{0}", windData.windDirection)));
        dataCells.Add(new MessageCellData(string.Format("Speed :{0}", windData.windSpeed)));

        dataCells.Add(new MessageCellData("< WaveData >", FontStyle.Bold, 25));
        dataCells.Add(new MessageCellData(string.Format("Time :{0}", waveData.time)));
        dataCells.Add(new MessageCellData(string.Format("Height :{0}", waveData.waveHeight)));
        dataCells.Add(new MessageCellData(string.Format("Speed :{0}", waveData.waveSpeed)));


        //WarringData 체크
        if (CheckWarring(windData, waveData))
        {
            Debug.Log(CheckWarring(windData, waveData).ToString());
            dataCells.Add(WarringData(windData, waveData));
            dataCells.Add(new MessageCellData(""));

            foreach (MessageCellData c in dataCells)
            {
                grid.AddItem(c, false);
            }
        }
       
        grid.UpdateAllCellData();

    }

    //WarringData 체크
    private MessageCellData WarringData(Database.WindData windData, Database.WaveData waveData)
    {
        string str = string.Empty;

        //**********수정해야할 부분
        //임시
        if (windData.windSpeed > 15.0f)
        {
            str="[DFA700]Caution";
        }
        else if (windData.windSpeed > 10.0f)
        {
            str="[B10C0C]Warning";
        }
        return new MessageCellData(str,FontStyle.Bold);
    }

    //WarringData 체크
    private bool CheckWarring(Database.WindData windData, Database.WaveData waveData)
    {
        //수정해야할 부분
        //임시
        if (windData.windSpeed > 10.0f)  //주의, 경고
        {
            return true;
        }
        else                             //보통
        {
            return false;
        }

    }




    #region Event
    public void EV_Add()
	{
        MessageCellData cell = new MessageCellData();
		cell.Index = grid.MaxCellData;
		//cell.ImgName = string.Format( "name:{0}", cell.Index );
		grid.AddItem( cell, true );
	}

	public void EV_Remove()
	{
		grid.RemoveItem( grid.GetCellData(0), true );
	}

	public void EV_RemoveAll()
	{
		grid.ClearItem(true);
	}
	#endregion
}
