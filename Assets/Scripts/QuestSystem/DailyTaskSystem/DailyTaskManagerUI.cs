using System.Collections.Generic;
using UnityEngine;

public class DailyTaskManagerUI : MonoBehaviour
{
    [SerializeField] private DailyTaskManager _taskManager;
    [SerializeField] private DayliTaskCard _cardPrefab;
    [SerializeField] private Transform _container;

    private List<DayliTaskCard> _allCards = new List<DayliTaskCard>();

    private void Start()
    {
        foreach (DailyTask task in _taskManager.DailyTasks)
        {
            DayliTaskCard newCard = Instantiate(_cardPrefab, _container);
            _allCards.Add(newCard);
            newCard.Initialize(task);
        }
    }
}
