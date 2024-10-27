using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public MeleeWeapon()
    {
        damage = 10;
        attackDelay = 3f;
    }

    public override void Attack()
    {
        if (_targetList.Count == 0) return;

        Debug.Log("MeleeWeapon Attack");
        foreach (Character target in _targetList)
        {
            target.TakeDamage(damage);
        }
    }
}
