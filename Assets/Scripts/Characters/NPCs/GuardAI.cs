using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GuardStateController))]
public class GuardAI : MonoBehaviour
{
    protected private NavMeshAgent m_agent;
    protected EGuardStates m_currentState = EGuardStates.PATROL;

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
        if(TargetTransform && 
            TargetTransform.gameObject.activeInHierarchy)
                m_agent.destination = TargetTransform.position;
    }

    private void OnEnable()
    {
        //OnStateChanged(EGuardStates.PATROL);
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
            m_agent.destination = other.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out TestPlayer player))
        {
            m_agent.destination = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null) { return; }

        if (other.TryGetComponent(out TestPlayer player))
        {
            m_agent.destination = TargetTransform.position;
        }
    }

    private float EvaluateDistance(Transform target)
    {
        return Vector3.Distance(transform.localPosition, target.localPosition);
    }

    private bool IsClose(float distance, Transform target)
    {
        if(EvaluateDistance(target) <= distance)
        {
            return true;
        }

        return false;
    }

}
