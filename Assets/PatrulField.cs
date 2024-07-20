using UnityEngine;

public class PatrulField : MonoBehaviour
{
    [field: SerializeField] public float Radius { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, Radius);
    }
}
