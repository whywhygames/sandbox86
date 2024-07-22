using CoverShooter;
using UnityEngine;
using UnityEngine.Events;

public class GrenadeInventoryCounter : MonoBehaviour
{
    [field: SerializeField] public int CountGrenade;

    [SerializeField] private ThirdPersonController _thirdPersonController;

    public event UnityAction ChangeCount;
    public event UnityAction GrenadSpawned;

    public void OnEnable()
    {
        _thirdPersonController.ThrowGrenadeEvent += UpdateCounter;
    }

    private void OnDisable()
    {
        _thirdPersonController.ThrowGrenadeEvent -= UpdateCounter;
    }

    private void UpdateCounter()
    {
        CountGrenade--;
        ChangeCount?.Invoke();
        GrenadSpawned?.Invoke();
    }

    public void AddMine(int count)
    {
        CountGrenade += count;
        ChangeCount?.Invoke();

    }
}
