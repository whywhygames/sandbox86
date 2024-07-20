public class KillFiveFreezEnemies : DailyTask
{
    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();

        foreach (EnemyHealth enemy in enemyManager.AllEnemies)
        {
            enemy.DayingFreez += OnFreezed;
        }
    }

    private void OnFreezed()
    {
        if (IsCompleted)
            return;

        AddCounter(1);

        if (CurrentCount == TargerCount)
            GiveReward();
    }
}
