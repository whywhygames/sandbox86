using CoverShooter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _weaponPanel;
    [SerializeField] private CanvasGroup _craftPanel;
    [SerializeField] private CraftCategoryManager _craftCategoryManager;
    [SerializeField] private BuildingsGrid _buildingGrid;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ThirdPersonController _controller;

    private bool _isOpen;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (_isOpen)
        {
            _isOpen = false;
            _weaponPanel.Activate();
            _craftPanel.Deactivate();
            _text.text = "OPEN CRAFT";
            _buildingGrid.StopCraft();
            _craftCategoryManager.Setup();
        }
        else
        {
            _controller.ZoomInput = false;
            _isOpen = true;
            _weaponPanel.Deactivate();
            _craftPanel.Activate();
            _text.text = "CLOSE CRAFT";
        }
    }
}
