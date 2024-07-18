using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private PlayerHealth _target;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyHealth _health;

    [Header("Patrul")]
    [SerializeField] private float _followDistance;
    [SerializeField] private float _followSpeed;
    
    private bool _isFollow = true;

    private const float _triggerTime = 20;
    private float _elepsedTriggerTime;
    private bool _isAttacked;

    [Header("Attack")]
    [SerializeField] private float _attackPause;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDistance;

    private float _elepsedTimeAttackPause;

    [Header("Patrul")]
    [SerializeField] private SphereCollider _patrulField;
    [SerializeField] private float _waitTimeMin;
    [SerializeField] private float _waitTimeMax;
    [SerializeField] private float _patrulDistance;
    [SerializeField] private float _patrulSpeed;

    private Vector3 _randomPoint;
    private bool _isWait;
    private float _waitTime;
    private float _readyToPatrulTime;
    private float _readyToPatrulElepsedTime;
    private float _elepsedWaitTime;

    private void Start()
    {
        Respawn();
    }

    private void Update()
    {
        if (_health.IsDied)
        {
            _agent.isStopped = true;
            return;
        }

        if (Vector3.Distance(transform.position, _target.transform.position) < _followDistance || _isAttacked)
        {
            Follow();
        }
        else if (Vector3.Distance(transform.position, _target.transform.position) > _patrulDistance && _isAttacked == false)
        {
            Patrul();
        }

        if (_isAttacked == true)
        {
            _elepsedTriggerTime += Time.deltaTime;

            if (_elepsedTriggerTime >= _triggerTime)
            {
                _isAttacked = false;
                _elepsedTriggerTime = 0;
            }
        }
    }

    public void Respawn()
    {
        _isWait = Random.Range(0, 2) == 0 ? true : false;

        _waitTime = Random.Range(_waitTimeMin, _waitTimeMax);
        _readyToPatrulTime = _waitTime + Random.Range(_waitTimeMin, _waitTimeMax);
        _animator.SetBool("Walk", true);
        _isAttacked = false;

        GetRandomPoint();
    }

    public void AttackedTriggerActivate()
    {
        _isAttacked = true;
    }

    public void Attack()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < _attackDistance)
        {
            _target.TakeDamage(_damage);
        }
    }

    private void Follow()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < _attackDistance)
        {
            transform.LookAt(_target.transform.position);
            _animator.SetTrigger("Attack");
            _isFollow = false;
            _agent.isStopped = true;
        }
        else
        {
            if (_isFollow == false)
            {
                _elepsedTimeAttackPause += Time.deltaTime;

                if (_elepsedTimeAttackPause > _attackPause)
                {
                    _isFollow = true;
                    _elepsedTimeAttackPause = 0;
                }
            }
            else
            {
                _agent.isStopped = false;
                _agent.SetDestination(_target.transform.position);
                _agent.speed = _followSpeed;
                _animator.SetBool("Run", true);
                _animator.SetBool("Walk", false);

            }
        }
    }

    private void Patrul()
    {
     //   _animator.SetBool("Run", false);
        _agent.speed = _patrulSpeed;

        if (Vector3.Distance(transform.position, _randomPoint) < 0.2)
        {
            GetRandomPoint();
            _animator.SetBool("Walk", false);
            _isWait = true;
        }

        if (_isWait)
        {
            _agent.isStopped = true;
            _elepsedWaitTime += Time.deltaTime;
            _readyToPatrulElepsedTime += Time.deltaTime;

            if (_elepsedWaitTime >= _waitTime)
            {
                if (Random.Range(0, 2) == 0)
                {
                    _animator.SetTrigger("Alert");
                }
                else
                {
                    _animator.SetTrigger("Blink");
                }

                _elepsedWaitTime = 0;
            }

            if (_readyToPatrulElepsedTime  >= _readyToPatrulTime)
            {
                _isWait = false;
                _readyToPatrulElepsedTime = 0;
                _animator.SetBool("Walk", true);
                _animator.SetBool("Run", false);
            }
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(_randomPoint);
        }
    }

    public Vector3 GetRandomPoint()
    {
        Vector3 randomPointInCircle = Random.insideUnitSphere * _patrulField.radius;
        _randomPoint = randomPointInCircle + _patrulField.transform.position;
        _randomPoint.y = 0;

        return _randomPoint;
    }
}
