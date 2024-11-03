using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType { HP, DAMAGE, SPEED, WEAPON, SKILL }

public class UpgradeOption : MonoBehaviour
{
    public UpgradeType upgradeType;
    public Character character;

    public void OnClick() {
        if (character == null)
        {
            Debug.Log("Character is null");
        }

        character.Upgrade(upgradeType);
    }
}
