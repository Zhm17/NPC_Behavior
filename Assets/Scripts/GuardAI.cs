using UnityEngine;
using UnityEngine.AI;

public class GuardScript : MonoBehaviour
{
    protected private NavMeshAgent m_agent;
    protected GuardStates m_currentState = GuardStates.PATROL;

    private float distance2Player;

    void Awake()
    {
        OnStateChanged(GuardStates.PATROL);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) { return; }

        if (other.TryGetComponent(out TestPlayer player))
        {
            //TODO
            // CHASE
            OnStateChanged(GuardStates.CHASING);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null) { return; }

        if (other.TryGetComponent(out TestPlayer player))
        {
            //TODO
            // CHASE
            OnStateChanged(GuardStates.PATROL);
        }
    }

    private void OnStateChanged(GuardStates newState)
    {
        m_currentState = newState;
        switch (newState){

            case GuardStates.PATROL:
                PatrolExec();
                break;
            case GuardStates.CHASING:
                ChaseExec ();
                break;
            case GuardStates.ATTACKING:
                AttackExec();
                break;
        }
    }

    private void PatrolExec()
    {
        // REandom target on the navigation area
        // Executing Patrol
    }

    private void ChaseExec()
    {
       
        // Executing Chasing
        //(Changing the target to the player
    }

    private void AttackExec()
    {
        // Executing Attack
    }
}
