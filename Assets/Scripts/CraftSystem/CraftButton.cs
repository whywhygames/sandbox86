using TouchControlsKit;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    [SerializeField] private CraftType _craftType;
    [SerializeField] private BuildingsGrid _buildingGrid;

    private void Update()
    {
        if (TCKInput.GetAction($"Craft{_craftType}", EActionEvent.Down))
        {
            _buildingGrid.StartPlacingBuilding(_craftType);
        }
    }
}
