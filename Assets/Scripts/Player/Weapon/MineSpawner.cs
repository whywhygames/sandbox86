using TouchControlsKit;
using UnityEngine;
using UnityEngine.Events;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private Mine _minePrefab;
    [SerializeField] private Transform _mineSpawnPoint;
    [SerializeField] private AudioClip _spawnMineSound;
    [SerializeField] private AudioSource _audioSource;

    [field: SerializeField] public int MineCount;
    
    public event UnityAction MineSpawned;

    private void Update()
    {
        if (TCKInput.GetButtonDown(InputParametrs.Mine))
        {
            if (MineCount > 0)
            {
                MineCount--;
                Instantiate(_minePrefab, _mineSpawnPoint.position, Quaternion.identity);
                _audioSource.PlayOneShot(_spawnMineSound);
                MineSpawned?.Invoke();
            }
        }
    }

    public void AddMine(int count)
    {
        MineCount += count;
        MineSpawned?.Invoke();
    }
}
