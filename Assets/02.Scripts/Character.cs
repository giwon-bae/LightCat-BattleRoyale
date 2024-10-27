using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // stats
    public float moveSpeed = 5f;
    public int hp = 100;

    public Weapon curWeapon;

    private void Update()
    {
        Move();
    }

    // Move()
    void Move()
    {
        Vector2 _moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Verticall")).normalized;

        transform.Translate(_moveDir * moveSpeed * Time.deltaTime);
    }

    // EquipWeapon()
    public void EquipWeapon(Weapon newWeapon)
    {
        curWeapon = newWeapon;
    }

    // Attack()
    public void Attack(Character target)
    {
        if (curWeapon == null) return;

        curWeapon.Attack(target);
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
