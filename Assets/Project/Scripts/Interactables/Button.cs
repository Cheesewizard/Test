using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeReference]
    private Emission _emission;

    private void OnTriggerEnter(Collider other)
    {
        _emission.OnEmit?.Invoke();
    }
}
