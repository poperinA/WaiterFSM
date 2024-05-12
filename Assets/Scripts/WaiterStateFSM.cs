using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterStateFSM : MonoBehaviour
{
    protected WaiterStateFSM stateMachine;

    public WaiterStateFSM(WaiterStateFSM stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}
