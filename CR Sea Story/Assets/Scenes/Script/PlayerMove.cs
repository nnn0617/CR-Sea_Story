using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    private bool _selFlag;
    private bool _moveFlag;
    private Vector3Int _mousePos;
    private Vector3 _movePos;
    private float _speed;

    void Start()
    {
        _selFlag = false;
        _moveFlag = false;
        _speed = 5.0f;
    }

    void Update()
    {
        if (_moveFlag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mousePos = RoundToPosition(Input.mousePosition);
                Debug.Log("_mousePos : " + _mousePos);
                _movePos = _mousePos - this.transform.position;
                Debug.Log("_movePos : " + _movePos);
            }
        }
    }

    private void OnMouseDown()
    {
        transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutElastic);
        _selFlag = true;
    }

    private void OnMouseOver()
    {
        _moveFlag = false;
    }

    private Vector3Int RoundToPosition(Vector3 position)
    {
        //小数点以下を四捨五入 & 100マス単位に調整
        Vector3Int afterPosition = new Vector3Int(
            (int)Mathf.RoundToInt(position.x) / 100 * 100,
            (int)Mathf.RoundToInt(position.y) / 100 * 100,
            (int)Mathf.RoundToInt(position.z) / 100 * 100);

        return afterPosition;
    }
}
