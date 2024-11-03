using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[RequireComponent(typeof(NetworkTransform), typeof(Collider2D))]
public abstract class Monster : NetworkBehaviour, IAttackable
{
    // stats - hp, moveSpeed, damage
    [Networked] public int hp { get; set; }
    public int damage;
    public float moveSpeed;
    public float detectionRange;
    public float attackRange;
    public float attackDelay;
    private float _curCoolDown = 0;

    [Networked] public Character curTarget { get; set; } = null;

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;

        if (_curCoolDown > 0) _curCoolDown -= Runner.DeltaTime;

        FindCharacter();

        if (curTarget == null) return;


        if (Vector2.Distance(transform.position, curTarget.transform.position) <= attackRange)
        {
            ExecuteAttack();
        }
        else
        {
            Move();
        }
    }

    public void ExecuteAttack()
    {
        if (_curCoolDown > 0) return;

        Attack(curTarget);
        _curCoolDown = attackDelay;
    }

    // TakeDamage()
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("[Monster] Take Damage: " + damage);

        if (hp <= 0)
        {
            // Die()
        }
    }

    // FindCharacter()
    public abstract void FindCharacter();
    // Move()
    public abstract void Move();
    // Attack()
    public abstract void Attack(IAttackable target);
}
