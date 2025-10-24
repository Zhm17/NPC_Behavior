using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    public virtual void Show()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }
}
