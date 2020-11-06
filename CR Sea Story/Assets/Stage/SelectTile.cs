using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectTile : MonoBehaviour
{
    [SerializeField] Tilemap _selectPos;
    [SerializeField] TileBase _selectTile;

    Vector3Int _mousePos;
    Vector3Int _lastPos;

    void Update()
    {
        _selectPos.SetTile(_lastPos, null);

        _mousePos = RoundToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _mousePos.z = -7;

        _selectPos.SetTile(_mousePos, _selectTile);
        _lastPos = _mousePos;
    }

    private Vector3Int RoundToPosition(Vector3 position)
    {
        Vector3Int afterPosition = new Vector3Int(
            Mathf.RoundToInt(position.x),
            Mathf.RoundToInt(position.y),
            Mathf.RoundToInt(position.z));

        return afterPosition;
    }
}
