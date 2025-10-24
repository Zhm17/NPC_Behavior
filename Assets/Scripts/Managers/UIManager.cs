using Generics;
using UnityEngine;
using Utils;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] private UIP_GameOver GameOverPanel;
    [SerializeField] private UIP_HUDs HUDsPanel;

    protected override void Init()
    {
        GameOverPanel.Hide();
    }

    private void OnEnable()
    {


        HealthComponent.OnDeath += ShowGameOver;
    }

    private void OnDisable()
    {
        HealthComponent.OnDeath -= ShowGameOver;
    }

    private void OnDestroy()
    {
        HealthComponent.OnDeath -= ShowGameOver;
    }

    private void ShowGameOver(GameObject gameobject)
    {
        GameOverPanel.Show();
    }
}
