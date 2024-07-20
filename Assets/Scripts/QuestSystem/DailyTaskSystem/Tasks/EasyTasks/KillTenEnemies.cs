public class KillTenEnemies : DailyTask
{
    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();

        foreach (EnemyHealth enemy in enemyManager.AllEnemies)
        {
            enemy.Daying += OnDaying;
        }
    }

    private void OnDaying(EnemyHealth arg0)
    {
        if (IsCompleted)
            return;

        AddCounter(1);

        if (CurrentCount >= TargerCount)
        {
            GiveReward();
        }
    }
}
