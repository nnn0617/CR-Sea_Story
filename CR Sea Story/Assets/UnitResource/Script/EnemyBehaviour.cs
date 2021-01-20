using UnityEngine;

public class EnemyBehaviour : ActorsBehaviour
{
    void Start()
    {
        _curState = UnitState.Idle;
        _type = UnitType.Enemy;
        _isMoving = false;
        _isMyTurn = false;
        InitAbility();
    }

    protected override void InitAbility()
    {
        _moveRange = 3;
        _attackRange = 1;
    }

    public override bool CheckType(UnitType type)
    {
        return _type == type;
    }

    public override void UnitUpdate()
    {
    }
}
