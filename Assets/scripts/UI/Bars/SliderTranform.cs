using UnityEngine;

public class SliderTranform : SmoothSliderBarHealth
{
    [SerializeField] private Vector3 _offset = new Vector3(0, 0.3f, 0);

    private Transform _target;

    private void LateUpdate()
    {
        if (_target == null)
        {
            if (EntityHealth == null)
            {
                Destroy(gameObject);
            }

            return;
        }

        transform.position = _target.position + _offset;
    }

    public void SetTarget(Transform target)
    {
        _target = target;

        if (_target != null)
        {
            transform.position = _target.position + _offset;
        }
    }
}
