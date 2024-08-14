using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour 
{
    [SerializeField] private List<ShopCategoryPanel> _allShopPanels = new List<ShopCategoryPanel>();
    [SerializeField] private List<ShopItem> _allShopItems = new List<ShopItem>();
    [SerializeField] private CanvasGroup _thisPanel;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openButton;
    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private ShopViewWindow _viewWindow;
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private CharacterRewardGetter _characterRewardGetter;

    private ShopCategoryPanel _openPanel;
    private ShopItemConfigure _selectItemConfigure;
    private ShopItem _selectItemButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(ClosePanel);
        _openButton.onClick.AddListener(OpenPanel);
        _buyButton.AddListener(Buy);
    }

    private void Start()
    {
        SetPanel(ShopCategoty.Charges);
        SetItem(_allShopItems[0]);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(ClosePanel);
        _openButton.onClick.RemoveListener(OpenPanel);
        _buyButton.RemoveListener(Buy);
    }

    public void SetPanel(ShopCategoty category)
    {
        if (_openPanel != null)
        {
            _openPanel.Close();
            _openPanel = null;
        }

        switch (category)
        {
            case ShopCategoty.Charges:
                _openPanel = _allShopPanels[0];
                _openPanel.Open();
                break;

            case ShopCategoty.InApp:
                _openPanel = _allShopPanels[1];
                _openPanel.Open();
                break;
        }
    }

    private void Buy()
    {
        if (_selectItemConfigure != null)
        {
            if (_playerMoney.TryBuy(_selectItemConfigure.Price))
            {
                _playerMoney.Buy(_selectItemConfigure.Price);
                _characterRewardGetter.GiveReward(_selectItemConfigure.Rewardtype, _selectItemConfigure.Count);
                _buyButton.UpdateLockBacground(_playerMoney.TryBuy(_selectItemConfigure.Price) ? true : false);
            }
        }
    }

    public void SetItem(ShopItem itemButton)
    {
        if (_selectItemButton != null)
        {
            _selectItemButton.DisableOutline();
            _selectItemButton = null;
        }

        _selectItemConfigure = itemButton.Configure;
        _selectItemButton = itemButton;
        _viewWindow.Initialize(_selectItemConfigure);

        _buyButton.UpdateLockBacground(_playerMoney.TryBuy(_selectItemConfigure.Price) ? true : false);
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
