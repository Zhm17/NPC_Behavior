using Generics;
using UnityEngine;
using UnityEngine.UI;

public class UIP_HUDs : UIPanel
{
    [SerializeField] private Slider m_healthSlider;

    private void OnEnable()
    {
        m_healthSlider.value = m_healthSlider.maxValue;
        HealthComponent.OnHealthUpdate += OnHealthUpdate;
    }

    private void OnDisable()
    {
        HealthComponent.OnHealthUpdate -= OnHealthUpdate;
    }

    private void OnDestroy()
    {
        HealthComponent.OnHealthUpdate -= OnHealthUpdate;
    }

    private void OnHealthUpdate(GameObject obj, int value)
    {
        m_healthSlider.value = value;
    }
}
