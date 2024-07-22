using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskManagerUI : MonoBehaviour
{
    [SerializeField] private DailyTaskManager _taskManager;
    [SerializeField] private DayliTaskCard _cardPrefab;
    [SerializeField] private Transform _container;

    private List<DayliTaskCard> _allCards = new List<DayliTaskCard>();

    private void OnEnable()
    {
        _taskManager.FillTaskList += OnFillTaskList;
        _taskManager.NewDayStarted += NewDayStarted;
    }

    private void OnDisable()
    {
        _taskManager.FillTaskList -= OnFillTaskList;
        _taskManager.NewDayStarted -= NewDayStarted;
    }

    private void NewDayStarted()
    {
        if (_allCards.Count > 0)
            foreach (DayliTaskCard card in _allCards)
            {
                Destroy(card.gameObject);
            }

        _allCards.Clear();
    }

    private void OnFillTaskList()
    {
        foreach (DailyTask task in _taskManager.DailyTasks)
        {
            DayliTaskCard newCard = Instantiate(_cardPrefab, _container);
            _allCards.Add(newCard);
            newCard.Initialize(task);
        }
    }
}
