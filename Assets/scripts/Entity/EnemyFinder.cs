using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public EntityHealth FindClosestEnemy(float radius, int layerMask)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        EntityHealth closestEnemy = null;

        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (!hit.TryGetComponent(out Enemy enemy)) continue;

            float distance = Vector3Extensions.SqrDistance(transform.position, hit.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.GetComponent<EntityHealth>();
            }
        }

        return closestEnemy;
    }
}
