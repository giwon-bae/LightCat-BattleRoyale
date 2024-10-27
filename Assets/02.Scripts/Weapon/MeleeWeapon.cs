using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public MeleeWeapon() : base(new MeleeAttackStrategy())
    {
        damage = 10;
        attackDelay = 3f;
    }
}
