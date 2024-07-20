using UnityEngine;

public class KillBugFromRevolverTask : DailyTask
{
    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();

        foreach (EnemyHealth enemy in enemyManager.AllEnemies)
        {
            enemy.Died += OnDied;
        }
    }

    private void OnDied(CoverShooter.HitType hitType, EnemyType enemyType)
    {
        if (IsCompleted)
            return;

        if (hitType == CoverShooter.HitType.Pistol && enemyType == EnemyType.Bug)
        {
            AddCounter(1);

            if (CurrentCount == TargerCount)
            {
                GiveReward();
            }
        }
    }
}
