using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private FightField _fightField;
    [field: SerializeField] public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public event UnityAction<int> ChangeHealth;
    public event UnityAction HalfHealth;
    public event UnityAction Died;

    private void Start()
    {
        Setup();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        ChangeHealth?.Invoke(CurrentHealth);

        if (CurrentHealth <= MaxHealth / 2)
        {
            HalfHealth?.Invoke();
            _fightField.gameObject.SetActive(true);
        }

        if (CurrentHealth <= 0)
        {
            transform.position = _startPoint.position;
            Died?.Invoke();
            Setup();
        }
    }

    private void Setup()
    {
        CurrentHealth = MaxHealth;
        ChangeHealth?.Invoke(CurrentHealth);
    }

    public void UpgradeMaxHp(int upgradeCount)
    {
        MaxHealth += upgradeCount;
        CurrentHealth = MaxHealth;
        ChangeHealth?.Invoke(CurrentHealth);
    }

    public void AddHealth(int count)
    {
        CurrentHealth += count;
        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
        ChangeHealth?.Invoke(CurrentHealth);
    }
}
