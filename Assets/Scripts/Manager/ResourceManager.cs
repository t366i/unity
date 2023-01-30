using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : BaseManager
{
    Dictionary<string, Object> origins = new Dictionary<string, Object>();

    public T Load<T>(string path) where T : Object
    {
        Object origin = null;
        if (origins.TryGetValue(path, out origin) == false)
        {
            origin = Resources.Load<T>(path);
            if (origin == null)
            {
                Debug.Log("Can't find File");
                return null;
            }
            if (origin as GameObject != null)
            {
                SaveOrigin comp = ((GameObject)origin).GetComponent<SaveOrigin>();
                if (comp != null)
                    origins.Add(path, origin);
            }
        }
        return origin as T;
    }

    public GameObject Instantiate(string path, Transform parent = null) 
    {
        return Instantiate(Load<GameObject>(path), parent);
    }

    public GameObject Instantiate(GameObject origin, Transform parent = null)
    {
        if (origin == null)
            return default;

        GameObject go;

        if (origin.GetComponent<Poolable>() != null)
            go = Managers.Instance.PoolManager.Pop(origin).gameObject;
        else
            go = Object.Instantiate<GameObject>(origin);

        // avoid Dontdestroy
        if (parent == null)
            go.transform.parent = GameObject.FindGameObjectsWithTag("MainCamera")[0].transform;
        go.transform.parent = parent;

        return go;
    }

    public void Destroy(GameObject gameObject)
    {
        Poolable poolable = gameObject.GetComponent<Poolable>();
        if (poolable == null)
            Object.Destroy(gameObject);
        else
            Managers.Instance.PoolManager.Push(poolable);
    }


    public override void Clear()
    {

    }

    public override void Init()
    {

    }
}
