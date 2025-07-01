using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    protected EntityHealth EntityHealth;

    protected virtual void OnEnable()
    {
        if (EntityHealth != null)
        {
            EntityHealth.ChangedHealth += UpdateHealthInfo;

            UpdateHealthInfo(EntityHealth.Value);
        }
    }

    protected virtual void OnDisable()
    {
        if (EntityHealth != null)
        {
            EntityHealth.ChangedHealth -= UpdateHealthInfo;
        }
    }

    protected abstract void UpdateHealthInfo(int health);
}