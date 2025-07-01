using UnityEngine;
using System.Collections;

public class AbilityDrawer : MonoBehaviour
{
    [SerializeField] private Aura _auraEffectPrefab;
    
    private HealthDrainAura _ability;
    private Aura _auraEffectInstance;
    private WaitForSeconds _abilityDuration;

    private void Awake()
    {
        if (TryGetComponent<HealthDrainAura>(out HealthDrainAura ability))
        {
            _ability = ability;
        }
    }

    public void SetDuration(float duration)
    {
        _abilityDuration = new WaitForSeconds(duration);

        StartCoroutine(Draw());
    }

    private IEnumerator Draw()
    {
        if (_auraEffectPrefab != null)
        {
            _auraEffectPrefab.transform.localScale = new Vector2(_ability.Radius, _ability.Radius);

            _auraEffectInstance = Instantiate(_auraEffectPrefab, transform.position, Quaternion.identity, this.transform);
        }

        yield return _abilityDuration;

        Destroy(_auraEffectInstance.gameObject);
    }
}
