using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Managers managers = Managers.Instance;
        ResourceManager resource = managers.ResourceManager;
        PoolManager poolManager = managers.PoolManager;

        GameObject origin = resource.Load<GameObject>("Prefabs/testPrefab");
        poolManager.LeastPool(origin, 100);
        poolManager.SetPoolSize(origin, 10);
        poolManager.LeastPool(origin, 20);
    }

}
