using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public GameObject projectilePrefab;

    public RangedWeapon()
    {
        damage = 3;
        attackDelay = 1f;
    }

    public override void Attack()
    {
        if (projectilePrefab == null)
        {
            throw new System.Exception("projectilePrefab is null");
        }

        Debug.Log("RangedWeapon Attack");
        //Instantiate(projectilePrefab, transform.position, transform.rotation);
    }

    public override void Upgrade()
    {
        _level++;
        Debug.Log("RangedWeapon level is " + _level);
    }
}
