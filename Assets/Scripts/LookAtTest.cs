using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTest : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);

        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.left);

        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right);

        }




    }
}
