using System.Collections.Generic;
using UnityEngine;

public class SupportManager : MonoBehaviour 
{
    [SerializeField] private List<GunDogMovement> _allSupports = new List<GunDogMovement>();

    public List<GunDogMovement> AllSupports { get => _allSupports; private set => _allSupports = value; }

    public void Setup()
    {
        foreach (GunDogMovement support in AllSupports)
        {
            support.Respawn();
        }
    }
}