using Generics;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GuardStateController))]
[RequireComponent(typeof(NavMeshAgent))]
public class GuardAI : MonoBehaviour
{
    protected private NavMeshAgent m_agent;
    public NavMeshAgent Agent 
    {
        get { return m_agent; }
        private set { m_agent = value; }
    }


    [SerializeField] private TargetPoint m_randomPointTransform;
    private TargetPoint RandomPointTransform => m_randomPointTransform;


    protected GuardStateController m_stateController;
    public GuardStateController StateController
    {
        get { return m_stateController; }
        protected set { m_stateController = value; }
    }


    [Header("Min Distance to Player")]
    [SerializeField] private float m_minDistanceToPlayer = 5f;
    public float MinDistanceToPlayer => m_minDistanceToPlayer;


    [Header("Character Detector")]
    [SerializeField] private CharacterDetector m_characterDetector;
    public CharacterDetector CharacterDetector => m_characterDetector;


    [Header("Delay After Attack")]
    [SerializeField] private float m_delaySeconds = 2f;
    public float AttackDelaySeconds => m_delaySeconds;


    private void OnEnable()
    {
        CharacterDetector.OnCharacterOutOfSight += OnCharacterPatrol;
        CharacterDetector.OnCharacterDetected += OnCharacterDetected;
        CharacterDetector.OnCharacterIsClose += OnCharacterAttack;

        TargetPoint.OnTargetRelocated += SetNewRandomTargetPoint;
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        CharacterDetector.OnCharacterOutOfSight -= OnCharacterPatrol;
        CharacterDetector.OnCharacterDetected -= OnCharacterDetected;
        CharacterDetector.OnCharacterIsClose -= OnCharacterAttack;

        TargetPoint.OnTargetRelocated -= SetNewRandomTargetPoint;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();

        CharacterDetector.OnCharacterOutOfSight -= OnCharacterPatrol;
        CharacterDetector.OnCharacterDetected -= OnCharacterDetected;
        CharacterDetector.OnCharacterIsClose -= OnCharacterAttack;

        TargetPoint.OnTargetRelocated -= SetNewRandomTargetPoint;
    }

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        StateController = GetComponent<GuardStateController>();
    }

    private void Start()
    {
        Init();
    }

    
    private void Init()
    {
        ActiveAIBehavior(true);
        OnCharacterPatrol();
    }

    private void ActiveAIBehavior(bool active)
    {
        Agent.enabled = active;
        CharacterDetector.Collider.enabled = active;
    }

    private void SetNewRandomTargetPoint()
    {
        if(StateController && StateController.CurrentState == EGuardStates.PATROL)
        {
            if (Agent) Agent.SetDestination(RandomPointTransform.transform.position);
        }
    }

    private void OnCharacterPatrol()
    {
        if (RandomPointTransform)
            Agent.destination =
                RandomPointTransform.transform.position;

        StateController.SetPatrol();
    }

    private void OnCharacterDetected()
    {
        StopAllCoroutines();

        if (Agent && CharacterDetector && CharacterDetector.Character)
        {
            Agent.destination = CharacterDetector.Character.transform.position;
            StateController.SetChase();

        }
    }

    private void OnCharacterAttack()
    {
        StopAllCoroutines();

        StateController.SetAttack();

        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        //Attack
        Debug.Log("GuardAI :: ATTACK !!! ");

        ActiveAIBehavior(false);

        yield return new WaitForSeconds(AttackDelaySeconds);

        Init();
    }



   

}
