using UnityEngine;

public class DogChangeMaterial : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _meshRenedereBody;

    [SerializeField] private Material[] _materalsBody;

    [SerializeField] private Material[] _freezMaterial;

    [SerializeField] private FreezController _freezController;


    private void OnEnable()
    {
        _freezController.Freezing += OnFreez;
        _freezController.Defreezing += OnDefreez;
    }

    private void OnDisable()
    {
        _freezController.Freezing -= OnFreez;
        _freezController.Defreezing -= OnDefreez;
    }

    private void OnDefreez()
    {
        _meshRenedereBody.materials = _materalsBody;
    }

    private void OnFreez()
    {
        _meshRenedereBody.materials = _freezMaterial;
    }
}
