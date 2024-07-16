using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private float _time;

    private float _elepsedTime;
    private Transform _target;
    private bool _isActive;
    private bool _isStop;
    
    private void Update()
    {
        if (_isActive && _isStop == false)
        {
            _elepsedTime += Time.deltaTime;

            if (_elepsedTime > _time )
            {
                _target.transform.position = GetRandomTeleportPoint();
                _elepsedTime = 0;
                _target = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isActive = true;
        _target = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        _isActive = false;
        _elepsedTime = 0;
        _target = null;
        _isStop = false;
    }

    public void StopTeleport()
    {
        _isStop = true;
    }

    private Vector3 GetRandomTeleportPoint()
    {
        Teleport[] teleports = FindObjectsOfType<Teleport>();

        if (teleports.Length > 1)
        {
            foreach (var teleport in teleports)
                if (teleport.gameObject != gameObject)
                {
                    var randomTeleport = teleports[Random.Range(0, teleports.Length)];
                    randomTeleport.StopTeleport();
                    return randomTeleport.transform.position;
                }
        }

        return Vector3.zero;
    }
}
