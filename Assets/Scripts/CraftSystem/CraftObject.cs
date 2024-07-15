using UnityEngine;

public class CraftObject : MonoBehaviour
{
    [Header ("Materials")]
    [SerializeField] private Material _posibleMaterial;
    [SerializeField] private Material _imposibleMaterial;
    [SerializeField] private MeshRenderer _renderer;

    [Header("Parametrs")]
    [SerializeField] private Vector3 _cubeScale;
    [SerializeField] private LayerMask _layerMask;

    [field: SerializeField] public Transform Prefab { get; private set; }
    [field: SerializeField] public CraftType Type { get; private set; }

    public bool CanPut { get; private set; }
    private void Update()
    {
        Collider[] hits = Physics.OverlapBox(_renderer.transform.position, _cubeScale, transform.rotation, _layerMask);

        if (hits.Length > 0)
        {
            _renderer.material = _imposibleMaterial;
            CanPut = false;
        }
        else
        {
            _renderer.material = _posibleMaterial;
            CanPut = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(_renderer.transform.position, _cubeScale);
    }
}
