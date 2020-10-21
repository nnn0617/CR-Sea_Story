using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    //ユニットの状態
    enum UnitState
    {
        Idle,   //待機状態
        Select, //選択状態
        Move,   //移動状態
        Attack  //攻撃状態
    }

    UnitState _curState;    //ユニットの現在の状態

    private int _moveRange = 3;
    private int _attackRange = 1;

    private Vector3Int _startingPos;   //選択時のユニット座標
    private Vector3Int _mousePos;      //マウスカーソル座標
    private Vector3 _moveVec;          //移動ベクトル

    private bool _moveFlag;
    private float _clickTime;

    public int moveRange { get { return _moveRange; } }
    public int attackRange { get { return _attackRange; } }

    public Vector3Int GetMouseClickPos { get { return _mousePos; } }

    public bool GetSelectionFlag { get { return _curState == UnitState.Select; } }
    public bool GetAttackFlag { get { return _curState == UnitState.Attack; } }

    void Start()
    {
        _curState = UnitState.Idle;
        _moveFlag = false;
    }

    void Update()
    {
        switch (_curState)
        {
            case UnitState.Idle:
                //クリックして離した瞬間にSelect_Stateに移行
                if (Input.GetMouseButtonUp(0))
                {
                    if (!_moveFlag)
                    {
                        _curState = UnitState.Select;
                        transform.DOScale(1.3f, 0.5f).SetEase(Ease.OutElastic);
                    }
                }                
                break;

            case UnitState.Select:
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
                        _curState = UnitState.Move;
                    }
                }
                break;

            case UnitState.Move:
                transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic);                
                MoveToDestination();//ユニットの移動
                break;

            case UnitState.Attack:
                if (!_moveFlag)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("攻撃");
                        _curState = UnitState.Idle;
                    }
                }
                break;
        }

        //右クリックで選択キャンセル
        if (Input.GetMouseButtonDown(1))
        {
            _moveFlag = true;
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
        if (_curState == UnitState.Attack)
        {
             _moveFlag = false;
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
            _curState = UnitState.Attack;
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
            _curState = UnitState.Attack;
        }
    }

    //小数点以下を四捨五入し、整数型に
    private Vector3Int RoundToPosition(Vector3 position)
    {
        Vector3Int afterPosition = new Vector3Int(
            Mathf.RoundToInt(position.x),
            Mathf.RoundToInt(position.y),
            Mathf.RoundToInt(position.z));

        return afterPosition;
    }
}
