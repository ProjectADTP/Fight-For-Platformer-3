using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HealthDrainButton : MonoBehaviour
{
    [SerializeField] private KeyCode _key = KeyCode.Q;
    [SerializeField] private HealthDrainAura _ability;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _ability.CooldownRemained += TryDeactivateInteractable;
        _ability.DurationRemained += TryActivateInteractable;

        _button.onClick.AddListener(ActivateAblility);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_key) && _button.interactable)
        {
            _button.onClick.Invoke();
        }
    }

    private void OnDisable()
    {
        _ability.CooldownRemained -= TryDeactivateInteractable;
        _ability.DurationRemained -= TryActivateInteractable;

        _button.onClick.RemoveListener(ActivateAblility);
    }

    private void ActivateAblility()
    {
        _ability.TryActivateAbility();
    }

    private void TryActivateInteractable(float _, float __)
    {
        if (_button.interactable)
        {
            _button.interactable = false;
        }
    }

    private void TryDeactivateInteractable(float value, float maxValue)
    {
        if (value == maxValue && _button.interactable == false)
        {
            _button.interactable = true;
        }
    }
}