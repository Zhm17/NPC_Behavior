using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GuardStateController : MonoBehaviour
{
    private EGuardStates m_currentState = EGuardStates.PATROL;
    public EGuardStates CurrentState
    {
        get { return m_currentState; }
    }

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

    private void OnEnable()
    {
        SetPatrol();
    }


    private void SetPatrol()
    {
        m_currentState = EGuardStates.PATROL;
        GState = new GS_PatrolState();
    }

    private void SetChase()
    {
        m_currentState = EGuardStates.CHASING;
        GState = new GS_ChaseState();
    }

    private void SetAttack()
    {
        m_currentState = EGuardStates.ATTACKING;
        GState = new GS_AttackState();
    }
}
