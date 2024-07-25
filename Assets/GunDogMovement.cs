using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class GunDogMovement : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private PlayerHealth _owner;
    [SerializeField] private Animator _animator;
    [SerializeField] private GunDogHealth _health;
    [SerializeField] private FreezController _freezController;
    
    private NavMeshPath _navMeshPath;

    [Header ("Patrul")]
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
    private bool _isAttacked;

    [Header ("Attack")]
    [Header ("Follow")]

   private bool _isFreez;

    public event UnityAction Freezed;

    public bool IsFreez { get => _isFreez; set => _isFreez = value; }

    private void OnEnable()
    {
        _freezController.Freezing += OnFreez;
        _freezController.Defreezing += OnDefreez;
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

        Patrul();
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

    public void StopAction()
    {
        _animator.SetInteger("ActionType_int", 0);
    }

    public Vector3 GetRandomPoint()
    {
        /*  Vector3 randomPointInCircle = Random.insideUnitSphere * _patrulField.Radius;
          _randomPoint = randomPointInCircle + _patrulField.transform.position;
          _randomPoint.y = 0;

          return _randomPoint;*/

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
    }

    private void OnFreez()
    {
    }
}
