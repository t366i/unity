using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInv 
{
    List<BaseItem> ItemList = new List<BaseItem>();
    
    public void OnUpdate(float totalTime)
    {
        foreach(BaseItem item in ItemList)
        {
            item.OnUpdate(totalTime);
        }
    }
    public void AddItem()
    {

    }
    public void UpgradeItem(int index)
    {
        ItemList[index].Upgrade();
    }
}
