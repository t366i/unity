using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeOfAttack : MonoBehaviour
{
    List<GameObject> range = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enermy")
            range.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
