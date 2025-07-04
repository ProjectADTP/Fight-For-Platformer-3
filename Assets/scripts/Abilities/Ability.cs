using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected float Cooldown = 4f;
    [SerializeField] protected float Duration = 6f;

    protected bool IsReady = true;

    public abstract void TryActivateAbility();
}
