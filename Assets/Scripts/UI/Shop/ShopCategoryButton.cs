using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryButton : MonoBehaviour
{
    [SerializeField] private ShopCategoty _categoty;
    [SerializeField] private Shop _shop;

    private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowPanel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowPanel);
    }

    private void ShowPanel()
    {
        _shop.SetPanel(_categoty);
    }
}
public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopCategoryPanel> _allShopPanels = new List<ShopCategoryPanel>();
    [SerializeField] private CanvasGroup _thisPanel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openButton;
    [SerializeField] private ShopViewWindow _viewWindow;

    private ShopCategoryPanel _openPanel;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(ClosePanel);
        _openButton.onClick.AddListener(OpenPanel);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(ClosePanel);
        _openButton.onClick.RemoveListener(OpenPanel);
    }

    public void SetPanel(ShopCategoty category)
    {
        if (_openPanel != null)
        {
            _openPanel.Close();
            _openPanel = null;
        }

        foreach (var panel in _allShopPanels)
        {
            if (panel.Categoty == category)
            {
                _openPanel = panel;
                _openPanel.Open();
            }
        }
    }

    public void ShopViewWindow()
    {

    }

    private void ClosePanel()
    {
        _thisPanel.Deactivate();
    }

    private void OpenPanel()
    {
        _thisPanel.Activate();
    }
}

public class ShopItem : MonoBehaviour
{

}

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

public enum ShopCategoty
{
    Charges,
    InApp
}

public class ShopViewWindow : MonoBehaviour
{

}