using UnityEngine;

[CreateAssetMenu(fileName = "TaskReward", menuName = "Create Task Reward", order = 51)]

public class TaskReward : ScriptableObject
{
    [field: SerializeField] public RewardType RewardType { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}