using UnityEngine;

public class PatrulField : MonoBehaviour
{
    [field: SerializeField] public float Radius { get; private set; }

    [SerializeField] private Color _color;
    [SerializeField] private bool _isShow;

    private void OnDrawGizmos()
    {
        if (_isShow)
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, Radius);
        }
    }
}
