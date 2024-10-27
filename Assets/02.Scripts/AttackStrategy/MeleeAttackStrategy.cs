using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackStrategy : IAttackStrategy
{
    public void ExecuteAttack(Character target)
    {
        Debug.Log("target is " + target.name);
    }
}
