using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MonsterType
{
    Zombie = 0,
    Orc = 1

}

abstract class MonsterData
{
    protected MonsterType MonsterType;
    protected string MonsterName;
    protected float Life;
    protected float Damage;
    protected bool Alive;


}
