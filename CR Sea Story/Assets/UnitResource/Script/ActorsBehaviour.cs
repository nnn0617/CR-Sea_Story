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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
