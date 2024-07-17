using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DailyTask : MonoBehaviour
{
    [SerializeField] private List<TaskReward> _rewards = new List<TaskReward>();

    [field: SerializeField] private string Description { get; set; }

    public bool IsCompleted { get; private set; }

    private CharacterRewardGetter _rewardGetter;

    public event UnityAction Completed;

    public void Initialize(CharacterRewardGetter rewardGetter)
    {
        _rewardGetter = rewardGetter;
    }

    protected void GiveReward()
    {
        IsCompleted = true;
        _rewardGetter.TakeReward(_rewards);
        Completed?.Invoke();
    }
}
