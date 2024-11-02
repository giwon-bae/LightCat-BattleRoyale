using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : ISkill
{
    private int _level = 1;

    public void ActiveSkill(Character character)
    {
        // need to add delay
        Debug.Log("activator is " + character.name);
        Debug.Log("Skill level is " + _level);
    }

    public void Upgrade()
    {
        _level++;
    }
}
