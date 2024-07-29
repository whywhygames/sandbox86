using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [field: SerializeField] public string Description { get; set; }
    [field: SerializeField] public float TargerCount { get; set; }

    [SerializeField] private Quest TargerQuest;
    [SerializeField] private List<TaskReward> _rewards = new List<TaskReward>();
    [SerializeField] private LayerMask _characterMask;
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _questPointer;

    [SerializeField] private UnityEvent<Quest> _started;
    [SerializeField] private UnityEvent _complited;
    [SerializeField] private UnityEvent _show;
    [SerializeField] private UnityEvent _hide;


    public bool IsCompleted { get; private set; }
    public bool IsStarted { get; private set; }
    public float CurrentCount { get; protected set; }
    public List<TaskReward> Rewards { get => _rewards; private set => _rewards = value; }

    private CharacterRewardGetter _rewardGetter;
    private bool _isShow;
    private const float _triggerRadius = 3;

    public event UnityAction<Quest> Started
    {
        add => _started.AddListener(value);
        remove => _started.RemoveListener(value);
    }

    public event UnityAction Completed
    {
        add => _complited.AddListener(value);
        remove => _complited.RemoveListener(value);
    }

    public event UnityAction Show
    {
        add => _show.AddListener(value);
        remove => _show.RemoveListener(value);
    }

    public event UnityAction Hide
    {
        add => _hide.AddListener(value);
        remove => _hide.RemoveListener(value);
    }

    public event UnityAction<float, float> ChangedCounter;
    public event UnityAction Stopped;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartQuest);
    }

    private void Start()
    {
        if (TargerQuest != null)
            TargerQuest.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartQuest);
    }

    private void Update()
    {
        if (IsStarted)
            return;

        if (Physics.CheckSphere(transform.position, _triggerRadius, _characterMask))
        {
            if (_isShow == false) 
            {
                _show?.Invoke();
                _isShow = true;
            }
        }
        else
        {
            if (_isShow)
            {
                _hide?.Invoke();
                _isShow = false;
            }
        }
    }

    public void Initialize(CharacterRewardGetter rewardGetter)
    {
        _rewardGetter = rewardGetter;
    }

    public virtual void Setup()
    {
        _questPointer.SetActive(true);
        IsStarted = false;
        Stopped?.Invoke();
        CurrentCount = 0;
    }

    protected virtual void StartQuest()
    {
        _started?.Invoke(this);
        _questPointer.SetActive(false);
        IsStarted = true;
        _hide?.Invoke();
        _isShow = false;
        ChangedCounter?.Invoke(TargerCount, CurrentCount);
    }

    public void GiveReward()
    {
        IsCompleted = true;
        _rewardGetter.TakeReward(_rewards);
        _complited?.Invoke();

        if (TargerQuest != null)
            TargerQuest.gameObject.SetActive(true);
    }

    public void AddCounter(float value)
    {
        CurrentCount += value;
        ChangedCounter?.Invoke(TargerCount, CurrentCount);
    }

    public void ChangeEquelCounter(float value)
    {
        CurrentCount = value;
        ChangedCounter?.Invoke(TargerCount, CurrentCount);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        Gizmos.DrawSphere(transform.position, _triggerRadius);
    }
}