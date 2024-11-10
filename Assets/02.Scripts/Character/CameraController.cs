using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0, 0, -10);
    private float _moveSpeed = 5f;
    private Transform _target;

    private void Start()
    {
        if (_target != null)
        {
            transform.position = _target.position + _offset;
        }
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPost = _target.position + _offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPost, _moveSpeed * Time.deltaTime);
        transform.position = smoothPos;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
