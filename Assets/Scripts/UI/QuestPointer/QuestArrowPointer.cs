using UnityEngine;

public class QuestArrowPointer : MonoBehaviour 
{
    private void OnEnable() {
        PointerManager.Instance.AddToList(this);
    }

    private void OnDisable()
    {
        PointerManager.Instance.RemoveFromList(this);
    }
}
