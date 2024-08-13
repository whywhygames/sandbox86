using CoverShooter;
using TMPro;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.UI;

public class CraftMenu : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Sprite _openCraftIcon;
    [SerializeField] private Sprite _closeCraftIcon;
    [SerializeField] private Image _craftButtonImage;

    [Header("CrafrPanel Parametrs")]
    [SerializeField] private CanvasGroup _weaponPanel;
    [SerializeField] private CanvasGroup _craftPanel;
    [SerializeField] private CraftCategoryManager _craftCategoryManager;
    [SerializeField] private BuildingsGrid _buildingGrid;
    [SerializeField] private ThirdPersonController _controller;

    private bool _isOpen;

    private void Update()
    {
        if (TCKInput.GetAction(InputParametrs.CraftSystem.CraftMenuBUTTON, EActionEvent.Down))
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        if (_isOpen)
        {
            _isOpen = false;
            _weaponPanel.Activate();
            _craftPanel.Deactivate();
            _craftButtonImage.sprite = _openCraftIcon;
            _buildingGrid.StopCraft();
            _craftCategoryManager.Setup();
        }
        else
        {
            _controller.ZoomInput = false;
            _isOpen = true;
            _weaponPanel.Deactivate();
            _craftPanel.Activate();
            _craftButtonImage.sprite = _closeCraftIcon;
        }
    }
}
