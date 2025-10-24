using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GuardStateController))]
public class GuardAI : MonoBehaviour
{
    protected private NavMeshAgent m_agent;

    [SerializeField] public TargetPoint TargetPointTransform;

    protected GuardStateController m_stateController;
    public GuardStateController StateController
    {
        get { return m_stateController; }
    }

    [Header("Min Distance to Player")]
    [SerializeField] private float m_minDistanceToPlayer = 100f;
    public float MinDistanceToPlayer => m_minDistanceToPlayer;

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
        if (StateController.CurrentState == EGuardStates.PATROL)
        {
            if (TargetPointTransform &&
                TargetPointTransform.gameObject.activeInHierarchy)
                    m_agent.destination = 
                    TargetPointTransform.transform.position;
        }
    }

    private void OnEnable()
    {

    }

    private void Init()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_stateController = GetComponent<GuardStateController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) { return; }

        if (other.TryGetComponent(out CharacterController player))
        {
            m_agent.destination = other.transform.position;
            StateController.SetChase();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out CharacterController player))
        {
            m_agent.destination = other.transform.position;

            if (PlayerIsClose(m_minDistanceToPlayer, other.transform))
            {
                StateController.SetAttack();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null) { return; }

        if (other.TryGetComponent(out CharacterController player))
        {
            m_agent.destination = TargetPointTransform.transform.position;
            StateController.SetPatrol();
        }
    }

    private float EvaluateDistance(Transform target)
    {
        return Vector3.Distance(transform.localPosition, target.localPosition);
    }

    private bool PlayerIsClose(float distance, Transform target)
    {
        if (target.TryGetComponent(out CharacterController character))
        {
            if (EvaluateDistance(character.transform) <= distance)
            {
                return true;
            }
        }
        
        return false;

    }

}
