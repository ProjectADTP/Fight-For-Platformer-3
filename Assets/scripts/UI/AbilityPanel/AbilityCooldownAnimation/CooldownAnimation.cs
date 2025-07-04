using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CooldownAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textColldown;
    [SerializeField] private HealthDrainAura _ability;

    private void OnEnable()
    {
        _ability.CooldownRemained += ChangeCooldownValue;
    }

    private void OnDisable()
    {
        _ability.CooldownRemained -= ChangeCooldownValue;
    }

    private void ChangeCooldownValue(float currentValue, float maxValue)
    {
        _textColldown.text = ((maxValue - currentValue).ToString());

        if (currentValue == maxValue)
        {
            _textColldown.text = "";
        }
    }
}