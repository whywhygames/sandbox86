using CoverShooter;
using UnityEngine;
using UnityEngine.Events;

public class GunDogHealth : MonoBehaviour
{
  //  [Header("ENEMY TYPE:")]
 //   [SerializeField] private EnemyType _enemyType;

    [Header("Parametrs:")]
    [SerializeField] private GunDogMovement _enemyMovement;
    //[SerializeField] private EnemyRespawner _enemyRespawner;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Animator _animator;

    public bool IsDied { get; private set; }

   // public event UnityAction<HitType, EnemyType> Died;
    public event UnityAction Daying;
    public event UnityAction DayingFreez;
    public event UnityAction Freezed;

  /*  private void Start()
    {
        _enemyRespawner.transform.parent = null;
    }*/

 /*   public void Respawn()
    {
        transform.position = _enemyMovement.GetRandomPoint();
        IsDied = false;
        gameObject.SetActive(true);
    }*/

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

        _currentHealth -= damage;
       // _enemyMovement.AttackedTriggerActivate();

        if (_currentHealth <= 0)
        {
            if (_enemyMovement.IsFreez)
            {
                DayingFreez?.Invoke();
            }

            IsDied = true;
            //   Died?.Invoke(hitType, _enemyType);
            _animator.SetInteger("ActionType_int", 0); 
            _animator.SetFloat("Movement_f", 0); 
            _animator.SetBool("AttackReady_b", false);
            _animator.SetBool("Death_b", true);
            Daying?.Invoke();
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