using UnityEngine;

public class BugChangeMaterial : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _meshRenedereBody;
    [SerializeField] private SkinnedMeshRenderer _meshRenedereEye;

    [SerializeField] private Material[] _materalsBody;
    [SerializeField] private Material[] _materalsEye;

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

        _meshRenedereEye.materials = _materalsEye;
    }

    private void OnFreez()
    {
        _meshRenedereBody.materials = _freezMaterial;

        _meshRenedereEye.materials = _freezMaterial;
    }
}
