using UnityEngine;

public class CraftCategotyPanel : MonoBehaviour
{
    [field: SerializeField] public CraftCategory CraftCategory;

    [SerializeField] private CanvasGroup _canvasGroup;

    public void Open()
    {
        _canvasGroup.Activate();
    }

    public void Close()
    {
        _canvasGroup.Deactivate();
    }
}
