using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    [Header("��������� �����������")]
    [SerializeField] protected float Cooldown = 4f;
    [SerializeField] protected float Duration = 6f;

    [Header("������ ���������")]

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
