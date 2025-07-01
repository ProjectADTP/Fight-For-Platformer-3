using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HealthDrainAura : Ability
{
    [SerializeField] private Aura _auraEffectPrefab;

    [SerializeField] private int _radius;
    [SerializeField] private int _damageOverTime;

    private Aura _auraEffectInstance;
    private EntityHealth _playerHealth;

    private WaitForSeconds _waitDuration;
    private WaitForSeconds _waitCooldown;
    private WaitForSeconds _waitDamage = new WaitForSeconds(1);

    private float _diameterToRadius = 2;
    private float _scale;

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

            StartCoroutine(ActivateAbility());
        }
    }

    private IEnumerator ActivateAbility()
    {
        IsReady = false;

        if (_auraEffectPrefab != null)
        {
            _auraEffectPrefab.transform.localScale = new Vector2(_radius, _radius);

            _auraEffectInstance = Instantiate(_auraEffectPrefab,transform.position,
                                              Quaternion.identity, _playerHealth.transform);
        }

        StartCoroutine(DrainHealthCoroutine());

        yield return _waitDuration;

        Destroy(_auraEffectInstance.gameObject);

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

            foreach (Collider2D hit in hits)
            {
                Enemy enemy = hit.GetComponent<Enemy>();

                if (enemy != null)
                {
                    EntityHealth enemyHealth = enemy.GetComponent<EntityHealth>();
                    enemyHealth.TakeDamage(_damageOverTime);

                    _playerHealth.RestoreHealth(_damageOverTime);
                }
            }

            yield return _waitDamage;
        }
    }
}
