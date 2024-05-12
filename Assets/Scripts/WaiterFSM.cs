using System.Collections;
using UnityEngine;

public class WaiterFSM : MonoBehaviour
{
    protected WaiterStateFSM currentState;

    // Properties for each state
    public WaiterStateFSM Idle { get; private set; }
    public WaiterStateFSM TableAssignment { get; private set; }
    public WaiterStateFSM CustomerService { get; private set; }
    public WaiterStateFSM RefillService { get; private set; }
    public WaiterStateFSM ClearTable { get; private set; }
    public WaiterStateFSM Serving { get; private set; }
    public WaiterStateFSM Performance { get; private set; }
    public WaiterStateFSM OrderTaking { get; private set; }
    public WaiterStateFSM EnquiryTaking { get; private set; }
    public WaiterStateFSM RecommendationTaking { get; private set; }
    public WaiterStateFSM Apology { get; private set; }

    void Start()
    {
        // Initialize states

        // Idle state
        Idle = new IdleState(this);

        // Table assignment state
        TableAssignment = new TableAssignmentState(this);

        // Customer servicing states
        CustomerService = new CustomerServiceState(this);
        RefillService = new RefillState(this);

        // Customer table service states
        ClearTable = new ClearTableState(this);
        Serving = new ServingState(this);
        Performance = new PerformanceState(this);

        // Order taking states
        OrderTaking = new OrderTakingState(this);
        EnquiryTaking = new EnquiryState(this);
        RecommendationTaking = new RecommendationState(this);
        Apology = new ApologyState(this);

        // Initialize the current state to IdleState
        currentState = Idle;
        
    }

    void Update()
    {
        if (currentState != null)
            currentState.Execute();
    }

    public void ChangeState(WaiterStateFSM nextState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = nextState;
        currentState.Enter();
    }
}
