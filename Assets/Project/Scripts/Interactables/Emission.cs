using System;
using System.Collections;
using UnityEngine;

public class Emission : MonoBehaviour
{
    public Action OnEmit;

    [SerializeField]
    private Transform _sphere;
    public Transform Sphere => _sphere;

    [SerializeField]
    private float duration = 3f;

    [SerializeField]
    private float targetScale = 10f;

    private void OnEnable()
    {
        OnEmit += SetScale;
    }

    private void SetScale()
    {
        StartCoroutine(ScaleSphereLoop(_sphere, targetScale, duration));

        OnEmit -= SetScale;
    }

    private IEnumerator ScaleSphereLoop(Transform target, float targetScale, float duration)
    {
        var startScale = target.localScale;
        var endScale = Vector3.one * targetScale;
        var timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            var time = Mathf.Clamp01(timer / duration);
            target.localScale = Vector3.Lerp(startScale, endScale, time);
            yield return null;
        }

        target.localScale = endScale;
    }
}