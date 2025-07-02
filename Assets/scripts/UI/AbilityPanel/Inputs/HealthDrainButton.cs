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
        _button.onClick.RemoveListener(ActivateAblility);
    }

    private void ActivateAblility()
    {
        _ability.TryActivateAbility();
    }
}