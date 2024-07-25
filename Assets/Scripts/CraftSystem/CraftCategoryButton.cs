using TouchControlsKit;
using UnityEngine;

public class CraftCategoryButton : MonoBehaviour
{
    [SerializeField] private CraftCategory _craftCategory;
    [SerializeField] private CraftCategoryManager _buildingGrid;

    private void Update()
    {
        if (TCKInput.GetAction(_craftCategory.ToString() + "BUTTON", EActionEvent.Down))
        {
            _buildingGrid.SetPanel(_craftCategory);
        }
    }
}
