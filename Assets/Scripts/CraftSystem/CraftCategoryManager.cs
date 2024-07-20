using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;

public class CraftCategoryManager : MonoBehaviour
{
    [SerializeField] private List<CraftCategotyPanel> _allPanels = new List<CraftCategotyPanel>();
    [SerializeField] private CanvasGroup _categoryPanel;
    [SerializeField] private CanvasGroup _backToCategoryButton;

    private CraftCategotyPanel _openPanel;

    private void Update()
    {
        if (TCKInput.GetAction(InputParametrs.CraftSystem.Categoty.BackToCategory, EActionEvent.Down))
        {
            Setup();
        }
    }

    public void SetPanel(CraftCategory category)
    {
        if (_openPanel != null)
        {
            _openPanel.Close();
            _openPanel = null;
        }

        foreach (var panel in _allPanels )
        {
            if (panel.CraftCategory == category)
            {
                _openPanel = panel;
                _openPanel.Open();
                _categoryPanel.Deactivate();
                _backToCategoryButton.Activate();
            }
        }
    }

    public void Setup()
    {
        if (_openPanel != null)
        {
            _openPanel.Close();
            _openPanel = null;
        }

        _categoryPanel.Activate();
        _backToCategoryButton.Deactivate();
    }
}
