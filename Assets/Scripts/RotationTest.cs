using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotationTest : MonoBehaviour
{

    [SerializeField] private float _yAngle = 0.0f;

    void Start()
    {
        
    }

    void Rotation1()
    {

        _yAngle += Time.deltaTime * 100.0f;
        transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
    }

    void Rotation2()
    {

        transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));
    }

    void Update()
    {
        //Rotation1();
        //Rotation2();

    }
}
