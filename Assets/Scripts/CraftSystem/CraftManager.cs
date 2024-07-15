using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    [Header("Parametrs")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _lenghtRay;
    [SerializeField] private Transform _directionPoint;
    [SerializeField] private LayerMask _isCraftableMask;

    [Header("ListObjects")]
    [SerializeField] private List<CraftObject> _poolObjects = new List<CraftObject>();

    private CraftObject _currentObject;
    private List<CraftObject> _poolCreatedObjects = new List<CraftObject>();

    private bool _isEnable;
    private RaycastHit hit;

    private void Awake()
    {
        foreach (var item in _poolObjects)
        {
            _poolCreatedObjects.Add(Instantiate(item));
            _poolCreatedObjects[_poolCreatedObjects.Count - 1].gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.C))
            _isEnable = !_isEnable;

        if (_isEnable == false)
            return;

        if (_currentObject == null)
            return;

        if (Physics.Raycast(_camera.transform.position, _directionPoint.forward * 10, out hit, _lenghtRay, _isCraftableMask))
        {
            if (hit.collider)
            {
                _currentObject.transform.position = hit.point;
                _currentObject.gameObject.SetActive(true);

                Craft();
            }
        }
        else
        {
            _currentObject.gameObject.SetActive(false);
        }
    }

    private void Craft()
    {
        if (TCKInput.GetAction(InputParametrs.Craft, EActionEvent.Down))
        {
            if (_currentObject.CanPut)
            {
                Instantiate(_currentObject.Prefab, _currentObject.transform.position, _currentObject.transform.rotation);
            }
        }
    }

    public void SetCraftObject(CraftType type)
    {
        if (_currentObject != null)
            _currentObject.gameObject.SetActive(false);

        foreach (var item in _poolCreatedObjects)
        {
            if (item.Type == type)
            {
                _currentObject = item;
            }
        }
    }
}
