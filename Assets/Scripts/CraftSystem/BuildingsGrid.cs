using System;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform _directionPoint;
    [SerializeField] private List<Building> _poolObjects = new List<Building>();
    [SerializeField] private LayerMask _isCraftableMask;
    [SerializeField] private float _speed;

    public Vector2Int GridSize = new Vector2Int(10000, 10000);
    private Vector3 _targetRotation;

    private Building[,] grid;
    private Building flyingBuilding;

    private RaycastHit hit;

    private List<Building> _poolCreatedObjects = new List<Building>();

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];
        
        mainCamera = Camera.main;

        foreach (var item in _poolObjects)
        {
            _poolCreatedObjects.Add(Instantiate(item));
            _poolCreatedObjects[_poolCreatedObjects.Count - 1].gameObject.SetActive(false);
        }
    }

    public void StartPlacingBuilding(CraftType type)
    {
        if (flyingBuilding != null)
        {
            flyingBuilding.gameObject.SetActive(false);
        }

        foreach (var item in _poolCreatedObjects)
        {
            if (item.Type == type)
            {
                flyingBuilding = item;
                Ray ray = new Ray(mainCamera.transform.position, _directionPoint.position);
                Physics.Raycast(mainCamera.transform.position, _directionPoint.forward * 10, out hit, 10000, _isCraftableMask);
                flyingBuilding.transform.position = hit.point;
                flyingBuilding.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (flyingBuilding != null)
        {
            Ray ray = new Ray(mainCamera.transform.position, _directionPoint.position);
            Physics.Raycast(ray);

            if (Physics.Raycast(mainCamera.transform.position, _directionPoint.forward * 10,out hit,10000,_isCraftableMask))
            {

                int x = Mathf.RoundToInt(hit.point.x);
                int y = Mathf.RoundToInt(hit.point.y);
                int z = Mathf.RoundToInt(hit.point.z);

                bool available = true;

                if (x < 0 || x > GridSize.x - flyingBuilding.Size.x) available = false;
                if (z < 0 || z > GridSize.y - flyingBuilding.Size.y) available = false;

                if (available && flyingBuilding.CanPut == false) available = false;

             //   flyingBuilding.transform.position = new Vector3(x, y, z);
                flyingBuilding.transform.position = Vector3.Lerp(flyingBuilding.transform.position, new Vector3(x, y, z), _speed * Time.deltaTime);
                flyingBuilding.SetTransparent(available);

                if (available && TCKInput.GetAction(InputParametrs.Craft, EActionEvent.Down)) 
                {
                    PlaceFlyingBuilding(x, z);
                }
            }

            CheckRotate();
        }
    }

    private void CheckRotate()
    {
        if (TCKInput.GetAction(InputParametrs.RotateObject, EActionEvent.Down))
        {
            _targetRotation = new Vector3(flyingBuilding.transform.eulerAngles.x, (flyingBuilding.transform.eulerAngles.y + 90), flyingBuilding.transform.eulerAngles.z);
            flyingBuilding.transform.eulerAngles = _targetRotation;
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

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingBuilding;
            }
        }

        //   flyingBuilding.SetNormal();
        Instantiate(flyingBuilding.Prefab, flyingBuilding.transform.position, flyingBuilding.transform.rotation);
      //  flyingBuilding = null;
    }

    public void StopCraft()
    {
        flyingBuilding.gameObject.SetActive(false);
        flyingBuilding = null;
    }
}
