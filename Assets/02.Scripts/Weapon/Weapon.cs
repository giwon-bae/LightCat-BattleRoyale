using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // damage
    // attackDelay
    public int damage;
    public float attackDelay;

    public Collider2D attackCollider;
    protected List<Character> _targetList = new List<Character>();

    // Attack()
    public abstract void Attack();

    public void ResetTargets()
    {
        _targetList.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character _target = collision.GetComponent<Character>();

        if (_target == null && _targetList.Contains(_target)) return;

        _targetList.Add(_target);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Character _target = collision.GetComponent<Character>();

        if (_target == null && !_targetList.Contains(_target)) return;

        _targetList.Remove(_target);
    }
}
