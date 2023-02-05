using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class WriteInventory : MonoBehaviour
{
    public ItemData Hand;
    public List<ItemData> Having;
    public int Coin;
}

[Serializable]
public class InventoryManager : BaseManager
{
    public ItemData Hand = new ItemData();
    public List<ItemData> Having = new List<ItemData>();
    public int Coin = 0;

    public void SaveInventory()
    {
        GameObject go = new GameObject();
        WriteInventory wi =  go.AddComponent<WriteInventory>();
        wi.Coin = Coin;
        wi.Hand = Hand;
        wi.Having = Having;
        Managers.Instance.DataManager.DataWrite("Inventory", wi);
        UnityEngine.Object.Destroy(go);
    }

    // JsonUtil.FromJson : MonoBehavoir의 Transform 필요
    // JsonUtil.ToJson : Component면 new로 동적 생성 불가.

    public bool LoadInventory()
    {
        InventoryManager inv = Managers.Instance.DataManager.DataRead<InventoryManager>("Inventory");
        if (inv == null)
            return false;
        Hand = inv.Hand;
        Having = inv.Having;
        Coin = inv.Coin;
        return true;
    }

    public override void Init()
    {
        LoadInventory();
    }

    public override void Clear()
    {
    }
}