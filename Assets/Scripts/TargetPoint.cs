using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    [Header("X Position")]
    [SerializeField] public float m_minXpos = -90f;
    [SerializeField] public float m_maxXpos = 90f;

    [Header("Z Position")]
    [SerializeField] public float m_minZpos = -90f;
    [SerializeField] public float m_maxZpos = 90f;

    [Header("Cooldown Time")]
    [SerializeField] private float m_cooldownTimeSpawn;
    [SerializeField] public float m_timeSpawn = 10f;

    private void OnEnable()
    {
        m_cooldownTimeSpawn = m_timeSpawn;
    }

    private void Update()
    {
        m_cooldownTimeSpawn -= Time.deltaTime;
        if(m_cooldownTimeSpawn <= 0)
        {
            TransformPositionRelocation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GuardAI player))
        {
            Debug.Log("On GuardAI Triggered !! ...");

            TransformPositionRelocation();
        }
    }

    private void TransformPositionRelocation()
    {
        float newXPos = Random.Range(m_minXpos, m_maxXpos);
        float newZPos = Random.Range(m_minZpos, m_maxZpos);

        transform.localPosition = new Vector3(newXPos, 
                                                transform.localPosition.y, 
                                                newZPos);

        m_cooldownTimeSpawn = m_timeSpawn;
    }
}
