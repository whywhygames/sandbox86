using System.Collections.Generic;
using UnityEngine;

public class KillBugsQuest : Quest
{
    [SerializeField] private List<EnemyHealth> _targetEnemyes;

    private void Start()
    {
        foreach (EnemyHealth enemy in _targetEnemyes)
        {
            enemy.Daying += OnDied;
            enemy.gameObject.SetActive(false);
        }
    }

    protected override void StartQuest()
    {
        base.StartQuest();

        foreach (EnemyHealth enemy in _targetEnemyes)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    private void OnDied(EnemyHealth enemy)
    {
        AddCounter(1);

        if (CurrentCount == TargerCount)
        {
            GiveReward();
        }
    }
}
