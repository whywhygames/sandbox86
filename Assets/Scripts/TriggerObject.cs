using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private UnityEvent _show;
    [SerializeField] private UnityEvent _hide;
    [SerializeField] private LayerMask _characterMask;

    private const float _triggerRadius = 3;
    private bool _isShow;

    public event UnityAction Show
    {
        add => _show.AddListener(value);
        remove => _show.RemoveListener(value);
    }

    public event UnityAction Hide
    {
        add => _hide.AddListener(value);
        remove => _hide.RemoveListener(value);
    }
    private void Update()
    {
        if (Physics.CheckSphere(transform.position, _triggerRadius, _characterMask))
        {
            if (_isShow == false)
            {
                _show?.Invoke();
                _isShow = true;
            }
        }
        else
        {
            if (_isShow)
            {
                _hide?.Invoke();
                _isShow = false;
            }
        }
    }
}