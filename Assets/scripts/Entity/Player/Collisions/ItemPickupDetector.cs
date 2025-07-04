using UnityEngine;

public class ItemPickupDetector : MonoBehaviour
{
    private EntityHealth _health;

    private void Awake()
    {
        _health = GetComponent<EntityHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collector = new ItemCollector(_health);

        if (other.TryGetComponent<ICollectibleItem>(out var item))
        {
            item.Accept(collector);
        }
    }
}