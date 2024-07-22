public class UseGrenade : DailyTask
{
    private GrenadeInventoryCounter _grenadeSpawner;

    private void OnEnable()
    {
        _grenadeSpawner = FindObjectOfType<GrenadeInventoryCounter>();
        _grenadeSpawner.GrenadSpawned += OnMineSpawned;
    }

    private void OnDisable()
    {
        _grenadeSpawner.GrenadSpawned -= OnMineSpawned;
    }

    private void OnMineSpawned()
    {
        if (IsCompleted)
            return;

        AddCounter(1);

        if (CurrentCount == TargerCount)
            GiveReward();
    }
}
