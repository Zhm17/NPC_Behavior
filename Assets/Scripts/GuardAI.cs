using UnityEngine;
using UnityEngine.AI;

public class GuardScript : MonoBehaviour
{
    protected private NavMeshAgent m_agent;
    protected GuardStates m_currentState = GuardStates.PATROL;

    private float m_distance2Player;

    [SerializeField] public Transform TargetTransform;

    void Awake()
    {
        Init();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        if(TargetTransform)
            m_agent.destination = TargetTransform.position;
    }

    private void OnEnable()
    {
        OnStateChanged(GuardStates.PATROL);
    }

    private void Init()
    {
        m_agent = GetComponent<NavMeshAgent>();
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
        switch (newState)
        {
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
        // Random target on the navigation area
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
