using Generics;
using System.Collections;
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

        StartCoroutine(UpdateMethod());
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


    IEnumerator UpdateMethod()
    {
        while (true)
        {
            if ((int)StateController.CurrentState > 0)
            {
                if (CharacterDetector.Character &&
                    PlayerIsClose(CharacterDetector.Character.transform))
                {
                    OnCharacterAttack();
                }
            }
            else
            {
                if (TargetPointTransform &&
                    TargetPointTransform.gameObject.activeInHierarchy)
                    m_agent.destination =
                        TargetPointTransform.transform.position;
            }

            yield return null;
        }
    }

    IEnumerator DelayAfterAttack()
    {
        yield return new WaitForSeconds(DelaySeconds);

        Init();
    }


    private void Init()
    {
        StopAllCoroutines();

        StartCoroutine(UpdateMethod());
    }

    private void OnCharacterEnter(Transform characterT)
    {
        m_agent.destination = characterT.position;
        StateController.SetChase();
    }

    private void OnCharacterAttack()
    {
        StateController.SetAttack();

        //Attack

        StartCoroutine(DelayAfterAttack());
    }

    private void OnCharacterExit(Transform characterT)
    { 
        m_agent.destination = TargetPointTransform.transform.position;
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
