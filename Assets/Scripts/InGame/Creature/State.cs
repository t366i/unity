using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    private float _MaxHP = 1;
    private float _HP = 1;
    private float _MoveSpeed = 0;

    public float MaxHP
    {
        get
        {
            return _MaxHP;
        }
        protected set
        {
            if (value <= 1)
                _MaxHP = 1;
            else
                _MaxHP = value;
        }
    }
    public float HP {
        get
        {
            return _HP;
        }
        protected set
        {
            if (value <= 0)
                _HP = 0;
            else if (_HP + value >= _MaxHP)
                _HP = MaxHP;
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
