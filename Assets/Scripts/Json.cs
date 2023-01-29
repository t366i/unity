using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyJson : MonoBehaviour
{

    public static T JasonChanger<T>(string data)
    {
        return JsonUtility.FromJson<T>(data);
    }


}
