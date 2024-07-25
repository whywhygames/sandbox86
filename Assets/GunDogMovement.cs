using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class GunDogMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private PlayerHealth _owner;
    [SerializeField] private Animator _animator;
    [SerializeField] private GunDogHealth _health;
    [SerializeField] private FreezController _freezController;

    private NavMeshPath _navMeshPath;

    [Header("Patrul")]
    [SerializeField] private PatrulField _patrulField;
    [SerializeField] private float _waitTimeMin;
    [SerializeField] private float _waitTimeMax;
    [SerializeField] private float _patrulSpeed;

    private Vector3 _randomPoint;
    private bool _isWait;
    private bool _isActionComplited;
    private float _waitTime;
    private float _readyToPatrulTime;
    private float _readyToPatrulElepsedTime;
    private float _elepsedWaitTime;

    [Header("Attack")]
    [SerializeField] private Transform _target;
    [SerializeField] private bool _isAttacked;
    [SerializeField] private DogWeapon _weapon;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    [Header("Follow")]
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _followDistance;

    private bool _isFollow;
    private bool _isFind;
    private bool _isFreez;

    public event UnityAction Freezed;

    public bool IsFreez { get => _isFreez; set => _isFreez = value; }

    private void OnEnable()
    {
        _freezController.Freezing += OnFreez;
        _freezController.Defreezing += OnDefreez;
        _owner.HalfHealth += OnHalfHealth;
    }

    private void Start()
    {
        _navMeshPath = new NavMeshPath();
        Respawn();
    }

    private void OnDisable()
    {
        _freezController.Freezing -= OnFreez;
        _freezController.Defreezing -= OnDefreez;
        _owner.HalfHealth -= OnHalfHealth;
    }

    private void Update()
    {
        if (_health.IsDied)
        {
            _agent.isStopped = true;
            return;
        }
        if (IsFreez)
            return;


        if (_isFollow)
        {
            if (Vector3.Distance(transform.position, _owner.transform.position) > _followDistance)
            {
                _agent.SetDestination(_owner.transform.position);
                _agent.isStopped = false;
                _agent.speed = _followSpeed;
                _animator.SetFloat("Movement_f", 1f);
            }
            else
            {
                _agent.isStopped = true;
                _animator.SetFloat("Movement_f", 0f);
                _isFollow = false;
                _isFind = true;
            }
        }
        else if (_isFollow == false && _isFind == true)
        {
            if (_target == null)
            {
                Collider[] collision = Physics.OverlapSphere(transform.position, _radius, _layerMask);

                if (collision.Length > 0)
                {
                    foreach (Collider c in collision)
                    {
                        if (c.TryGetComponent(out EnemyHealth enemy))
                        {
                            _target = enemy.transform;
                            enemy.Daying += OnEnemyDaying;
                            _isFind = false;
                            _isAttacked = true;
                        }
                    }
                }
            }
        }
        else if (_isAttacked == true && _isFind == false)
        {
            _animator.SetBool("AttackReady_b", true);
            _animator.SetFloat("Movement_f", 0);
            _agent.isStopped = true;
            transform.LookAt(_target.transform.position, Vector3.up);
        }
        else
        {
            _weapon.Close();
            _animator.SetBool("AttackReady_b", false);
            Patrul();
        }
    }

    private void OnEnemyDaying(EnemyHealth enemy)
    {
        _target = null;
        enemy.Daying -= OnEnemyDaying;

        Collider[] collision = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        if (collision.Length > 0)
        {
            foreach (Collider c in collision)
            {
                if (c.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    if (enemyHealth.IsDied == false)
                    {
                        _target = enemyHealth.transform;
                        enemyHealth.Daying += OnEnemyDaying;
                    }
                }
            }
        }

        if (_target == null)
        {
            _isAttacked = false;
        }
    }

    private void OnHalfHealth()
    {
        if (_isAttacked == false && _isFind == false) 
        {
            _isFollow = true;
        }
    }

    public void OpenWeapon() //Вызывается анимацией
    {
        _weapon.Open();
    }

    private void Patrul()
    {
        _agent.speed = _patrulSpeed;

        if (Vector3.Distance(transform.position, _randomPoint) < 0.2)
        {
            GetRandomPoint();
            _isWait = true;
        }

        if (_isWait)
        {
            _agent.isStopped = true;
            _animator.SetFloat("Movement_f", 0);

            if (_isActionComplited == false)
            {
                _elepsedWaitTime += Time.deltaTime;

                if (_elepsedWaitTime >= _waitTime)
                {
                    _isActionComplited = true;
                    _animator.SetInteger("ActionType_int", Random.Range(1, 5));
                    _elepsedWaitTime = 0;
                }
            }
            else
            {
                _readyToPatrulElepsedTime += Time.deltaTime;

                if (_readyToPatrulElepsedTime >= _readyToPatrulTime)
                {
                    _isWait = false;
                    _isActionComplited = false;
                    _readyToPatrulElepsedTime = 0;
                    //_animator.SetFloat("Movement_f", 0.4f);
                }
            }
        }
        else
        {
            _animator.SetFloat("Movement_f", 0.4f);
            _agent.isStopped = false;
            _agent.SetDestination(_randomPoint);
        }
    }

    public void StopAction() //Вызывается анимацией
    {
        _animator.SetInteger("ActionType_int", 0);
    }

    public Vector3 GetRandomPoint()
    {
        bool getCorrectPoint = false;

        while (getCorrectPoint == false)
        {
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(Random.insideUnitSphere * _patrulField.Radius + _patrulField.transform.position, out navMeshHit, _patrulField.Radius, NavMesh.AllAreas);

            _randomPoint = navMeshHit.position;

            _agent.CalculatePath(_randomPoint, _navMeshPath);

            if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
                getCorrectPoint = true;
        }

        return _randomPoint;
    }

    public void Respawn()
    {
        _isWait = Random.Range(0, 2) == 0 ? true : false;

        _waitTime = Random.Range(_waitTimeMin, _waitTimeMax);
        _readyToPatrulTime = _waitTime + Random.Range(_waitTimeMin, _waitTimeMax);
        _animator.SetFloat("Movement_f", 0);
        _isAttacked = false;
        IsFreez = false;
        GetRandomPoint();
    }

    private void OnDefreez()
    {
        IsFreez = false;
        _agent.isStopped = false;
    }

    private void OnFreez()
    {
        IsFreez = true;
        _agent.isStopped = true;
        _animator.SetFloat("Movement_f", 0);
        Freezed?.Invoke();
        _health.Freez();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
