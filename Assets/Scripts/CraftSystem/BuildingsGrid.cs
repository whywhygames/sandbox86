using System;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEditor;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _directionPoint;
    [SerializeField] private List<Building> _poolObjects = new List<Building>();
    [SerializeField] private LayerMask _isCraftableMask;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxDistance;
    [SerializeField] private Transform _container;

    public Vector2Int GridSize = new Vector2Int(10000, 10000);
    private Vector3 _targetRotation;
    private Vector3 _newPosition;

    private Building[,] _grid;
    private Building _flyingBuilding;

    private RaycastHit _hit;
    private Ray _ray;

    private List<Building> _poolCreatedObjects = new List<Building>();

    private void Awake()
    {
        _grid = new Building[GridSize.x, GridSize.y];
        
        _mainCamera = Camera.main;

        foreach (var item in _poolObjects)
        {
            _poolCreatedObjects.Add(Instantiate(item, _container));
            _poolCreatedObjects[_poolCreatedObjects.Count - 1].gameObject.SetActive(false);
        }
    }

    public void StartPlacingBuilding(CraftType type)
    {
        if (_flyingBuilding != null)
        {
            _flyingBuilding.gameObject.SetActive(false);
        }

        foreach (var item in _poolCreatedObjects)
        {
            if (item.Type == type)
            {
                _flyingBuilding = item;
                Ray ray = new Ray(_mainCamera.transform.position, _directionPoint.position);
                Physics.Raycast(_mainCamera.transform.position, _directionPoint.forward * 10, out _hit, 10000, _isCraftableMask);

                _newPosition = _hit.point;

                if (Vector3.Distance(_mainCamera.transform.position, _hit.point) > _maxDistance)
                {
                    _newPosition = _ray.GetPoint(_maxDistance);
                }

                _flyingBuilding.transform.position = _newPosition;
                _flyingBuilding.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (_flyingBuilding != null)
        {
            _ray = new Ray(_mainCamera.transform.position, _directionPoint.forward * 10);

            if (Physics.Raycast(_ray,out _hit,10000,_isCraftableMask))
            {
                int x = Mathf.RoundToInt(_hit.point.x);
                int y = Mathf.RoundToInt(_hit.point.y);
                int z = Mathf.RoundToInt(_hit.point.z);

                if (Vector3.Distance(_mainCamera.transform.position, _hit.point) > _maxDistance)
                {
                    x = Mathf.RoundToInt(_ray.GetPoint(_maxDistance).x);
                    y = Mathf.RoundToInt(_ray.GetPoint(_maxDistance).y);
                    z = Mathf.RoundToInt(_ray.GetPoint(_maxDistance).z);
                }

                bool available = true;

                if (x < 0 || x > GridSize.x - _flyingBuilding.Size.x) available = false;
                if (z < 0 || z > GridSize.y - _flyingBuilding.Size.y) available = false;

                if (available && _flyingBuilding.CanPut == false) available = false;

                _newPosition = new Vector3(x, y, z);

                _flyingBuilding.transform.position = Vector3.Lerp(_flyingBuilding.transform.position, _newPosition, _speed * Time.deltaTime);
                _flyingBuilding.SetTransparent(available);

                if (available && TCKInput.GetAction(InputParametrs.CraftSystem.Craft, EActionEvent.Down)) 
                {
                    PlaceFlyingBuilding(x, z, _newPosition);
                }
            }

            CheckRotate();
        }
    }

    private void CheckRotate()
    {
        if (TCKInput.GetAction(InputParametrs.CraftSystem.RotateObject, EActionEvent.Down))
        {
            _targetRotation = new Vector3(_flyingBuilding.transform.eulerAngles.x, (_flyingBuilding.transform.eulerAngles.y + 90), _flyingBuilding.transform.eulerAngles.z);
            _flyingBuilding.transform.eulerAngles = _targetRotation;
          //  flyingBuilding.transform.eulerAngles = Vector3.Lerp(flyingBuilding.transform.eulerAngles, _targetRotation, _speed * Time.deltaTime);
        }

    }

    /* private bool IsPlaceTaken(int placeX, int placeY)
     {


         for (int x = 0; x < flyingBuilding.Size.x; x++)
         {
             for (int y = 0; y < flyingBuilding.Size.y; y++)
             {
                 if (grid[placeX + x, placeY + y] != null) return true;
             }
         }

         return false;
     }*/

    private void PlaceFlyingBuilding(int placeX, int placeY, Vector3 newPosition)
    {
        for (int x = 0; x < _flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.Size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _flyingBuilding;
            }
        }

        //   flyingBuilding.SetNormal();
        Instantiate(_flyingBuilding.Prefab, newPosition, _flyingBuilding.transform.rotation);
      //  flyingBuilding = null;
    }

    public void StopCraft()
    {
        if (_flyingBuilding != null)
            _flyingBuilding.gameObject.SetActive(false);

        _flyingBuilding = null;
    }
}
