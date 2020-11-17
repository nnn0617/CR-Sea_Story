﻿using UnityEngine;

public abstract class ActorsBehaviour : MonoBehaviour
{
    //ユニットの状態
    protected enum UnitState
    {
        Idle,       //待機状態
        Select,     //選択状態
        Move,       //移動状態
        Attack,     //攻撃状態
        Intercept   //迎撃状態(相手のターン)
    }

    protected UnitState _curState;//ユニットの現在の状態

    protected Vector3 _diffPos;//移動先の座標
    protected Vector3 _actionVec;//行動範囲ベクトル

    protected int _moveRange;//移動範囲
    protected int _attackRange;//攻撃範囲

    protected bool _isMyTurn;//ターン
    protected bool _isSelecting;//選択
    protected bool _isMoving;//移動

    //行動範囲取得用
    public int moveRange { get { return _moveRange; } }
    public int attackRange { get { return _attackRange; } }

    //自分のターンかどうかのフラグ取得、代入用
    public bool SharedIsMyTurn { get { return _isMyTurn; } set { _isMyTurn = value; } }

    public bool GetSelectionFlag { get { return _curState == UnitState.Select; } }
    public bool GetAttackFlag { get { return _curState == UnitState.Attack; } }

    public int GetUnitState { get { return (int)_curState; } set { _curState = (UnitState)value; } }

    protected abstract void InitAbility();//パラメータ初期化

    public ActorsBehaviour(int move = 2, int attack = 1)
    {
        this._moveRange = move;
        this._attackRange = attack;
    }

    abstract public void UnitUpdate();//継承元更新処理

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
