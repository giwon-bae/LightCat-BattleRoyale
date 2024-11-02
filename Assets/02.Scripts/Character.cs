using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IAttackable
{
    // stats
    public float moveSpeed = 5f;
    public int hp = 100;

    public int level = 1;
    public int curExp = 0;
    public int maxExp = 100;

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

        // for test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp();
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
        Debug.Log("[Character] Take Damage: " + damage);
        if (hp <= 0)
        {
            // Die()
        }
    }

    public void GainExp(int amount)
    {
        curExp += amount;
        if (curExp >= maxExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        curExp = 0;
        maxExp += level * 100;
        GameManager.Instance.ShowLevelUpUI();
    }

    // Upgrade()
    public void Upgrade(UpgradeType type)
    {
        Debug.Log("Character Upgrade with " + type);
        switch(type)
        {
            case UpgradeType.HP:
                break;
            case UpgradeType.DAMAGE:
                break;
            case UpgradeType.SPEED:
                break;
            case UpgradeType.WEAPON:
                curWeapon.Upgrade();
                break;
            case UpgradeType.SKILL:
                curSkill.Upgrade();
                break;
        }

        GameManager.Instance.HideLevelUpUI();
    }
}
