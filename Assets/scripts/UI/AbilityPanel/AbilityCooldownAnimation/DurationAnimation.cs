using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DurationAnimation : MonoBehaviour
{
    [SerializeField] private Slider _sliderDuration;
    [SerializeField] private HealthDrainAura _ability;

    private Coroutine _smoothSlider;

    private bool _isSmoothing = false;
    private float _currentDisplayValue;

    private float _smoothTime = 0.006f;

    private void OnEnable()
    {
        _ability.DurationRemained += ChangeDurationValue;
    }

    private void OnDisable()
    {
        _ability.DurationRemained -= ChangeDurationValue;
    }

    private void ChangeDurationValue(float currentValue, float maxValue)
    {
        if (_sliderDuration.value == _sliderDuration.minValue)
        {
            ResetSliderValue();
        }

        if (_isSmoothing)
        {
            StopCoroutine(_smoothSlider);
        }

        float targetValue = (1 - currentValue / maxValue);

        _smoothSlider = StartCoroutine(SmoothValueUpdate(targetValue));
    }

    private IEnumerator SmoothValueUpdate(float value)
    {
        _isSmoothing = true;

        while (!Mathf.Approximately(_currentDisplayValue, value))
        {
            _currentDisplayValue = Mathf.MoveTowards(_currentDisplayValue, value, _smoothTime);

            _sliderDuration.value = _currentDisplayValue;

            yield return null;
        }

        _currentDisplayValue = value;
        _sliderDuration.value = _currentDisplayValue;

        _isSmoothing = false;
    }

    private void ResetSliderValue()
    {
        _sliderDuration.value = _sliderDuration.maxValue;

        _currentDisplayValue = _sliderDuration.value;
    }
}
