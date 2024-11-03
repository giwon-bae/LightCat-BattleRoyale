using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : Monster
{
    public override void Spawned()
    {
        hp = 100;
        damage = 10;
        moveSpeed = 3f;
        detectionRange = 0f;
        attackRange = 2f;
        attackDelay = 2f;
    }

    public override void Attack(IAttackable target)
    {
        target.TakeDamage(damage);
    }

    public override void FindCharacter()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        float closestDistance = Mathf.Infinity;
        Character closestTarget = null;

        foreach (var hit in hits)
        {
            Character target = hit.GetComponent<Character>();
            if (target != null)
            {
                float distance = Vector2.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
        }

        curTarget = closestTarget;
    }

    public override void Move()
    {
        if (curTarget == null) return;

        transform.position = Vector2.MoveTowards(transform.position, curTarget.transform.position, moveSpeed * Time.deltaTime);
    }
}
