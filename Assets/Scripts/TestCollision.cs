using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f , Color.red, 1.0f);
            
            int mask = (1 << 8) | (1 << 9);
            LayerMask mask2 = LayerMask.GetMask("Name");




        }

    }

}
