using CoverShooter;
using UnityEngine;

public class KillOneEnemyForWeapon : DailyTask
{
    [SerializeField] private HitType TargetType;

    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();

        foreach (EnemyHealth enemy in enemyManager.AllEnemies)
        {
            enemy.Died += OnDied;
        }
    }

    private void OnDied(HitType hitType, EnemyType enemyType)
    {
        if (IsCompleted)
            return;

        if (hitType == TargetType)
        {
            AddCounter(1);

            if (CurrentCount == TargerCount)
            {
                GiveReward();
            }
        }
    }
}
