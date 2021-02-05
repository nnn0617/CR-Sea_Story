using UnityEngine;
using System;
using System.Collections;
using ActorsState;

public abstract class ActorsBehaviour : MonoBehaviour
{
    protected string _beforeStateName;
    public StateProcessor _stateProcessor;//状態管理

    public StateIdle StateIdle;           //待機状態
    public StateSelect StateSelect;       //選択状態
    public StateMove StateMove;           //移動状態
    public StateAttack StateAttack;       //攻撃状態
    public StateIntercept StateIntercept; //傍受状態
   
    public enum UnitType    //ユニットのタイプ
    {
        Player, //プレイヤー
        Enemy   //敵
    }
    protected UnitType _type;

    protected Vector3 _startPos;//移動開始座標
    protected Vector3 _diffPos;//移動先座標
    protected Vector3 _actionVec;//行動範囲ベクトル
    protected Vector3 _moveX = new Vector3(1.0f, 0f, 0f);
    protected Vector3 _moveY = new Vector3(0f, 1.0f, 0f);
    protected float _speed;

    protected int _moveRange;//移動範囲
    protected int _attackRange;//攻撃範囲

    protected bool _isSelecting;//選択
    protected bool _isMoving;//移動

    protected Animator _animator;//アニメーション管理
    protected float _isRight;//画像横反転用(右向きかどうか)

    //行動範囲取得用
    public int moveRange { get { return _moveRange; } }
    public int attackRange { get { return _attackRange; } }

    //値取得用
    public bool GetSelectionFlag { get { return _stateProcessor.State == StateSelect; } }
    public bool GetAttackFlag { get { return _stateProcessor.State == StateAttack; } }
    public int GetUnitType { get { return (int)_type; } }

    //パラメータ初期化
    protected abstract void InitAbility();

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

        if (range > actionRange) return true;

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
