using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem 
{
    
    protected string WeaponName;
    protected float AttackSpeed;
    protected int WeaponType;
    protected int WeaponGrade;
    protected int WeaponDamage;
    protected float UseTime;

    public BaseItem(int weaponIndex, float totalTime)
    {
        LoadData(weaponIndex);
        UseTime = totalTime;
    }


    public abstract void Upgrade();
    // Define�� ���� ���⸦ �����س���, �ش� enum���� ���� LoadData ����.


    public void OnUpdate(float totalTime)
    {
        if(UseTime + AttackSpeed > totalTime)
        {
            UseTime += AttackSpeed;

        }
    }

    protected abstract void Use();
    // ���, ��ų Prefab ��ȯ �� Damage ����.

    private void LoadData(int weaponIndex)
    {
        // Load Json

        // WeaponName =?;
        //AttackSpeed =?;
        //WeaponType =?;
        //WeaponGrade =?;
        //WeaponDamage =?;
    }
}
