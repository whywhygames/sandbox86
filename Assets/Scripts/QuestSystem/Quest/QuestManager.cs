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

    public void Setup()
    {
        if (_currentQuest != null)
        {
            _currentQuest.Setup();
            _questPanelManager.Setup();
        }
    }

    private void OnStarted(Quest quest)
    {
        _questPanelManager.Initialize(quest);
        _currentQuest = quest;
    }
}