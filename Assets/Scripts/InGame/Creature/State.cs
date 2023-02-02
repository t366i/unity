using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    private float _HP = 0;
    private float _MoveSpeed = 0;

    public float HP {
        get
        {
            return _HP;
        }
        protected set
        {
            if (value <= 0)
                _HP = 0;
            else
                _HP = value;
        }
    }
    public float MoveSpeed {
        get
        {
            return _MoveSpeed;
        }
        protected set
        {
            if (value <= 0)
                _MoveSpeed = 0;
            else
                _MoveSpeed = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Attack component = collision.gameObject.GetComponent<Attack>();
        if (component != null)
            OnHit(collision.gameObject, component);
    }
    private void OnTriggerEnter(Collider other)
    {
        Attack component = other.gameObject.GetComponent<Attack>();
        if (component != null)
            OnHit(other.gameObject, component);
    }


    protected abstract void OnHit(GameObject gameObject, Attack attack);
    protected abstract void Clear();

}
