using UnityEngine;

public abstract class ActorsBehaviour : MonoBehaviour
{
    //ユニットの状態
    protected enum UnitState
    {
        Idle,   //待機状態
        Select, //選択状態
        Move,   //移動状態
        Attack  //攻撃状態
    }

    protected UnitState _curState;    //ユニットの現在の状態

    protected int _moveRange;
    protected int _attackRange;

    protected Vector3 _diffPos;       //移動ベクトル

    protected bool _selectFlag;
    protected bool _moveFlag;

    public int moveRange { get { return _moveRange; } }
    public int attackRange { get { return _attackRange; } }

    public bool GetSelectionFlag { get { return _curState == UnitState.Select; } }
    public bool GetAttackFlag { get { return _curState == UnitState.Attack; } }

    protected abstract void InitAbility();//パラメータ初期化

    public ActorsBehaviour(int move = 2, int attack = 1)
    {
        this._moveRange = move;
        this._attackRange = attack;
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    //行動範囲チェック(範囲外…true、範囲内…false)
    protected bool CheckDifference(int actionRange, Vector3Int diffPos, Vector3Int startPos)
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
