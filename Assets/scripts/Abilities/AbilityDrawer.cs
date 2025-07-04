using UnityEngine;

[RequireComponent(typeof(HealthDrainAura))]
public class AbilityDrawer : MonoBehaviour
{
    [SerializeField] private Aura _auraEffectPrefab;
    
    private HealthDrainAura _ability;
    private Aura _auraEffectInstance;

    private void OnEnable()
    {
        _ability.DurationRemained += SetDuration;
    }

    private void Awake()
    {
        _ability = GetComponent<HealthDrainAura>();

        Create();
    }

    private void OnDisable()
    {
        _ability.DurationRemained -= SetDuration;
    }

    private void SetDuration(float value, float maxValue)
    {
        if (value == maxValue)
        {
            _auraEffectInstance.gameObject.SetActive(false);
        }
        else 
        {
            _auraEffectInstance.gameObject.SetActive(true);
        }
    }

    private void Create()
    {
        if (_auraEffectPrefab != null)
        {
            _auraEffectPrefab.transform.localScale = new Vector2(_ability.Radius, _ability.Radius);

            _auraEffectInstance = Instantiate(_auraEffectPrefab, transform.position, Quaternion.identity, this.transform);
            _auraEffectInstance.gameObject.SetActive(false);
        }
    }
}
