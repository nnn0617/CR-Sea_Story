using System.Collections;
using UnityEngine;

namespace ActorsState
{
    public class StateProcessor //実行管理クラス
    {
        private ActorsState _state;
        public ActorsState State
        {
            set { _state = value; }
            get { return _state; }
        }
        public void Execute()
        {
            State.Execute();
        }
    }

    public abstract class ActorsState
    {
        public delegate void ExecuteState();
        public ExecuteState execDelegate;

        public virtual void Execute()
        {
            if(execDelegate != null)
            {
                execDelegate();
            }
        }

        public abstract string GetStateName();
    }

    public class StateIdle : ActorsState
    {
        public override string GetStateName()
        {
            return "待機状態";
        }
    }

    public class StateSelect : ActorsState
    {
        public override string GetStateName()
        {
            return "選択状態";
        }
    }

    public class StateMove : ActorsState
    {
        public override string GetStateName()
        {
            return "移動状態";
        }
    }

    public class StateAttack : ActorsState
    {
        public override string GetStateName()
        {
            return "攻撃状態";
        }
    }

    public class StateIntercept : ActorsState
    {
        public override string GetStateName()
        {
            return "迎撃状態";
        }
    }
}