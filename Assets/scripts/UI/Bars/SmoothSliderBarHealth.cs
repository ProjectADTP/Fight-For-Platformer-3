using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]
public class SmoothSliderBarHealth : HealthBar
{
    private Slider _healthSlider;
    private float _currentDisplayHealth;
    private float _ratio;
    private float _smoothTime = 0.0125f;

    private Coroutine _smoothCoroutine;
    private bool _isSmoothing;

    private void Awake()
    {
        EntityHealth = GetComponentInParent<EntityHealth>();
        _healthSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (EntityHealth != null)
        {
            _ratio = EntityHealth.MaxValue;
            _currentDisplayHealth = EntityHealth.Value / _ratio;

            UpdateHealthInfo(EntityHealth.Value);
        }
    }

    protected override void UpdateHealthInfo(int health)
    {
        if (_isSmoothing)
        {
            StopCoroutine(_smoothCoroutine);
        }

        _smoothCoroutine = StartCoroutine(SmoothHealthUpdate(health));
    }

    private IEnumerator SmoothHealthUpdate(int health)
    {
        _isSmoothing = true;

        float targetValue = health / _ratio;

        while (!Mathf.Approximately(_currentDisplayHealth, targetValue))
        {
            _currentDisplayHealth = Mathf.MoveTowards(_currentDisplayHealth,targetValue,
                                                      _smoothTime * Time.deltaTime * _ratio);

            _healthSlider.value = _currentDisplayHealth;

            yield return null;
        }

        _currentDisplayHealth = targetValue;
        _healthSlider.value = _currentDisplayHealth;
        _isSmoothing = false;
    }
}
