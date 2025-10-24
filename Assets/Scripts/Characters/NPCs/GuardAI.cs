using Generics;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GuardStateController))]
[RequireComponent(typeof(NavMeshAgent))]
public class GuardAI : MonoBehaviour
{
    protected private NavMeshAgent m_agent;
    public NavMeshAgent Agent => m_agent;

    [SerializeField] public TargetPoint TargetPointTransform;

    protected GuardStateController m_stateController;
    public GuardStateController StateController
    {
        get { return m_stateController; }
    }

    [Header("Min Distance to Player")]
    [SerializeField] private float m_minDistanceToPlayer = 100f;
    public float MinDistanceToPlayer => m_minDistanceToPlayer;


    [Header("Character Detector")]
    [SerializeField] private CharacterDetector m_characterDetector;
    public CharacterDetector CharacterDetector => m_characterDetector;

    [Header("Delay After Attack")]
    [SerializeField] private float m_delaySeconds = 3f;
    public float DelaySeconds => m_delaySeconds;

    private void OnEnable()
    {
        CharacterDetector.OnCharacterDetected += OnCharacterEnter;
        CharacterDetector.OnCharacterExit += OnCharacterExit;

        StartCoroutine(GetCloserCoroutine());
    }

    private void OnDisable()
    {
        CharacterDetector.OnCharacterDetected -= OnCharacterEnter;
        CharacterDetector.OnCharacterExit -= OnCharacterExit;

        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        CharacterDetector.OnCharacterDetected -= OnCharacterEnter;
        CharacterDetector.OnCharacterExit -= OnCharacterExit;

        StopAllCoroutines();
    }

    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_stateController = GetComponent<GuardStateController>();
    }

    private void Start()
    {
        Init();
    }


    IEnumerator GetCloserCoroutine()
    {
        while (true)
        {
            if ((int)StateController.CurrentState == 1)
            {
                if (CharacterDetector.Character &&
                    PlayerIsClose(CharacterDetector.Character.transform))
                {
                    OnCharacterAttack();
                }
            }

            yield return null;
        }
    }

    IEnumerator AttackCoroutine()
    {
        //Attack
        Debug.Log("GuardAI :: ATTACK !!! ");

        m_agent.destination = transform.position;

        yield return new WaitForSeconds(DelaySeconds);

        Init();
    }


    private void Init()
    {
        StopAllCoroutines();

        OnCharacterExit();

        //StartCoroutine(GetCloserCoroutine());
    }

    private void OnCharacterEnter()
    {
        m_agent.destination = CharacterDetector.Character.transform.position;
        StateController.SetChase();

        StartCoroutine(GetCloserCoroutine());
    }

    private void OnCharacterAttack()
    {
        StopAllCoroutines();

        StateController.SetAttack();

        StartCoroutine(AttackCoroutine());
    }

    private void OnCharacterExit()
    {
        if (TargetPointTransform &&
            TargetPointTransform.gameObject.activeInHierarchy)
                        m_agent.destination =
                            TargetPointTransform.transform.position;

        StateController.SetPatrol();
    }

    private bool PlayerIsClose( Transform target)
    {
        return (MinDistanceToPlayer <= EvaluateDistance(target));
    }

    private float EvaluateDistance(Transform target)
    {
        return Vector3.Distance(transform.localPosition, target.localPosition);
    }

}
