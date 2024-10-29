using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour, IAttackable
{
    // stats - hp, moveSpeed, damage
    public int hp;
    public int damage;
    public float moveSpeed;
    public float detectionRange;
    public float attackRange;
    public float attackDelay;
    private float _curCoolDown = 0;

    public Character curTarget = null;

    private void Update()
    {
        if (_curCoolDown > 0) _curCoolDown -= Time.deltaTime;

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
        Debug.Log("[Character] Take Damage: " + damage);

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
