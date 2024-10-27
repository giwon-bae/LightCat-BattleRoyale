using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // damage
    // attackDelay
    public int damage;
    public float attackDelay;

    private IAttackStrategy _attackStrategy;

    public Weapon(IAttackStrategy strategy)
    {
        _attackStrategy = strategy;
    }

    // Attack()
    public void Attack(Character target)
    {
        _attackStrategy.ExecuteAttack(target);
    }
}
