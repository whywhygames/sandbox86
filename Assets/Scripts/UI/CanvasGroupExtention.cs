using UnityEngine;

public static class CanvasGroupExtention
{
    public static void Activate(this CanvasGroup button)
    {
        button.alpha = 1;
        button.interactable = true;
        button.blocksRaycasts = true;
    }

    public static void Deactivate(this CanvasGroup button)
    {
        button.alpha = 0;
        button.interactable = false;
        button.blocksRaycasts = false;
    }
}
