using UnityEngine;
using UnityEngine.Events;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int _addMoneyCount;
    public int CurrentMoney { get; private set;}

    public event UnityAction<int> ChangeMoney;

    private void Start()
    {
        ChangeMoney?.Invoke(CurrentMoney);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Money monye))
        {
            AddMonye(_addMoneyCount);
            Destroy(monye.gameObject);
        }
    }

    public void AddMonye(int count)
    {
        CurrentMoney += count;
        ChangeMoney?.Invoke(CurrentMoney);
    }
}
