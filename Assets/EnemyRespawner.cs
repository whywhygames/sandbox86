using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private EnemyMovement _enemyMovement;

    private float _respawnTime;
    private float _elapsedTime;

    private void Start()
    {
        _respawnTime = _enemyHealth.RespawnTime;
    }

    private void Update()
    {
        if (_enemyHealth.IsDied)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _respawnTime)
            {
                _enemyHealth.Respawn(); 
                _enemyMovement.Respawn();
                _elapsedTime = 0;
            }
        }
    }
}
