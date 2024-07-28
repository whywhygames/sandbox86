using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private QuestPanelManager _questPanelManager;
    [SerializeField] private List<Quest> _quests = new List<Quest>();
    [SerializeField] private CharacterRewardGetter _rewardGetter;

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

    private void OnStarted(Quest quest)
    {
        _questPanelManager.Initialize(quest);
    }
}