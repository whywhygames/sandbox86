using UnityEngine;

public class Building : MonoBehaviour
{
    [field: SerializeField] public CraftType Type { get; private set; }
    [field: SerializeField] public Transform Prefab { get; private set; }

    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;

    [Header("Materials")]
    [SerializeField] private Material _posibleMaterial;
    [SerializeField] private Material _imposibleMaterial;

    [Header("Parametrs")]
    [SerializeField] private Vector3 _cubeScale;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _cubePoint;
    public bool CanPut { get; private set; }

    public void SetTransparent(bool available)
    {
        /*if (available)
        {
            MainRenderer.material = _posibleMaterial;
        }
        else
        {
            MainRenderer.material = _imposibleMaterial;
        }*/
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapBox(_cubePoint.position, _cubeScale, transform.rotation, _layerMask);

        Debug.Log(hits.Length);
        if (hits.Length > 0)
        {
            MainRenderer.material = _imposibleMaterial;
            CanPut = false;
        }
        else
        {
            MainRenderer.material = _posibleMaterial;
            CanPut = true;
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }

        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(_cubePoint.position, _cubeScale * 2);
    }
}