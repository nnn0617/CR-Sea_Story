﻿using UnityEngine;

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
        _moveRange = 2;
        _attackRange = 1;
    }

    public void EnemyUpdate()
    {
    }
}
