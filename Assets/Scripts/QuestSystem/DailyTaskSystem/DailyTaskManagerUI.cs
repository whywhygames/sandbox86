using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskManagerUI : MonoBehaviour
{
    [Header ("FullPanel")]
    [SerializeField] private DailyTaskManager _taskManager;
    [SerializeField] private DayliTaskCard _cardPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openButton;
    [SerializeField] private CanvasGroup _thisPanel;

    private List<DayliTaskCard> _allCards = new List<DayliTaskCard>();

    [Header ("MiniPanel")]
    [SerializeField] private MiniDayliTaskCard _miniCardPrefab;
    [SerializeField] private Transform _containerForMiniTaskCard;

    private List<MiniDayliTaskCard> _allMiniCards = new List<MiniDayliTaskCard>();


    private void OnEnable()
    {
        _taskManager.FillTaskList += OnFillTaskList;
        _taskManager.NewDayStarted += NewDayStarted;
        _closeButton.onClick.AddListener(ClosePanel);
        _openButton.onClick.AddListener(OpenPanel);
    }

    private void OnDisable()
    {
        _taskManager.FillTaskList -= OnFillTaskList;
        _taskManager.NewDayStarted -= NewDayStarted;
        _closeButton.onClick.RemoveListener(ClosePanel);
        _openButton.onClick.RemoveListener(OpenPanel);
    }

    private void ClosePanel()
    {
        _thisPanel.Deactivate();
    }

    private void OpenPanel()
    {
        _thisPanel.Activate();
    }

    private void NewDayStarted()
    {
        if (_allCards.Count > 0)
            for (int i = 0; i < _allCards.Count; i++)
            {
                Destroy(_allCards[i].gameObject);
                Destroy(_allMiniCards[i].gameObject);
            }

        _allCards.Clear();
        _allMiniCards.Clear();
    }

    private void OnFillTaskList()
    {
        foreach (DailyTask task in _taskManager.DailyTasks)
        {
            DayliTaskCard newCard = Instantiate(_cardPrefab, _container);
            _allCards.Add(newCard);
            newCard.Initialize(task);

            MiniDayliTaskCard newMiniCard = Instantiate(_miniCardPrefab, _containerForMiniTaskCard);
            _allMiniCards.Add(newMiniCard);
            newMiniCard.Initialize(task);
        }
    }
}
