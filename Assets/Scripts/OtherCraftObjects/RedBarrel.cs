using UnityEngine;
using CoverShooter;

public class RedBarrel : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private ParticleSystem _explotion;
    [SerializeField] private AudioSource _explotionSound;

    public void OnHit(Hit hit)
    {
        TakeDamage(hit.Damage);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _explotion.transform.parent = null;
            _explotion.Play();
            _explotionSound.Play();
            Destroy(gameObject);
        }
    }
}
