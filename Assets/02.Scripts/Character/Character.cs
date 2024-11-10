using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[RequireComponent(typeof(NetworkTransform), typeof(Rigidbody2D), typeof(Collider2D))]
public class Character : NetworkBehaviour, IAttackable
{
    // stats
    [Networked] public float moveSpeed { get; set; } = 5f;
    [Networked] public int hp { get; set; } = 100;

    [Networked] public int level { get; set; } = 1;
    [Networked] public int curExp { get; set; } = 0;
    [Networked] public int maxExp { get; set; } = 100;

    [SerializeField] private CameraController _cameraControllerPrefab;

    public Weapon curWeapon;
    public ISkill curSkill = new TestSkill();

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            CameraController cameraController = Instantiate(_cameraControllerPrefab);
            cameraController.SetTarget(transform);
        }
    }

    private void Update()
    {
        // for test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Move(data.direction);

            if (data.attackInput)
            {
                Attack();
            }
            if (data.skillInput)
            {
                Skill();
            }
        }
    }

    // Move()
    void Move(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.Translate(Vector2.right * moveSpeed * Runner.DeltaTime);
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
