using UnityEngine;

public class ShopCategoryPanel : MonoBehaviour 
{
    [field: SerializeField] public ShopCategoty Categoty { get; private set; }

    [SerializeField] private CanvasGroup _thisPanel;
    
    public void Open()
    {
        _thisPanel.Activate();
    }

    public void Close()
    {
        _thisPanel.Deactivate();
    }
}
