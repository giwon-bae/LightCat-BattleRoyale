using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public RangedWeapon() : base(new RangedAttackStrategy())
    {
        damage = 3;
        attackDelay = 1f;
    }
}
