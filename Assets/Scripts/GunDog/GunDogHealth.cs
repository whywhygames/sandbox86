using CoverShooter;
using UnityEngine;
using UnityEngine.Events;

public class GunDogHealth : MonoBehaviour
{
    [Header("Parametrs:")]
    [SerializeField] private GunDogMovement _enemyMovement;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Animator _animator;

    public bool IsDied { get; private set; }

    public event UnityAction Daying;
    public event UnityAction DayingFreez;
    public event UnityAction Freezed;

    public void OnHit(Hit hit)
    {
        if (hit.Attacker.TryGetComponent(out GunDogHealth health))
            return;

        if (_enemyMovement.IsFreez)
            TakeDamage(hit.Damage * 2.5f, hit.Type);
        else
            TakeDamage(hit.Damage, hit.Type);
    }

    public void TakeDamage(float damage, HitType hitType)
    {
        if (IsDied)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            if (_enemyMovement.IsFreez)
            {
                DayingFreez?.Invoke();
            }

            IsDied = true;
            _animator.speed = 1;
            _animator.SetInteger("ActionType_int", 0); 
            _animator.SetFloat("Movement_f", 0); 
            _animator.SetBool("AttackReady_b", false);
            _animator.SetBool("Death_b", true);
            Daying?.Invoke();
        }
    }

    public void Respwan()
    {
        _currentHealth = _maxHealth;
        IsDied = false;
        gameObject.SetActive(true);
    }

    public void Death()
    {
        _currentHealth = _maxHealth;
        IsDied = false;
        gameObject.SetActive(false);
    }

    public void Freez()
    {
        Freezed?.Invoke();
    }
}