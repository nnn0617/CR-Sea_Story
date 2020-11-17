using UnityEngine;

public class EnemyBehaviour : ActorsBehaviour
{
    void Start()
    {
        _curState = UnitState.Idle;        
        _isMoving = false;
        _isMyTurn = false;
        InitAbility();
    }

    protected override void InitAbility()
    {
        _moveRange = 3;
        _attackRange = 1;
    }

    public override void UnitUpdate()
    {
    }
}
