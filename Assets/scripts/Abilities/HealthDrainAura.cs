using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyFinder))]
public class HealthDrainAura : Ability
{
    [SerializeField] private int _radius;
    [SerializeField] private int _damageOverTime;

    [SerializeField] private EntityHealth _playerHealth;

    private EnemyFinder _enemyFinder;

    private WaitForSeconds _waitDuration;
    private WaitForSeconds _waitCooldown;

    private WaitForSeconds _waitDamage = new WaitForSeconds(1);

    private float _diameterToRadius = 2;
    private float _scale;

    public event Action<float, float> DurationRemained;
    public event Action<float, float> CooldownRemained;

    public int Radius => _radius;

    private void Awake()
    {
        _enemyFinder = GetComponent<EnemyFinder>();

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

        StartCoroutine(DrainHealth());

        yield return _waitDuration;

        StartCoroutine(WaitingCooldown());

        yield return _waitCooldown;

        IsReady = true;
    }

    private IEnumerator DrainHealth()
    {
        float elapsedTime = 0f;

        while (elapsedTime < Duration)
        {
            elapsedTime++;
            DurationRemained?.Invoke(elapsedTime, Duration);

            EntityHealth closestEnemy = _enemyFinder.FindClosestEnemy(_radius * _scale / _diameterToRadius, 7);

            ProcessDrain(closestEnemy);

            yield return _waitDamage;
        }
    }

    private void ProcessDrain(EntityHealth targetHealth)
    {
        if (targetHealth == null) return;

        int possibleDamage = Mathf.Min(_damageOverTime, targetHealth.Value);

        targetHealth.TakeDamage(possibleDamage);

        _playerHealth.RestoreHealth(possibleDamage);
    }


    private IEnumerator WaitingCooldown()
    {
        float elapsedTime = 0f;

        while (elapsedTime < Cooldown)
        {
            elapsedTime++;

            CooldownRemained?.Invoke(elapsedTime, Cooldown);

            yield return _waitDamage;
        }
    }
}
