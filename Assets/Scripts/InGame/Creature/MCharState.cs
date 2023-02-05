using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCharState : State
{
    protected override void Clear()
    {
        
    }

    protected override void OnHit(GameObject gameObject, Attack attack)
    {
        HP -= attack.Damage;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("이동 관련 함수 만들기");
        }    
    }
}
