using UnityEngine;
using UnityEngine.UI;
[ExecuteAlways]
public class GridPixelPerfect : MonoBehaviour
{
    private Grid grid;
    private Vector3 lastPosition;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        if (transform.position != lastPosition && grid != null)
        {
            SnapToGrid();
            lastPosition = transform.position;
        }
    }

    private void SnapToGrid()
    {
        Vector3Int cellPosition = grid.WorldToCell(transform.position);
        transform.position = grid.GetCellCenterWorld(cellPosition);
    }
}
