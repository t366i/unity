using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        // Get Key down = 한번씩
        // Get Key = 계속 

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, 0.0f, -1.0f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1.0f, 0.0f, 0.0f);
        }

    }
}
