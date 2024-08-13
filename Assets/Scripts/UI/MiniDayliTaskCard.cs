using TMPro;
using UnityEngine;

public class MiniDayliTaskCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _counter;

    private DailyTask _task;

    public void Initialize(DailyTask task)
    {
        _task = task;
        _task.ChangedCounter += OnChangedCount;
        _task.Completed += OnComlitedCount;

        _counter.text = $"{_task.CurrentCount}/{_task.TargerCount}";
    }

    private void OnComlitedCount()
    {
        _counter.text = "DONE";
        _counter.color = Color.black;
    }

    private void OnChangedCount(float targerCount, float currentCount)
    {
        _counter.text = $"{Mathf.Round(currentCount)}/{Mathf.Round(targerCount)}";
    }
}
