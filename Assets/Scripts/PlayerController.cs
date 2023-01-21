using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5.0f;
    

    void Start()
    {

        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;

        //ManagerTest.Input.KeyAction += OnKeyBoard(); 이거 붙이면 안됨 ㅋ.ㅋ


    }

    void OnKeyBoard()
    {

        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += new Vector3(0.0f, 0.0f, 1.0f);
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.position += new Vector3(0.0f, 0.0f, -1.0f);
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);

        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);

        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);

        }
    }

    
    void Update()
    {
        // Get Key down = 한번씩
        // Get Key = 계속 

        


    }
}
