using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HealthDrainAura : Ability
{
    [SerializeField] private int _radius;
    [SerializeField] private int _damageOverTime;


    private EntityHealth _playerHealth;

    private WaitForSeconds _waitDuration;
    private WaitForSeconds _waitCooldown;
    private WaitForSeconds _waitDamage = new WaitForSeconds(1);

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

    protected override void TryActivateAbility()
    {
        if (IsReady)
        {
            if (Button.TryGetComponent<HealthDrainButton>(out HealthDrainButton healthDrainButton))
            {
                healthDrainButton.SetDuration(Duration, Cooldown);
            }

            if (TryGetComponent<AbilityDrawer>(out AbilityDrawer abilityDrawer))
            {
                abilityDrawer.SetDuration(Duration);
            }

            StartCoroutine(ActivateAbility());
        }
    }

    private IEnumerator ActivateAbility()
    {
        IsReady = false;

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

            Enemy closestEnemy = null;
            EntityHealth closestHealth = null;

            float closestDistance = Mathf.Infinity;

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    if (enemy.TryGetComponent<EntityHealth>(out EntityHealth enemyHealth))
                    {
                        float distance = Vector2.Distance(transform.position, hit.transform.position);

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
