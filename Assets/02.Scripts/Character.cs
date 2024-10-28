using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // stats
    public float moveSpeed = 5f;
    public int hp = 100;

    public Weapon curWeapon;
    public ISkill curSkill = new TestSkill();

    private void Update()
    {
        Move();

        if (Input.GetButton("Fire1"))
        {
            Attack();
        }
        if (Input.GetButton("Fire2"))
        {
            Skill();
        }
    }

    // Move()
    void Move()
    {
        Vector2 _moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        transform.Translate(_moveDir * moveSpeed * Time.deltaTime);
    }

    // EquipWeapon()
    public void EquipWeapon(Weapon newWeapon)
    {
        curWeapon = newWeapon;
        curWeapon.ResetTargets();
    }

    // Attack()
    public void Attack()
    {
        if (curWeapon == null) return;

        curWeapon.ExecuteAttack();
    }

    // Skill()
    public void Skill()
    {
        if (curSkill == null) return;

        curSkill.ActiveSkill(this);
    }

    // TakeDamage()
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            // Die()
        }
    }

    // etc...
}
