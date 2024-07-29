using System.Collections.Generic;
using UnityEngine;

public class FreeTheDogQuest : MonoBehaviour
{
    [SerializeField] private List<EnemyHealth> _targetEnemyes = new List<EnemyHealth>();
    [SerializeField] private GunDogMovement _lockedDog;
    [SerializeField] private List<GameObject> _barriers = new List<GameObject>();
    [SerializeField] private Quest _questObject;

    private void OnEnable()
    {
        _questObject.Started += StartQuest;
        _questObject.Stopped += Setup;

        foreach (EnemyHealth enemy in _targetEnemyes)
        {
          //  enemy.Daying += OnDied;
            enemy.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        foreach (EnemyHealth enemy in _targetEnemyes)
        {
            enemy.Daying += OnDied;
            //enemy.gameObject.SetActive(false);
        }
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

            foreach (GameObject barrier in _barriers)
            {
                barrier.gameObject.SetActive(false);
            }

            _lockedDog.Unlcoked();
        }
    }
}
