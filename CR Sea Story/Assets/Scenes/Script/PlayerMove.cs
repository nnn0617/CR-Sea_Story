﻿using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    enum UnitState
    {
        Idele_State,//待機状態
        Select_State,//選択状態
        Move_State//移動状態
    }

    UnitState _curState;//ユニットの現在の状態

    private Vector3Int _startingPos;//選択時のユニット座標
    private Vector3Int _mousePos;//マウスカーソル座標
    private Vector3 _moveVec;//移動ベクトル

    private bool _moveFlag;
    private float _clickTime;

    public Vector3Int GetMouseClickPos { get { return _mousePos; } }
    public bool GetSelectionFlag { get { return _curState == UnitState.Select_State; } }

    void Start()
    {
        _curState = UnitState.Idele_State;
        _moveFlag = false;
    }

    void Update()
    {
        switch (_curState)
        {
            case UnitState.Idele_State:
                //クリックして離した瞬間にSelect_Stateに移行
                if (Input.GetMouseButtonUp(0))
                {
                    if (!_moveFlag)
                    {
                        _curState = UnitState.Select_State;
                        transform.DOScale(1.3f, 0.5f).SetEase(Ease.OutElastic);
                    }
                }                
                break;

            case UnitState.Select_State:
                if (!_moveFlag)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //クリックタイム取得
                        _clickTime = Time.deltaTime;

                        //マウスカーソルの座標取得
                        _mousePos = RoundToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                        _mousePos.z = 0;

                        //移動距離の計算
                        _startingPos = RoundToPosition(transform.position);
                        _moveVec = _mousePos - _startingPos;

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

        //右クリックで選択キャンセル
        if (Input.GetMouseButtonDown(1))
        {
            transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic);
            _curState = UnitState.Idele_State;
        }
    }

    //マウスカーソルがユニット上にある場合
    private void OnMouseOver()
    {
        //選択状態でその場をクリックした場合はMove_Stateに移行しない
        if (_curState == UnitState.Select_State)
        {
            _moveFlag = false;
            _curState = UnitState.Select_State;
        }

        if(_curState == UnitState.Idele_State)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _moveFlag = false;
            }
        }
    }

    //ユニットの移動
    void MoveToDestination()
    {
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
        float curClickAfterTime = _clickTime;

        
        if (integerPos.x != _mousePos.x)
        {
            transform.position += new Vector3(1.0f * (_moveVec.x / Mathf.Abs(_moveVec.x)), 0.0f, 0.0f);//横移動
            //transform.position = Vector3.Lerp(_startingPos, _mousePos, Mathf.Abs(_moveVec.x) * curClickAfterTime);
        }
        else if(integerPos.y != _mousePos.y)
        {
            transform.position += new Vector3(0.0f, 1.0f * (_moveVec.y / Mathf.Abs(_moveVec.y)), 0.0f);//縦移動
            //transform.position = Vector3.Lerp(_startingPos, _mousePos, Mathf.Abs(_moveVec.x) * curClickAfterTime);
        }
        else
        {          
            _curState = UnitState.Idele_State;
        }

        _clickTime += Time.deltaTime;
        _clickTime = Mathf.Clamp(_clickTime, float.MinValue, 1.0f);
    }

    //縦優先移動
    void VerticalPriorityMove()
    {
        Vector3Int integerPos = RoundToPosition(transform.position);

        if (integerPos.y != _mousePos.y)
        {
            transform.position += new Vector3(0.0f, 1.0f * (_moveVec.y / Mathf.Abs(_moveVec.y)), 0.0f);//縦移動
        }
        else if (integerPos.x != _mousePos.x)
        {
            transform.position += new Vector3(1.0f * (_moveVec.x / Mathf.Abs(_moveVec.x)), 0.0f, 0.0f);//横移動
        }
        else
        {
            _moveFlag = false;
            _curState = UnitState.Idele_State;
        }
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
