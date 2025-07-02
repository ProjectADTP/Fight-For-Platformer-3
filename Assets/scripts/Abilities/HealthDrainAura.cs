using System;
using System.Collections;
using UnityEngine;

public class HealthDrainAura : Ability
{
    [SerializeField] private int _radius;
    [SerializeField] private int _damageOverTime;

    private EntityHealth _playerHealth;

    private WaitForSeconds _waitDuration;
    private WaitForSeconds _waitCooldown;
    private WaitForSeconds _waitDamage = new WaitForSeconds(1);

    public event Action<float, float> Activated;

    private float _diameterToRadius = 2;
    private float _scale;

    public int Radius => _radius;

    private void Awake()
    {
        _playerHealth = GetComponentInParent<EntityHealth>();

        _waitDuration = new WaitForSeconds(Duration);
        _waitCooldown = new WaitForSeconds(Cooldown);

        _scale = _playerHealth.transform.localScale.x;
    }

    public override void TryActivateAbility()
    {
        if (IsReady)
        {
            StartCoroutine(ActivateAbility());
        }
    }

    private IEnumerator ActivateAbility()
    {
        IsReady = false;

        Activated?.Invoke(Duration, Cooldown);

        StartCoroutine(DrainHealthCoroutine());

        yield return _waitDuration;

        yield return _waitCooldown;

        IsReady = true;
    }

    private IEnumerator DrainHealthCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < Duration)
        {
            elapsedTime ++;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius * _scale / _diameterToRadius);

            Enemy closestEnemy;
            EntityHealth closestHealth = null;

            float closestDistance = Mathf.Infinity;

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out Enemy enemy))
                {
                    if (enemy.TryGetComponent(out EntityHealth enemyHealth))
                    {
                        float distance = Vector3Extensions.SqrDistance(transform.position, hit.transform.position);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestEnemy = enemy;
                            closestHealth = enemyHealth;
                        }
                    }
                }
            }

            if (closestHealth != null)
            {
                closestHealth.TakeDamage(_damageOverTime);
                _playerHealth.RestoreHealth(_damageOverTime );
            }

            yield return _waitDamage;
        }
    }
}
