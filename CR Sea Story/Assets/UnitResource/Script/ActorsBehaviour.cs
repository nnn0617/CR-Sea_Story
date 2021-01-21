using UnityEngine;

public abstract class ActorsBehaviour : MonoBehaviour
{
    //ユニットの状態
    public enum UnitState
    {
        Idle,       //待機状態
        Select,     //選択状態
        Move,       //移動状態
        Attack,     //攻撃状態
        Intercept   //迎撃状態(相手のターン)
    }

    //ユニットのタイプ
    public enum UnitType
    {
        Player, //プレイヤー
        Enemy   //敵
    }

    protected UnitState _curState;//ユニットの現在の状態
    protected UnitType _type;

    protected Vector3 _diffPos;//移動先の座標
    protected Vector3 _actionVec;//行動範囲ベクトル
    protected Vector3 _moveX = new Vector3(1.0f, 0f, 0f);
    protected Vector3 _moveY = new Vector3(0f, 1.0f, 0f);
    protected float _speed;

    protected int _moveRange;//移動範囲
    protected int _attackRange;//攻撃範囲

    protected bool _isSelecting;//選択
    protected bool _isMoving;//移動
    protected bool _isLeft;//

    //行動範囲取得用
    public int moveRange { get { return _moveRange; } }
    public int attackRange { get { return _attackRange; } }

    public bool GetSelectionFlag { get { return _curState == UnitState.Select; } }
    public bool GetAttackFlag { get { return _curState == UnitState.Attack; } }
    public int GetUnitState { get { return (int)_curState; } set { _curState = (UnitState)value; } }
    public int GetUnitType { get { return (int)_type; } }

    protected abstract void InitAbility();//パラメータ初期化
    public abstract bool CheckType(UnitType type);//ユニットのタイプ確認

    public ActorsBehaviour(int move = 2, int attack = 1)
    {
        this._moveRange = move;
        this._attackRange = attack;
    }

    abstract public void UnitUpdate();//継承元の更新処理

    void Start()
    {
    }

    void Update()
    {        
    }

    //行動範囲チェック(範囲外…true、範囲内…false)
    protected virtual bool CheckDifference(int actionRange, Vector3Int diffPos, Vector3Int startPos)
    {
        //移動距離の計算
        _diffPos = diffPos - startPos;
        _diffPos.z = 0f;

        float range = Mathf.Abs(_diffPos.x) + Mathf.Abs(_diffPos.y);
        if (range > actionRange)
        {
            return true;
        }
        return false;
    }

    //float型の小数点以下を四捨五入し、int(整数)型に
    protected Vector3Int RoundToPosition(Vector3 position)
    {
        Vector3Int afterPosition = new Vector3Int(
            Mathf.RoundToInt(position.x),
            Mathf.RoundToInt(position.y),
            Mathf.RoundToInt(position.z));

        return afterPosition;
    }
}
