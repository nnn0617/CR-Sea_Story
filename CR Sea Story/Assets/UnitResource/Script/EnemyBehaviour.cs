using UnityEngine;
using ActorsState;

public class EnemyBehaviour : ActorsBehaviour
{
    [SerializeField] private PlayerBehaviour _player;

    void Start()
    {
        _type = UnitType.Enemy;
        _isMoving = false;
        InitAbility();

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
    }

    public override void UnitUpdate()
    {
    }

    private void PlayerUnitSearch()
    {
    }

    public void IdleUpdate()
    {
        
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
