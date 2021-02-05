using UnityEngine;
using ActorsState;
using DG.Tweening;
using System.Collections.Generic;

public class EnemyBehaviour : ActorsBehaviour
{
    private GameObject[] _playerObjects;
    private List<PlayerBehaviour> _players;
    private Vector3 _distanceToPlayer;

    void Start()
    {
        _type = UnitType.Enemy;
        _isMoving = false;
        InitAbility();

        _players = new List<PlayerBehaviour>();
        _playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in _playerObjects)
        {
            _players.Add(player.GetComponent<PlayerBehaviour>());
        }

        _animator = GetComponent<Animator>();

        _stateProcessor = new StateProcessor();
        StateIdle = new StateIdle();           //待機状態
        StateSelect = new StateSelect();       //選択状態
        StateMove = new StateMove();           //移動状態
        StateAttack = new StateAttack();       //攻撃状態
        StateIntercept = new StateIntercept(); //傍受状態

        _stateProcessor.State = StateIdle;
        StateIdle.execDelegate = IdleUpdate;
        StateSelect.execDelegate = SelectUpdate;
        StateMove.execDelegate = MoveUpdate;
        StateAttack.execDelegate = AttackUpdate;
        StateIntercept.execDelegate = InterceptUpdate;
    }

    protected override void InitAbility()
    {
        _moveRange = 3;
        _attackRange = 1;
        _speed = 5;
        _life = 2;
    }

    public override void UnitUpdate()
    {
        _stateProcessor.Execute();
    }

    private void PlayerUnitSearch()
    {
        foreach (var player in _players)
        {
            Vector3 playerPos = player.transform.position;
            _distanceToPlayer = playerPos - transform.position;
        }
    }

    public void IdleUpdate()
    {
        PlayerUnitSearch();
        //transform.position = _startPos;

        _stateProcessor.State = StateSelect;
        _animator.SetBool("run", false);
        //transform.DOScale(1.3f, 0.5f).SetEase(Ease.OutElastic);
    }

    public void SelectUpdate()
    {
    }

    public void MoveUpdate()
    {
    }

    public void AttackUpdate()
    {
    }

    public void InterceptUpdate()
    {
    }
}
