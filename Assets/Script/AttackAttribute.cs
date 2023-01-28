using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackAttribute
{
    public enum Type
    {
        Poison,
        AttackBuff
    }

    public Type type;
    public int damage;
    public float duration;
    public float interval;
    public GameObject effectGo;
}
