using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class FieldOfView : NetworkBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach (Collider2D target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector2 directionToTarget = (targetTransform.position - transform.position).normalized;

            if (Vector2.Angle(transform.right, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, targetTransform.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(targetTransform);
                    SetTargetRenderer(targetTransform, true);
                }
                else
                {
                    SetTargetRenderer(targetTransform, false);
                }
            }
            else
            {
                SetTargetRenderer(targetTransform, false);
            }
        }
    }

    void SetTargetRenderer(Transform target, bool isVisible)
    {
        if (!Object.HasInputAuthority) return;

        SpriteRenderer renderer = target.GetComponent<SpriteRenderer>();
        if (renderer == null) return;

        renderer.enabled = isVisible;
    }

    // for test
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector2 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector2 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + viewAngleB * viewRadius);

        Gizmos.color = Color.red;
        foreach (Transform visibleTarget in visibleTargets)
        {
            Gizmos.DrawLine(transform.position, visibleTarget.position);
        }
    }
}
