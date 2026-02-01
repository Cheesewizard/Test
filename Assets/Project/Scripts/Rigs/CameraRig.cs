using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField]
    private Transform _trackingTarget;

    [SerializeField]
    private float _trackingSpeed = 5.0f;

    private void LateUpdate()
    {
        if (!_trackingTarget)
            return;

        transform.position = Vector3.Lerp(transform.position, _trackingTarget.position, _trackingSpeed * Time.deltaTime);
    }
}
