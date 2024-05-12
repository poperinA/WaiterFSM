using UnityEngine;

public class WaiterFSM : MonoBehaviour
{
    protected WaiterStateFSM currentState;

    public void TransitionToState(WaiterStateFSM nextState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = nextState;
        currentState.Enter();
    }

    void Update()
    {
        if (currentState != null)
            currentState.Execute();
    }
}
