using UnityEngine;
using UnityEngine.Events;

public abstract class DailyTask : MonoBehaviour
{
    [field: SerializeField] public int Index { get; private set; }

    [SerializeField] private TaskReward _reward;

    [field: SerializeField] public float TargerCount { get; set; }
    [field: SerializeField] public string Description { get; set; }

    public bool IsCompleted { get; private set; }
    public float CurrentCount { get; protected set; }
    public TaskReward Reward { get => _reward; private set => _reward = value; }

    private CharacterRewardGetter _rewardGetter;

    public event UnityAction Completed;
    public event UnityAction<float, float> ChangedCounter;
    public event UnityAction<int> ChangingCounter;


    public void Initialize(CharacterRewardGetter rewardGetter)
    {
        _rewardGetter = rewardGetter;
    }

    protected void GiveReward()
    {
        IsCompleted = true;
        _rewardGetter.TakeReward(_reward);
        Completed?.Invoke();
    }

    protected void AddCounter(float value)
    {
        CurrentCount += value;
        ChangedCounter?.Invoke(TargerCount, CurrentCount);
        ChangingCounter?.Invoke(Index);
    }

    public void ChangeEquelCounter(float value)
    {
        CurrentCount = value;
        ChangedCounter?.Invoke(TargerCount, CurrentCount);
        ChangingCounter?.Invoke(Index);
    }

    public void CheckComplited()
    {
        if (CurrentCount >= TargerCount)
        {
            Completed?.Invoke();
        }
    }
}
