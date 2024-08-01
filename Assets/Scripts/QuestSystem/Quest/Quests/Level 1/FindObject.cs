using UnityEngine;
using UnityEngine.Events;

public class FindObject : MonoBehaviour
{
    [SerializeField] private LayerMask _characterMask;

    [SerializeField] private float _triggerRadius = 3;

    public event UnityAction<FindObject> Finded;
    private bool _isFind;

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, _triggerRadius, _characterMask) && _isFind == false)
        {
            Finded?.Invoke(this);
            _isFind = true;
        }
    }

    public void Setup()
    {
        _isFind = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, _triggerRadius);
    }
}
