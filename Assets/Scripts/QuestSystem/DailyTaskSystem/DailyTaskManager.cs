using System.Collections.Generic;
using UnityEngine;

public class DailyTaskManager : MonoBehaviour
{
    [SerializeField] private List<DailyTask> _easyTasks = new List<DailyTask>();
    [SerializeField] private List<DailyTask> _hardTasks = new List<DailyTask>();
    [SerializeField] private int _generalMoneyReward;
    [SerializeField] private Transform _taskContainer;
    [SerializeField] private CharacterRewardGetter _characterRewardGetter;

    private List<DailyTask> _tempEasyTasks = new List<DailyTask>();
    private List<DailyTask> _tempHardTasks = new List<DailyTask>();

    private List<DailyTask> _dailyTasks = new List<DailyTask>();
    
    private const int MaxEasyDailyTasks = 3;
    private const int MaxHardDailyTasks = 2;

    public List<DailyTask> DailyTasks { get => _dailyTasks; set => _dailyTasks = value; }

    private void Awake()
    {
        InitTempLists();

        FillDailyTasks(ref _tempEasyTasks, MaxEasyDailyTasks);
        FillDailyTasks(ref _tempHardTasks, MaxHardDailyTasks);

        InitDailyTasks();
    }

    private void InitDailyTasks()
    {
        foreach (DailyTask task in _dailyTasks)
        {
            task.Initialize(_characterRewardGetter);
        }
    }

    private void FillDailyTasks(ref List<DailyTask> tempTasks, int count)
    {
        for (int i = 0; i < count; i++)
        {
            DailyTask randomTask = tempTasks[Random.Range(0, tempTasks.Count)];

            _dailyTasks.Add(Instantiate(randomTask, _taskContainer));
            _dailyTasks[_dailyTasks.Count - 1].Completed += CheckComlitedTasks;
            
            tempTasks.Remove(randomTask);
        }
    }

    private void InitTempLists()
    {
        foreach (var task in _easyTasks)
            _tempEasyTasks.Add(task);

        foreach (var task in _hardTasks)
            _tempHardTasks.Add(task);
    }

    private void CheckComlitedTasks()
    {
        bool fullComplited = false;

        foreach (DailyTask task in _dailyTasks)
        {
            if (task.IsCompleted)
            {
                fullComplited = true;
            }
            else
            {
                fullComplited = false;
                break;
            }
        }

        if (fullComplited)
            _characterRewardGetter.GetMoney(_generalMoneyReward);
    }
}