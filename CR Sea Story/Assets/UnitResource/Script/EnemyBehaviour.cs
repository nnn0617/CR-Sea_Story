using UnityEngine;

public class EnemyBehaviour : ActorsBehaviour
{
    void Start()
    {
        _curState = UnitState.Idle;        
        _moveFlag = false;
        InitAbility();
    }

    protected override void InitAbility()
    {
        _moveRange = 3;
        _attackRange = 1;
    }

    public void EnemyUpdate()
    {
    }
}
