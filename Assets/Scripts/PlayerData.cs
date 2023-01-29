using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Archer = 0,
    SwordsMan = 1

}

public class PlayerData 
{
    [SerializeField]
    private PlayerType PlayerType;
    public string PlayerName;
    public float Life;
    public float Damage;
    public bool Alive;
    public float MoveSpeed = 10.0f;
    public float AttackSpeed = 1.0f;


    public PlayerType Type
    {
        get { return PlayerType; }
        set { PlayerType = value; }
    }

    public string Name
    {
        get { return PlayerName; }
        set { PlayerName = value; }
    }

    public float Hp
    {
        get { return Life; }
        set { Life = value; }
    }

    public float playerDamage
    {
        get { return Damage; }
        set { Damage = value; }
    }


    public bool PlayerAlive
    {
        get { return Alive; }
        set { Alive = value; }
    }

    public float PlayerMoveSpeed
    {
        get { return MoveSpeed; }
        set { MoveSpeed = value; }
    }

    public float PlayerAttackSpeed
    {
        get { return AttackSpeed; }
        set { AttackSpeed = value; }
    }













}
