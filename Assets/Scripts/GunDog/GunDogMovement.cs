using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class GunDogMovement : MonoBehaviour
{
    [field: SerializeField] public bool IsAvailable {  get; private set; }

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
    [SerializeField] private float _followRadius;

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
        _health.Daying += OnDaying;
    }

    private void OnDaying()
    {
        _weapon.Close();
        _target = null;
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
        _health.Daying += OnDaying;
    }

    private void Update()
    {
        if (_health.IsDied)
        {
            _agent.isStopped = true;
            return;
        }
        if (IsFreez)
        { 
            return;
        }

        if (_isFollow)
        {
            _agent.SetDestination(_owner.transform.position);
            _agent.isStopped = false;
            _agent.speed = _followSpeed;
            _animator.SetFloat("Movement_f", 1f);
        }
        else if (_isAttacked == true /*&& _isFind == false*/)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) < _attackDistance)
            {
                _animator.SetBool("AttackReady_b", true);
                _animator.SetFloat("Movement_f", 0);
                _agent.isStopped = true;
                transform.LookAt(_target.transform.position, Vector3.up);
            }
            else
            {
                _animator.SetBool("AttackReady_b", false);
                _weapon.Close();
                _animator.SetFloat("Movement_f", 1);
                _agent.isStopped = false;
                transform.LookAt(_target.transform.position, Vector3.up);
                _agent.SetDestination(_target.transform.position);
            }
        }
        else
        {
            _weapon.Close();
            _animator.SetBool("AttackReady_b", false);
            Patrul();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isFollow && _target == null)
        {
            if (other.TryGetComponent(out FightField fightField))
            {
                FindTargetEnemy(_owner.transform.position);

                _isFollow = false;
            }
        }
    }

    private void OnEnemyDaying(EnemyHealth enemy)
    {
        _target = null;
        enemy.Daying -= OnEnemyDaying;

        FindTargetEnemy(_owner.transform.position);

        if (_target == null)
        {
            _isAttacked = false;
        }
    }

    private void FindTargetEnemy(Vector3 findPoint)
    {
        Collider[] collision = Physics.OverlapSphere(findPoint, _radius, _layerMask);

        if (collision.Length > 0)
        {
            foreach (Collider c in collision)
            {
                if (c.TryGetComponent(out EnemyHealth enemy))
                {
                    if (enemy.IsDied == false)
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

        if (Vector3.Distance(transform.position, _randomPoint) < _agent.stoppingDistance + 0.2f)
        {
            GetRandomPoint(_patrulField.transform.position, _patrulField.Radius);
            _animator.SetFloat("Movement_f", 0);
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
                    _animator.SetInteger("ActionType_int", Random.Range(1, 4));
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

    public Vector3 GetRandomPoint(Vector3 targetPosition, float radius)
    {
        bool getCorrectPoint = false;

        while (getCorrectPoint == false)
        {
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(Random.insideUnitSphere * /*_patrulField.Radius*/radius + targetPosition/*_patrulField.transform.position*/, out navMeshHit, radius/*_patrulField.Radius*/, NavMesh.AllAreas);

            _randomPoint = navMeshHit.position;

            _agent.CalculatePath(_randomPoint, _navMeshPath);

            if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
                getCorrectPoint = true;
        }

        return _randomPoint;
    }

    public Vector3 GetRandomPointInSpawn()
    {
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(Random.insideUnitSphere * _patrulField.Radius + _patrulField.transform.position, out navMeshHit, _patrulField.Radius, NavMesh.AllAreas);

        _randomPoint = navMeshHit.position;

        return _randomPoint;
    }

    public void SetAvailable()
    {
        IsAvailable = true;
    }

    public void Respawn()
    {
        if (IsAvailable == false)
            return;

        _health.Respwan();
        transform.position = GetRandomPointInSpawn();
        _isWait = Random.Range(0, 2) == 0 ? true : false;

        _waitTime = Random.Range(_waitTimeMin, _waitTimeMax);
        _readyToPatrulTime = _waitTime + Random.Range(_waitTimeMin, _waitTimeMax);
        _animator.SetFloat("Movement_f", 0);
        _isAttacked = false;
        IsFreez = false;
        GetRandomPoint(_patrulField.transform.position, _patrulField.Radius);
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
