using CoverShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Animator _animator;
    
    public bool IsDied { get; private set; }

    public void OnHit(Hit hit)
    {
        TakeDamage(hit.Damage);  
    }

    public void TakeDamage(float damage)
    {
        if (IsDied)
            return;

        _currentHealth -= damage;
        _enemyMovement.AttackedTriggerActivate();

        if (_currentHealth <= 0)
        {
            IsDied = true;
            _animator.SetTrigger("Death");
        }
    }

    public void Death()
    {
        _currentHealth = _maxHealth;
        gameObject.SetActive(false);
        IsDied = false;
    }
}
