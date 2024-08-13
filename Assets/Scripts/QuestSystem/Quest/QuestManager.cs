using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private QuestPanelManager _questPanelManager;
    [SerializeField] private List<Quest> _quests = new List<Quest>();
    [SerializeField] private CharacterRewardGetter _rewardGetter;

    private Quest _currentQuest;

    private void OnEnable()
    {
        foreach (Quest quest in _quests)
        {
            quest.Started += OnStarted;
            quest.Completed += OnComplited;
            quest.Initialize(_rewardGetter);
        }
    }

    private void OnDisable()
    {
        foreach (Quest quest in _quests)
        {
            quest.Started -= OnStarted;
        }
    }

    private void OnComplited()
    {
        foreach (Quest quest in _quests)
        {
            if (quest.Activate == true)
                quest.gameObject.SetActive(true);
        }
    }

    public void Setup()
    {
        if (_currentQuest != null)
        {
            _currentQuest.Setup();
            _questPanelManager.Setup();
        }
    }

    private void OnStarted(Quest startedQuest)
    {
        _questPanelManager.Initialize(startedQuest);
        _currentQuest = startedQuest;

        foreach (Quest quest in _quests)
        {
            if (quest != startedQuest)
                quest.gameObject.SetActive(false);
        }
    }
}