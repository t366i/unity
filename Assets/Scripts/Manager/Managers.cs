
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance; // 유일성이 보장된다
    private DataManager s_DataManager = new DataManager();
    private InventoryManager s_inventory = new InventoryManager();

    public DataManager DataManager { get { return s_DataManager; } }
    public InventoryManager Inventory { get { return s_inventory; } }


    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다
    
    void Start()
    {
        Init();
        DataManager.Init();
        Inventory.LoadInventory();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
