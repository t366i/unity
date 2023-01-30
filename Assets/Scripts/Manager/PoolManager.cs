using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : BaseManager
{
    public class Pool
    {
        GameObject Root;
        GameObject origin;

        Stack<Poolable> objects;

        public int Count { get { return objects.Count;  } }

        public Pool(GameObject origin)
        {
            GameObject poolManager = GameObject.Find("@PoolManager");
            Root = new GameObject(origin.GetComponent<Poolable>().poolableName + "_Pool");
            Root.transform.parent = poolManager.transform;
            this.origin = origin;
            objects = new Stack<Poolable>(); 
        }

        public Poolable Pop()
        {
            Poolable poolable = null;
            if (objects.Count > 0)
                poolable = objects.Pop();
            else
                poolable = Utils.GetOrAddComponent<Poolable>(UnityEngine.Object.Instantiate<GameObject>(origin));

            poolable.gameObject.SetActive(true);

            return poolable;
        }
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;
            poolable.gameObject.SetActive(false);
            poolable.gameObject.transform.parent = Root.transform;
            objects.Push(poolable);
        }
        public void PushNew(int count = 10)
        {
            GameObject newObject;
            for(int i=0; i< count; i++)
            {
                newObject = UnityEngine.Object.Instantiate<GameObject>(origin);
                newObject.SetActive(false);
                newObject.transform.parent = Root.transform;
                objects.Push(Utils.GetOrAddComponent<Poolable>(newObject));
            }
        }
        public void SetSize(int count)
        {
            if(count < Count)
                while(Count - count != 0)
                    Object.Destroy(objects.Pop().gameObject);
            else
                PushNew(count - Count);
        }
    }

    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    public override void Clear()
    {
        pools.Clear();
    }

    public override void Init()
    {
        GameObject managers = GameObject.Find("@Managers");
        GameObject PoolManager = new GameObject("@PoolManager");
        PoolManager.transform.parent = managers.transform;
    }

    public void Push(Poolable poolable)
    {
        if (poolable == null)
            return;
        if (poolable.poolableName == "")
            poolable.poolableName = poolable.gameObject.name;
        Pool pool;
        if (!pools.TryGetValue(poolable.poolableName, out pool))
            return;
        pool.Push(poolable);
    }

    public bool CreatePool(GameObject origin, int count = 10)
    {
        if (origin == null)
            return false;
        Poolable poolable = origin.GetComponent<Poolable>();
        if (poolable == null)
            return false;
        if (poolable.poolableName == "")
            poolable.poolableName = poolable.gameObject.name;

        Pool pool;
        if (!pools.TryGetValue(poolable.poolableName, out pool))
        {
            pool = new Pool(origin);
            pools.Add(poolable.poolableName, pool);
            pool.PushNew(count);
        }
        return true;
    }

    public bool LeastPool(GameObject origin, int count)
    {
        if (!CreatePool(origin, 0))
            return false;
        Pool pool;
        Poolable poolable = origin.GetComponent<Poolable>();
        pools.TryGetValue(poolable.poolableName, out pool);
        if(count > pool.Count)
            pool.PushNew(count - pool.Count);

        return true;
    }
    public bool SetPoolSize(GameObject origin, int count)
    {
        if (!CreatePool(origin, 0))
            return false;
        Pool pool;
        Poolable poolable = origin.GetComponent<Poolable>();
        pools.TryGetValue(poolable.poolableName, out pool);
        pool.SetSize(count);
        return true;
    }

    public Poolable Pop(GameObject origin)
    {
        if (!CreatePool(origin))
            return null;
        Poolable poolable = origin.GetComponent<Poolable>();
        Pool pool;
        pools.TryGetValue(poolable.poolableName, out pool);
        Poolable popPoolable = pool.Pop();
        popPoolable.poolableName = poolable.poolableName;
        return popPoolable;
    }

}
