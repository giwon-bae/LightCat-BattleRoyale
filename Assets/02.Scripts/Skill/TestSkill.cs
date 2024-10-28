using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : ISkill
{
    public void ActiveSkill(Character character)
    {
        // need to add delay
        Debug.Log("activator is " + character.name);
    }
}
