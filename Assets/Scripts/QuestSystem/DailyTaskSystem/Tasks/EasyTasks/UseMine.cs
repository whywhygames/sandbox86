public class UseMine : DailyTask
{
    private MineSpawner _mineSpawner;

    private void OnEnable()
    {
        _mineSpawner = FindObjectOfType<MineSpawner>();
        _mineSpawner.MineSpawned += OnMineSpawned;
    }

    private void OnDisable()
    {
        _mineSpawner.MineSpawned -= OnMineSpawned;
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
