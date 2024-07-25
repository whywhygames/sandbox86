using CoverShooter;
using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [Header ("ENEMY TYPE:")]
    [SerializeField] private EnemyType _enemyType;

    [Header ("Parametrs:")]
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyRespawner _enemyRespawner;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Animator _animator;

    [field: SerializeField] public float RespawnTime { get; private set; }
    
    public bool IsDied { get; private set; }

    public event UnityAction<HitType, EnemyType> Died;
    public event UnityAction<EnemyHealth> Daying;
    public event UnityAction DayingFreez;
    public event UnityAction Freezed;

    private void Start()
    {
        _enemyRespawner.transform.parent = null;
    }

    public void Respawn()
    {
        transform.position = _enemyMovement.GetRandomPoint();
        IsDied = false;
        gameObject.SetActive(true);
    }

    public void OnHit(Hit hit)
    {
        if (_enemyMovement.IsFreez)
            TakeDamage(hit.Damage * 2.5f, hit.Type);
        else
            TakeDamage(hit.Damage, hit.Type); 
    }

    public void TakeDamage(float damage, HitType hitType)
    {
        if (IsDied)
            return;
        Debug.Log(damage);

        _currentHealth -= damage;
        _enemyMovement.AttackedTriggerActivate();

        if (_currentHealth <= 0)
        {
            if (_enemyMovement.IsFreez)
            {
                DayingFreez?.Invoke();
            }

            IsDied = true;
            Died?.Invoke(hitType, _enemyType);
            _animator.SetBool("IsStop", false);
            _animator.SetTrigger("Death");
            Daying?.Invoke(this);
        }
    }

    public void Death()
    {
        _currentHealth = _maxHealth;
        gameObject.SetActive(false);
    }

    public void Freez()
    {
        Freezed?.Invoke();
    }
}
