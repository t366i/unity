using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLerpTest : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 5.0f);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 5.0f);

        }


        // Get key ¿Í Get Key down À» ¼¯¾î¾²¸é µ¿½Ã¿¡ ´©¸¦°æ¿ì ¾ÃÈû.
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.5f);

        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 1.0f);

        }

    }
}
