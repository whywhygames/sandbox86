using System.Collections.Generic;

public class CraftFiveItems : DailyTask
{
    private List<Building> _buildings = new List<Building>();
    private BuildingsGrid _grid = new BuildingsGrid();

    private void OnEnable()
    {
        _grid = FindObjectOfType<BuildingsGrid>();
        _grid.Craft += OnCraft;
    }

    private void OnDisable()
    {
        _grid.Craft -= OnCraft;
    }

    private void OnCraft(Building item)
    {
        if (IsCompleted)
            return;

        bool isUnique = true;

        foreach (Building building in _buildings)
        {
            if (item.Type == building.Type)
                isUnique = false;
        }

        if (isUnique) 
        {
            AddCounter(1);
            _buildings.Add(item);
        }

        if (CurrentCount == TargerCount)
            GiveReward();
    }
}
