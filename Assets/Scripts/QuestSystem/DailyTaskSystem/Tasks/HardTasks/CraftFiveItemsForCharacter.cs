using UnityEngine;

public class CraftFiveItemsForCharacter : DailyTask
{
    [SerializeField] private CharacterType _targetCaharacterType;
    [SerializeField] private PlayerBootstrap _player;

    private BuildingsGrid _buildingsGrid;

    private void OnEnable()
    {
        _buildingsGrid = FindObjectOfType<BuildingsGrid>();
        _player = FindObjectOfType<PlayerBootstrap>();
        _buildingsGrid.Craft += OnCraft;
    }

    private void OnDisable()
    {
        _buildingsGrid.Craft -= OnCraft;
    }

    private void OnCraft(Building craftObject)
    {
        if (IsCompleted)
            return;
       
        if (_player.Type == _targetCaharacterType)
        {
            AddCounter(1);

            if (CurrentCount == TargerCount)
                GiveReward();
        }
    }
}
