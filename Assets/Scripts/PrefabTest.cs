using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{

    public GameObject prefab;
    public GameObject box;

    private void Start()
    {

        //box = Instantiate(prefab);
        //Destroy(box, 3.0f);

        //prefab = Resources.Load<GameObject>("Prefabs/Cube");
        //box = Instantiate(prefab);

        box = Managers.Resource.Instantiate("Cube");

        //Managers.Resource.Destroy(box);
    }

}
