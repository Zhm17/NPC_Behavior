using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    public delegate void TargetPointDelegate();
    public static event TargetPointDelegate OnTargetRelocated;

    [Header("X Position")]
    [SerializeField] private float m_minXpos = -90f; 
    public float minXPos => m_minXpos;
    [SerializeField] private float m_maxXpos = 90f;
    public float maxXPos => m_maxXpos;

    [Header("Z Position")]
    [SerializeField] private float m_minZpos = -90f;
    public float minZpos => m_minZpos;
    [SerializeField] private float m_maxZpos = 90f;
    public float maxZpos => m_maxZpos;


    [Header("Cooldown Time")]
    [SerializeField] private float m_cooldownTimeSpawn;
    private float CooldownTimeSpawn
    {
        get { return m_cooldownTimeSpawn; }
        set { m_cooldownTimeSpawn = value; }
    }

    [SerializeField] private float m_timeSpawn = 10f;
    public float TimeSpawn => m_timeSpawn;


    private void OnEnable()
    {
        CooldownTimeSpawn = TimeSpawn;
    }

    private void Update()
    {
        CooldownTimeSpawn -= Time.deltaTime;
        if(CooldownTimeSpawn <= 0)
        {
            TransformPositionRelocation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GuardAI guardAI))
        {
            Debug.Log("TargetPoint :: On GuardAI reached !! ...");

            TransformPositionRelocation();
        }
    }

    private void TransformPositionRelocation()
    {
        float newXPos = Random.Range(minXPos, maxXPos);
        float newZPos = Random.Range(minZpos, maxZpos);

        transform.localPosition = new Vector3(newXPos, 
                                                transform.localPosition.y, 
                                                newZPos);

        if (OnTargetRelocated != null)
            OnTargetRelocated();

        Debug.Log("TargetPoint :: Relocation...");

        CooldownTimeSpawn = TimeSpawn;
    }
}
