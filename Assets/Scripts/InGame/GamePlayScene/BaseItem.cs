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
    // Define에 다음 무기를 정의해놓고, 해당 enum값에 따라 LoadData 진행.


    public void OnUpdate(float totalTime)
    {
        if(UseTime + AttackSpeed > totalTime)
        {
            UseTime += AttackSpeed;

        }
    }

    protected abstract void Use();
    // 사용, 스킬 Prefab 소환 후 Damage 수정.

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
