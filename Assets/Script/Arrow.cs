using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 5;

    //public DropItem dropItem;
    public AttackAttribute atkAtt;
    private void FixedUpdate()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BgCollider")
            Destroy(gameObject);
    }

    public void SetAttribute(AttackAttribute atkAtt)
    {
        this.atkAtt = atkAtt;
    }

}
