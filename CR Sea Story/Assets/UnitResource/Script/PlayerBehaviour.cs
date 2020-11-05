using UnityEngine;
using DG.Tweening;

public class PlayerBehaviour : ActorsBehaviour
{
    private Vector3Int _startPos;   //選択時のユニット座標
   
    private float _clickTime;

    //public Vector3Int GetMouseClickPos { get { return _deffPos; } }

    public PlayerBehaviour(int move, int attack):base(move, attack)
    {
        this._moveRange = move;
        this._attackRange = attack;
    }

    void Start()
    {
        _curState = UnitState.Idle;       
        _moveFlag = false;
        _selectFlag = false;
        InitAbility();
    }

    protected override void InitAbility()
    {
        _moveRange = 3;
        _attackRange = 1;
    }

    public void PlayerUpdate()
    {
        switch (_curState)
        {
            case UnitState.Idle:
                //クリックして離した瞬間にSelect_Stateに移行
                if (_selectFlag && Input.GetMouseButtonUp(0))
                {
                    _curState = UnitState.Select;
                    transform.DOScale(1.3f, 0.5f).SetEase(Ease.OutElastic);
                }               
                break;

            case UnitState.Select:
                if (Input.GetMouseButtonDown(0))
                {
                    //クリックタイム取得
                    _clickTime = Time.deltaTime;

                    //移動範囲外の場合
                    if (CheckDifference(
                        _moveRange,
                        RoundToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)),
                        RoundToPosition(transform.position))) break;

                    _moveFlag = true;
                    _curState = UnitState.Move;
                }
                break;

            case UnitState.Move:
                transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic);                
                MoveToDestination();//ユニットの移動
                break;

            case UnitState.Attack:
                if (Input.GetMouseButtonDown(0))
                {
                    //攻撃範囲外の場合
                    if (CheckDifference(
                        _attackRange,
                        RoundToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)),
                        RoundToPosition(transform.position))) break;

                    Debug.Log("攻撃");
                    _curState = UnitState.Idle;
                    _selectFlag = false;
                }
                break;
        }

        //右クリックで選択キャンセル
        if (Input.GetMouseButtonDown(1))
        {
            _moveFlag = true;
            _selectFlag = false;
            transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic);
            _curState = UnitState.Idle;
        }       
    }

    //マウスカーソルがユニット上にある場合
    private void OnMouseOver()
    {
        //選択状態でその場をクリックした場合はMove_Stateに移行しない
        if (_curState == UnitState.Select)
        {
            _moveFlag = false;
            _curState = UnitState.Select;
        }
    }

    private void OnMouseDown()
    {
        if(_curState == UnitState.Idle)
        {
            _selectFlag = true;
        }
        if (_curState == UnitState.Attack)
        {
             _moveFlag = false;
        }
    }

    //private bool DifferenceCheck(int actionRange)
    //{
    //    //マウスカーソルの座標取得
    //    _deffPos = RoundToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //    _deffPos.z = 0;

    //    //移動距離の計算
    //    _startPos = RoundToPosition(transform.position);
    //    _moveVec = _deffPos - _startPos;

    //    float range = Mathf.Abs(_moveVec.x) + Mathf.Abs(_moveVec.y);
    //    if (range > actionRange)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //ユニットの移動
    void MoveToDestination()
    {
        if (Mathf.Abs(_diffPos.x) > Mathf.Abs(_diffPos.y))
        {
            HorizontalPriorityMove();
        }

        else if (Mathf.Abs(_diffPos.y) > Mathf.Abs(_diffPos.x))
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
        //float curClickAfterTime = _clickTime;
        
        if (integerPos.x != _diffPos.x)
        {
            transform.position += new Vector3(1.0f * (_diffPos.x / Mathf.Abs(_diffPos.x)), 0.0f, 0.0f);//横移動
            //transform.position = Vector3.Lerp(_startingPos, _mousePos, Mathf.Abs(_moveVec.x) * curClickAfterTime);
        }
        else if(integerPos.y != _diffPos.y)
        {
            transform.position += new Vector3(0.0f, 1.0f * (_diffPos.y / Mathf.Abs(_diffPos.y)), 0.0f);//縦移動
            //transform.position = Vector3.Lerp(_startingPos, _mousePos, Mathf.Abs(_moveVec.x) * curClickAfterTime);
        }
        else
        {          
            _curState = UnitState.Attack;
        }

　　　　//_clickTime ＋＝ Time.deltaTime；
        //_clickTime = Mathf.Clamp(_clickTime, float.MinValue, 1.0f);
    }

    //縦優先移動
    void VerticalPriorityMove()
    {
        Vector3Int integerPos = RoundToPosition(transform.position);

        if (integerPos.y != _diffPos.y)
        {
            transform.position += new Vector3(0.0f, 1.0f * (_diffPos.y / Mathf.Abs(_diffPos.y)), 0.0f);//縦移動
        }
        else if (integerPos.x != _diffPos.x)
        {
            transform.position += new Vector3(1.0f * (_diffPos.x / Mathf.Abs(_diffPos.x)), 0.0f, 0.0f);//横移動
        }
        else
        {
            _curState = UnitState.Attack;
        }
    }
}
