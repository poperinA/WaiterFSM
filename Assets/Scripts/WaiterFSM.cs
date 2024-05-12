using UnityEngine;

public class WaiterFSM : WaiterStateFSM
{
    protected WaiterStateFSM currentState;

    public WaiterFSM() : base(null) { } // Add constructor for WaiterFSM


    void Start()
    {
        //initialising states

        //idle state
        IdleState idleState = new IdleState(this);

        //table assignment state
        TableAssignmentState tableAssignmentState = new TableAssignmentState(this);

        //customer servicing states
        CustomerServiceState customerServiceState = new CustomerServiceState(this);
        RefillState refillServiceState = new RefillState(this);

        //customer table service states
        ClearTableState clearTableState = new ClearTableState(this);
        ServingState servingState = new ServingState(this);
        PerformanceState performanceState = new PerformanceState(this);

        //order taking states
        OrderTakingState orderTakingState = new OrderTakingState(this);
        EnquiryState enquiryTakingState = new EnquiryState(this);
        RecommendationState recommendationTakingState = new RecommendationState(this);
        ApologyState apologyState = new ApologyState(this);

        // Initialize the current state to IdleState
        TransitionToState(idleState);

    }

    void Update()
    {
        if (currentState != null)
            currentState.Execute();
    }

    public void TransitionToState(WaiterStateFSM nextState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = nextState;
        currentState.Enter();
    }

}
