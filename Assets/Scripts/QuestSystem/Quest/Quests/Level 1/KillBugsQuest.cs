using System.Collections.Generic;
using UnityEngine;

public class KillBugsQuest : MonoBehaviour
{
    [SerializeField] private List<EnemyHealth> _targetEnemyes = new List<EnemyHealth>();
    [SerializeField] private FreeMedecine _freeMedecineReward;
    [SerializeField] private Quest _questObject;

    private void OnEnable()
    {
        _questObject.Started += StartQuest;
        _questObject.Stopped += Setup;
    }

    private void Start()
    {
        foreach (EnemyHealth enemy in _targetEnemyes)
        {
            enemy.Daying += OnDied;
            enemy.gameObject.SetActive(false);
        }

        _freeMedecineReward.gameObject.SetActive(false);
    }

    private void StartQuest(Quest arg0)
    {
        foreach (EnemyHealth enemy in _targetEnemyes)
        {
            enemy.gameObject.SetActive(true);
            enemy.GetComponent<EnemyMovement>().Setup();
        }
    }

    private void Setup()
    {
        foreach (EnemyHealth enemy in _targetEnemyes)
        {
            enemy.Respawn();
            enemy.gameObject.SetActive(false);
        }
    }

    private void OnDied(EnemyHealth enemy)
    {
        _questObject.AddCounter(1);

        if (_questObject.CurrentCount == _questObject.TargerCount)
        {
            _questObject.GiveReward();
            _freeMedecineReward.transform.position = new Vector3(enemy.transform.position.x, _freeMedecineReward.transform.position.y, enemy.transform.position.z);
            _freeMedecineReward.gameObject.SetActive(true);
        }
    }
}
