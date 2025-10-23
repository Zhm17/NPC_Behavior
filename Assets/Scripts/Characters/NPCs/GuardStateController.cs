using UnityEngine;

public class GuardStateController : MonoBehaviour
{
    private EGuardStates m_currentState = EGuardStates.PATROL;
    [SerializeField] public EGuardStates CurrentState => m_currentState;

    private IGuardState m_gState;
    public IGuardState GState
    {
        get { return m_gState;}
        set 
        { 
            m_gState = value;
            m_gState.Exec();
        }
    }

    [Header("Materials")]
    [SerializeField] 
    private Material[] m_stateMaterials;

    private void OnEnable()
    {
        SetPatrol();
    }


    private void SetPatrol()
    {
        //Assign current state
        m_currentState = EGuardStates.PATROL;

        ChangeColor();

        //Create and execute
        GState = new GS_PatrolState();
    }

    private void SetChase()
    {
        //Assign current state
        m_currentState = EGuardStates.CHASING;

        ChangeColor();

        //Create and execute
        GState = new GS_ChaseState();
    }

    private void SetAttack()
    {
        //Assign current state
        m_currentState = EGuardStates.ATTACKING;

        ChangeColor();

        //Create and execute
        GState = new GS_AttackState();
    }

    /// <summary>
    /// Change the material depending the Current State
    /// </summary>
    private void ChangeColor()
    {
        if (GetComponent<Renderer>() &&
            m_stateMaterials.Length > (int) CurrentState &&
            m_stateMaterials[(int) CurrentState] != null)
        {

            GetComponent<Renderer>().material =
                                m_stateMaterials[(int) CurrentState];
        }
    }
}
