using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackStrategy : IAttackStrategy
{
    // GameObject projectile..?

    public void ExecuteAttack(Character target)
    {
        // create projectile
        Debug.Log("target is " + target.name);
    }
}
