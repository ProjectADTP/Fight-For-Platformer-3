using UnityEngine;

public class PanelTransform : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 panelPosition = transform.position;

        panelPosition.x = _target.transform.position.x + _startPosition.x;
        panelPosition.y = _startPosition.y;

        transform.position = panelPosition;
    }
}
