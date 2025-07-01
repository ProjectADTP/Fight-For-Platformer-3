using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    [Header("Настройки способности")]
    [SerializeField] protected float Cooldown = 4f;
    [SerializeField] protected float Duration = 6f;

    [Header("Кнопка активации")]

    [SerializeField] protected Button Button;

    protected bool IsReady = true;

    protected void OnEnable()
    {
        Button.onClick.AddListener(TryActivateAbility);
    }

    protected void OnDisable()
    {
        Button.onClick.RemoveListener(TryActivateAbility);
    }

    protected abstract void TryActivateAbility();
}
