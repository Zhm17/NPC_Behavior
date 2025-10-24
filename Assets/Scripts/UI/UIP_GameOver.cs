using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIP_GameOver : UIPanel
{
    private void OnEnable()
    {
        StartCoroutine(RestartSceneCoroutine());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator RestartSceneCoroutine()
    {
        yield return new WaitForSeconds(5f);

        ReloadStage();
    }

    public virtual void ReloadStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
