using System;
using UnityEngine;

public class Emission : MonoBehaviour
{
    public Action OnEmit;
    
    [SerializeField]
    private Transform _sphere;

    private void OnEnable()
    {
        OnEmit += SetScale;
    }

    private void OnDisable()
    {
        OnEmit -= SetScale;
    }

    private void SetScale()
    {
        _sphere.localScale = Vector3.one * 10.0f;
    }
}