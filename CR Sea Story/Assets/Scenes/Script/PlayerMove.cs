using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    enum UnitState
    {
        Idele_State,
        Select_State,
        Move_State
    }

    UnitState _curState;

    private bool _selFlag;
    private bool _moveFlag;

    private Vector3Int _mousePos;
    private Vector3 _moveVec;

    void Start()
    {
        _curState = UnitState.Idele_State;
        _selFlag = false;
        _moveFlag = false;
    }

    void Update()
    {
        switch (_curState)
        {
            case UnitState.Idele_State:
                if (_selFlag && Input.GetMouseButtonDown(0))
                {                    
                    _curState = UnitState.Select_State;
                }
                break;

            case UnitState.Select_State:
                if (!_moveFlag)
                {
                    if (_selFlag && Input.GetMouseButtonDown(0))
                    {
                        Vector3Int integerPos = RoundToPosition(transform.position);

                        //マウスカーソルの座標取得
                        _mousePos = RoundToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                        _mousePos.z = 0;

                        //移動距離の計算
                        _moveVec = _mousePos - integerPos;

                        _moveFlag = true;
                        _curState = UnitState.Move_State;
                    }
                }
                break;

            case UnitState.Move_State:
                transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic);

                //ユニットの移動
                MoveToDestination();
                break;
        }

        if (Input.GetMouseButtonDown(1))
        {
            transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic);
            _curState = UnitState.Idele_State;
        }
    }

    void MoveToDestination()
    {
        Vector3Int integerPos = RoundToPosition(transform.position);

        if (integerPos == _mousePos)
        {
            _selFlag = _moveFlag = false;
            _curState = UnitState.Idele_State;
        }

        if (Mathf.Abs(_moveVec.x) > Mathf.Abs(_moveVec.y))
        {
            HorizontalPriorityMove();
        }

        else if (Mathf.Abs(_moveVec.y) > Mathf.Abs(_moveVec.x))
        {
            VerticalPriorityMove();
        }
        else
        {
            HorizontalPriorityMove();
        }
    }

    //横優先移動
    void HorizontalPriorityMove()
    {
        Vector3Int integerPos = RoundToPosition(transform.position);

        if (integerPos.x != _mousePos.x)
        {
            transform.position += new Vector3(1.0f * (_moveVec.x / Mathf.Abs(_moveVec.x)), 0.0f, 0.0f);//横移動
        }
        else
        {
            if (integerPos.y != _mousePos.y)
            {
                transform.position += new Vector3(0.0f, 1.0f * (_moveVec.y / Mathf.Abs(_moveVec.y)), 0.0f);//縦移動
            }
        }
    }

    //縦優先移動
    void VerticalPriorityMove()
    {
        Vector3Int integerPos = RoundToPosition(transform.position);

        if (integerPos.y != _mousePos.y)
        {
            transform.position += new Vector3(0.0f, 1.0f * (_moveVec.y / Mathf.Abs(_moveVec.y)), 0.0f);//縦移動
        }
        else
        {
            if (integerPos.x != _mousePos.x)
            {
                transform.position += new Vector3(1.0f * (_moveVec.x / Mathf.Abs(_moveVec.x)), 0.0f, 0.0f);//横移動
            }
        }
    }

    //マウスカーソルがユニット上でクリックした場合
    private void OnMouseDown()
    {
        if (!_selFlag)
        {
            transform.DOScale(1.3f, 0.5f).SetEase(Ease.OutElastic);
            _selFlag = true;
        }
    }

    //マウスカーソルがユニット上にある場合
    private void OnMouseOver()
    {
        _moveFlag = false;
    }

    //小数点以下を四捨五入し、整数型に
    private Vector3Int RoundToPosition(Vector3 position)
    {
        Vector3Int afterPosition = new Vector3Int(
            (int)Mathf.RoundToInt(position.x),
            (int)Mathf.RoundToInt(position.y),
            (int)Mathf.RoundToInt(position.z));

        return afterPosition;
    }
}
