using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CooldownAnimation : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private HealthDrainAura _ability;

    private float _abilityDuration;
    private float _abilityCooldown;

    private void OnEnable()
    {
        _ability.Activated += SetDuration;
    }

    private void OnDisable()
    {
        _ability.Activated -= SetDuration;
    }

    public void SetDuration(float duration, float cooldown)
    {
        _abilityDuration = duration;
        _abilityCooldown = cooldown;

        StartCoroutine(AnimateDuration());
    }

    private IEnumerator AnimateDuration()
    {
        _slider.value = _slider.maxValue;

        float elapsedTime = 0f;

        while (elapsedTime < _abilityDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / _abilityDuration;

            _slider.value = Mathf.Lerp(_slider.maxValue, 0f, progress);

            yield return null;
        }

        _slider.value = 0f;

        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _abilityCooldown)
        {
            elapsedTime += Time.deltaTime;

            _text.text = ((int)(_abilityCooldown - elapsedTime) + 1).ToString();

            yield return null;
        }

        _text.text = "";
    }
}