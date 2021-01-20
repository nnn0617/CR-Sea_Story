using UnityEngine;
using DG.Tweening;

public class PlayerBehaviour : ActorsBehaviour
{
    private Vector3Int _startPos;   //選択時のユニット座標
    private float _clickTime;

    public PlayerBehaviour(int move, int attack):base(move, attack)
    {
        this._moveRange = move;
        this._attackRange = attack;
    }

    void Start()
    {
        _curState = UnitState.Idle;
        _type = UnitType.Player;
        InitAbility();
    }

    protected override void InitAbility()
    {
        _isMoving = false;
        _isSelecting = false;
        _isMyTurn = false;
        _moveRange = 3;
        _attackRange = 1;
        _speed = 8;
    }

    public override bool CheckType(UnitType type)
    {
        return _type == type;
    }

    public override void UnitUpdate()
    {
        switch (_curState)
        {
            case UnitState.Idle:
                //クリックして離した瞬間にSelect_Stateに移行
                if (_isSelecting && Input.GetMouseButtonUp(0))
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
                    if (CheckDifference(_moveRange)) break;

                    _isMoving = true;
                    _curState = UnitState.Move;
                }
                break;

            case UnitState.Move:
                MoveToDestination();//ユニットの移動
                break;

            case UnitState.Attack:
                transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic);                
                if (Input.GetMouseButtonDown(0))
                {
                    //攻撃範囲外の場合
                    if (CheckDifference(_attackRange)) break;

                    Debug.Log("攻撃");
                    _curState = UnitState.Intercept;
                    _isMyTurn = false;
                    _isSelecting = false;
                }
                break;

            case UnitState.Intercept:
                
                break;
        }

        //右クリックで選択キャンセル
        if (Input.GetMouseButtonDown(1))
        {
            _isSelecting = false;
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
            _isMoving = false;
            _curState = UnitState.Select;
        }
    }

    private void OnMouseDown()
    {
        if(_curState == UnitState.Idle)
        {
            _isSelecting = true;
        }
        if (_curState == UnitState.Attack)
        {
             _isMoving = false;
        }
    }

    //行動範囲チェック(範囲外…true、範囲内…false)
    private bool CheckDifference(int actionRange)
    {
        //マウスカーソルの座標取得
        _diffPos = RoundToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _diffPos.z = 0;

        //移動距離の計算
        _startPos = RoundToPosition(transform.position);
        _actionVec = _diffPos - _startPos;

        float range = Mathf.Abs(_actionVec.x) + Mathf.Abs(_actionVec.y);
        if (range > actionRange)
        {
            return true;
        }
        return false;
    }

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
        if (transform.position.x != _diffPos.x)
        {
            Vector3 tmpDiffPos = new Vector3(_diffPos.x, transform.position.y, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, tmpDiffPos, Time.deltaTime * _speed);//横移動
        }
        else if (transform.position.y != _diffPos.y)
        {
            Vector3 tmpDiffPos = new Vector3(transform.position.x, _diffPos.y, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, tmpDiffPos, Time.deltaTime * _speed);//縦移動
        }
        else
        {
            _actionVec = new Vector3(0, 0, 0);
            _curState = UnitState.Attack;
        }
    }

    //縦優先移動
    void VerticalPriorityMove()
    {
        if (transform.position.y != _diffPos.y)
        {
            Vector3 tmpDiffPos = new Vector3(transform.position.x, _diffPos.y, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, tmpDiffPos, Time.deltaTime * _speed);//縦移動
        }
        else if (transform.position.x != _diffPos.x)
        {
            Vector3 tmpDiffPos = new Vector3(_diffPos.x, transform.position.y, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, tmpDiffPos, Time.deltaTime * _speed);//横移動
        }
        else
        {
            _actionVec = new Vector3(0, 0, 0);
            _curState = UnitState.Attack;
        }
    }
}
