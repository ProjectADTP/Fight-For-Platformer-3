using UnityEngine;
using System.Collections;

public class AbilityDrawer : MonoBehaviour
{
    [SerializeField] private Aura _auraEffectPrefab;
    
    private HealthDrainAura _ability;
    private Aura _auraEffectInstance;
    private WaitForSeconds _abilityDuration;

    private void OnEnable()
    {
        _ability.Activated += SetDuration;
    }

    private void Awake()
    {
        if (TryGetComponent(out HealthDrainAura ability))
        {
            _ability = ability;
        }

        Draw();
    }

    private void OnDisable()
    {
        _ability.Activated -= SetDuration;
    }

    private void SetDuration(float duration, float _)
    {
        _abilityDuration = new WaitForSeconds(duration);

        StartCoroutine(Enable());
    }

    private void Draw()
    {
        if (_auraEffectPrefab != null)
        {
            _auraEffectPrefab.transform.localScale = new Vector2(_ability.Radius, _ability.Radius);

            _auraEffectInstance = Instantiate(_auraEffectPrefab, transform.position, Quaternion.identity, this.transform);
            _auraEffectInstance.gameObject.SetActive(false);
        }
    }

    private IEnumerator Enable()
    {
        _auraEffectInstance.gameObject.SetActive(true);

        yield return _abilityDuration;

        _auraEffectInstance.gameObject.SetActive(false);
    }
}
