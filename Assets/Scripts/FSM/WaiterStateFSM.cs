using UnityEngine;

public abstract class WaiterStateFSM
{
    protected WaiterFSM fsm; // Corrected the field type to WaiterFSM

    // Constructor to initialize the state with a reference to the FSM
    public WaiterStateFSM(WaiterFSM fsm)
    {
        this.fsm = fsm;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
