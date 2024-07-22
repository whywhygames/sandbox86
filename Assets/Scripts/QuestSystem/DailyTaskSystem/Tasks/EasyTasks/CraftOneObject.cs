using UnityEngine;

public class CraftOneObject : DailyTask
{
    [SerializeField] private CraftType _targetType;
    
    private BuildingsGrid _buildingsGrid;

    private void OnEnable()
    {
        _buildingsGrid = FindObjectOfType<BuildingsGrid>();
        _buildingsGrid.Craft += OnCraft;
    }

    private void nDisable()
    {
        _buildingsGrid.Craft -= OnCraft;
    }

    private void OnCraft(Building craftObject)
    {
        if (IsCompleted)
            return;

        if (craftObject.Type == _targetType)
        {
            AddCounter(1);

            if (CurrentCount == TargerCount)
                GiveReward();
        }
    }
}
