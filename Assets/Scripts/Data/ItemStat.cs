using System.Collections.Generic;
using System;

[Serializable]
public class ItemData
{
    public string ItemName;
    public int Damage;
}

[Serializable]
public class ItemStat : ILoader
{
    public Dictionary<string, ItemData> dic;

    public List<ItemData> ItemList;

    public void MakeDictionary()
    {
        dic = new Dictionary<string, ItemData>();
        foreach (ItemData itemData in ItemList)
            dic.Add(itemData.ItemName, itemData);
    }
}