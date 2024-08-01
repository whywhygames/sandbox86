using System.Collections.Generic;
using UnityEngine;

public class FindObjectQuest : MonoBehaviour
{
    [SerializeField] private List<FindObject> _targetFindObjects = new List<FindObject>();
    [SerializeField] private Quest _questObject;

    private void OnEnable()
    {
        _questObject.Started += StartQuest;
        _questObject.Stopped += Setup;

        foreach (FindObject obj in _targetFindObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        foreach (FindObject obj in _targetFindObjects)
        {
            obj.Finded += OnFinded;
        }
    }

    private void StartQuest(Quest arg0)
    {
        foreach (FindObject obj in _targetFindObjects)
        {
            obj.gameObject.SetActive(true);
        }
    }

    private void Setup()
    {
        foreach (FindObject obj in _targetFindObjects)
        {
            obj.Setup();
            obj.gameObject.SetActive(false);
        }
    }

    private void OnFinded(FindObject findObject)
    {
        _questObject.AddCounter(1);
        findObject.gameObject.SetActive(false);
        Debug.Log(123);

        if (_questObject.CurrentCount == _questObject.TargerCount)
        {
            _questObject.GiveReward();
        }
    }
}
