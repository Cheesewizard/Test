using UnityEngine;
using Skyhook.Events;

public class Avatar : MonoBehaviour
{
    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private Transform _offset;

    [SerializeField]
    private float _offsetAmplitude = 0.1f, _offsetSpeed = 2.0f;
    
    private Vector3 _velocityTarget;

    [SerializeField]
    private float _movementMultiplier = 5.0f;
    
    [SerializeField]
    private float _turnSpeed = 10.0f;

    [SerializeField]
    private float _deadZone = 0.01f;
    
    private void OnEnable()
    {
        InputEvents.OnMove += SetVelocity;
    }

    private void OnDisable()
    {
        InputEvents.OnMove -= SetVelocity;
    }

    private void Update()
    {
        _offset.localPosition = new Vector3(_offset.localPosition.x, 0.0f + Mathf.Sin(Time.time * _offsetSpeed) * _offsetAmplitude, _offset.localPosition.z);
        
        Vector3 adjustedVelocity = _velocityTarget * _movementMultiplier;

        if (!_characterController.isGrounded)
            adjustedVelocity += Physics.gravity;
        
        _characterController.Move(adjustedVelocity * Time.deltaTime);
        
        Vector3 v = adjustedVelocity;
        v.y = 0.0f;

        if (v.sqrMagnitude < _deadZone * _deadZone)
            return;

        Quaternion target = Quaternion.LookRotation(v.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, _turnSpeed * Time.deltaTime);
    }

    private void SetVelocity(Vector2 velocity)
    {
        if (!_characterController)
            return;
        
        _velocityTarget = new Vector3(velocity.x, 0, velocity.y);
    }
}