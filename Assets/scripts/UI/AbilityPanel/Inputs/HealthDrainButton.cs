using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[RequireComponent(typeof(Button))]
public class HealthDrainButton : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    private float _abilityDuration;
    private float _abilityCooldown;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetDuration(float duration, float cooldown)
    {
        _abilityDuration = duration;
        _abilityCooldown = cooldown;

        StartCoroutine(AnimateDuration());
    }

    private IEnumerator AnimateDuration()
    {
        _button.interactable = false;

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

        _button.interactable = true; 
    }
}