using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FreezController : MonoBehaviour
{
    public event UnityAction Freezing;
    public event UnityAction Defreezing;

    public void Freez()
    {
        Freezing?.Invoke();
    }

    public void Defreez()
    {
        Defreezing?.Invoke();
    }
}