using TouchControlsKit;
using UnityEngine;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour
{
    [SerializeField] private CraftType _craftType;
    [SerializeField] private BuildingsGrid _buildingGrid;
    [SerializeField] private Image _outline;

    private void Update()
    {
        if (TCKInput.GetAction($"Craft{_craftType}BUTTON", EActionEvent.Down))
        {
            _buildingGrid.StartPlacingBuilding(_craftType, this);
            _outline.color = new Color(0, 0.07843138f, 0);

        }
    }

    public void DisableOutline()
    {
        _outline.color = Color.white;
    }
}
