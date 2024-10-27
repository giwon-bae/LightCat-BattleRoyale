using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{
    // Attack()
    void ExecuteAttack(Character target);
}
