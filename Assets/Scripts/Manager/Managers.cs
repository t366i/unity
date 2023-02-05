
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance; // 유일성이 보장된다
    private DataManager s_dataManager = new DataManager();
    private InventoryManager s_inventory = new InventoryManager();
    private PoolManager s_poolManager = new PoolManager();
    private ResourceManager s_resourceManager = new ResourceManager();

    public DataManager DataManager { get { return Instance.s_dataManager; } }
    public InventoryManager Inventory { get { return Instance.s_inventory; } }
    public PoolManager PoolManager { get { return Instance.s_poolManager; } }
    public ResourceManager ResourceManager { get { return s_resourceManager; } }


    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다
    
    void Start()
    {
        Init();
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

            // Start에 Init을 넣으니까 다른 컴포넌트가 GameManager보다 먼저 호출 시 다른 매니저 사용에 에러가 발생함.
            Instance.DataManager.Init();
            Instance.Inventory.Init();
            Instance.PoolManager.Init();
            Instance.ResourceManager.Init();
        }
    }
}
