using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour , WalkingMonster
{

    public void Move() { Debug.Log("Zombie Move"); } 
    public void Attack() { Debug.Log("Zombie Attack"); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
