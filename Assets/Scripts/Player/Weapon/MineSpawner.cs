using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.Events;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private Mine _minePrefab;
    [SerializeField] private Transform _mineSpawnPoint;
    [SerializeField] private AudioClip _spawnMineSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Transform _mineContainer;

    [field: SerializeField] public int MineCount;
    
    private int MineMaxCount = 50;
    
    public event UnityAction MineSpawned;

    private List<Mine> _pools = new List<Mine>();


    private void Start()
    {
        for (int i = 0; i < MineMaxCount; i++)
        {
            _pools.Add(Instantiate(_minePrefab));
            _pools[_pools.Count - 1].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (TCKInput.GetButtonDown(InputParametrs.MineBUTTON))
        {
            if (MineCount > 0)
            {
                CreateMine();
                _audioSource.PlayOneShot(_spawnMineSound);
                MineSpawned?.Invoke();
            }
        }
    }

    private void CreateMine()
    {
        MineCount--;
        Mine mine = GetFreeMine();
        mine.transform.rotation = Quaternion.identity;
        mine.transform.position = _mineSpawnPoint.position;
        mine.gameObject.SetActive(true);
    }

    private Mine GetFreeMine()
    {
        foreach (var mine in _pools)
        {
            if (mine.gameObject.activeSelf == false)
            {
                return mine;
            }
        }

        return null;
    }

    public void AddMine(int count)
    {
        MineCount += count;
        MineSpawned?.Invoke();
    }
}
