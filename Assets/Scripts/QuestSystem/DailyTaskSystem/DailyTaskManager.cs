using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ThirdPersonCamera.DemoSceneScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DailyTaskManager : MonoBehaviour
{
    [SerializeField] private List<DailyTask> _easyTasks = new List<DailyTask>();
    [SerializeField] private List<DailyTask> _hardTasks = new List<DailyTask>();
    [SerializeField] private int _generalMoneyReward;
    [SerializeField] private Transform _taskContainer;
    [SerializeField] private CharacterRewardGetter _characterRewardGetter;

    public TMP_Text _textDELETE;

    private List<DailyTask> _tempEasyTasks = new List<DailyTask>();
    private List<DailyTask> _tempHardTasks = new List<DailyTask>();

    private List<int> _easyTasksIndexes = new List<int>();
    private List<int> _hardTasksIndexes = new List<int>();

    private List<float> _tasksProgress = new List<float>() { 0, 0, 0, 0, 0 };

    private List<DailyTask> _dailyTasks = new List<DailyTask>();

    private const int MaxEasyDailyTasks = 3;
    private const int MaxHardDailyTasks = 2;

    public List<DailyTask> DailyTasks { get => _dailyTasks; set => _dailyTasks = value; }

    public event UnityAction FillTaskList;
    public event UnityAction NewDayStarted;

    private const float _saveDelay = 60;
    private float _elapsedSaveTime = 0;

     public void Initialize()
     {
         string lastTime = PlayerPrefs.GetString("LastTime");

         DateTime lastClaimTime;

         if (string.IsNullOrEmpty(lastTime) == false)
         {
             lastClaimTime = DateTime.Parse(lastTime);
         }
         else
         {
             lastClaimTime = DateTime.MinValue;
         }

         if (DateTime.Today > lastClaimTime)
         {
             NewDay();
             Debug.Log(1234);
         }
         else
         {
            LoadGame();
            Debug.Log(00000);
         }
     }

    private void Start()
    {
        Initialize();
        PlayerPrefs.SetString("LastTime", DateTime.Now.ToString());
    }

    private void Update()
    {
        int hourse = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalHours);
        int minutes = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalMinutes) % 60;
        _textDELETE.text = $"{hourse}:{minutes}";

        _elapsedSaveTime += Time.deltaTime;

        if (_elapsedSaveTime >= _saveDelay)
        {
            SaveGame();
            _elapsedSaveTime = 0;
            Debug.Log("Игра сохранена!");
        //    PlayerPrefs.GetString("LastTime", DateTime.Now.ToString());
        }
    }

    private void InitDailyTasks()
    {
        foreach (DailyTask task in _dailyTasks)
        {
            task.Initialize(_characterRewardGetter);
            task.ChangingCounter += OnChangedCounter;
        }

        FillTaskList?.Invoke();
    }

    private void FillDailyTasks(ref List<DailyTask> tempTasks, int count, ref List<int> indexList)
    {
        for (int i = 0; i < count; i++)
        {
            DailyTask randomTask = tempTasks[UnityEngine.Random.Range(0, tempTasks.Count)];

            indexList.Add(randomTask.Index);
            _dailyTasks.Add(Instantiate(randomTask, _taskContainer));
            _dailyTasks[_dailyTasks.Count - 1].Completed += CheckComlitedTasks;

            tempTasks.Remove(randomTask);
        }
    }

    private void InitTempLists()
    {
        foreach (var task in _easyTasks)
        {
            _tempEasyTasks.Add(task);
        }

        foreach (var task in _hardTasks)
        {
            _tempHardTasks.Add(task);
        }
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

    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();

        for (int i = 0; i < 3; i++)
        {
            data.EasyTasksIndexes.Add(_dailyTasks[i].Index);
        }

        for (int i = 3; i < 5; i++)
        {
            data.HardTasksIndexes.Add(_dailyTasks[i].Index);
        }

        for (int i = 0; i < _tasksProgress.Count; i++) 
        {
            data.TaskProgress[i] = _tasksProgress[i];
        }

        bf.Serialize(file, data);
        file.Close();
    }

    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            Debug.Log(0);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            Debug.Log(1);
            if (file.Length == 0)
            {
                Debug.Log(2);
                Init();
                return;
            }
            Debug.Log(3);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

         /*   if (data.EasyTasksIndexes.Count == 0)
            {
                Init();
                return;
            }*/

            _easyTasksIndexes = data.EasyTasksIndexes;
            _hardTasksIndexes = data.HardTasksIndexes;

            for (int i = 0; i < _easyTasksIndexes.Count; i++)
            {
                foreach (var task in _easyTasks)
                    if (task.Index == _easyTasksIndexes[i])
                    {
                        _dailyTasks.Add(Instantiate(task, _taskContainer));
                        _dailyTasks[_dailyTasks.Count - 1].Completed += CheckComlitedTasks;
                    }
            }

            for (int i = 0; i < _hardTasksIndexes.Count; i++)
            {
                foreach (var task in _hardTasks)
                    if (task.Index == _hardTasksIndexes[i])
                    {
                        _dailyTasks.Add(Instantiate(task, _taskContainer));
                        _dailyTasks[_dailyTasks.Count - 1].Completed += CheckComlitedTasks;
                    }
            }

            InitDailyTasks();

            for (int i = 0; i < _dailyTasks.Count; i++)
            {
                _tasksProgress[i] = data.TaskProgress[i];
                _dailyTasks[i].ChangeEquelCounter(data.TaskProgress[i]);
                _dailyTasks[i].CheckComplited();
            }
        }
        else
        {
            Debug.Log(-1);
            Init();
            return;
        }
    }

    public void Init()
    {
        Debug.Log("INIT");
        InitTempLists();

        FillDailyTasks(ref _tempEasyTasks, MaxEasyDailyTasks, ref _easyTasksIndexes);
        FillDailyTasks(ref _tempHardTasks, MaxHardDailyTasks, ref _hardTasksIndexes);

        InitDailyTasks();

        SaveGame();
    }

    public void NewDay()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);

            if (file.Length == 0)
            {
                Init();
                return;
            }

            _dailyTasks.Clear();
            _easyTasksIndexes.Clear();
            _hardTasksIndexes.Clear();
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _tempEasyTasks.Clear();
            _tempHardTasks.Clear();

            data.Reset();
            NewDayStarted?.Invoke();
            Init();
        }
        else
        {
            Init();
            return;
        }
    }

    private void OnChangedCounter(int counter)
    {
        for (int i = 0; i < _dailyTasks.Count; i++)
        {
            _tasksProgress[i] = _dailyTasks[i].CurrentCount;
        }
    }
}

[Serializable]
class SaveData
{
    public List<int> EasyTasksIndexes = new List<int>();
    public List<int> HardTasksIndexes = new List<int>();
    public List<float> TaskProgress = new List<float>() { 0, 0, 0, 0, 0};

    public void Reset()
    {
        EasyTasksIndexes.Clear();
        HardTasksIndexes.Clear();
        TaskProgress = new List<float>() { 0, 0, 0, 0, 0 };
    }
}