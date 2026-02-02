using Skyhook.Events;
using UnityEngine;

namespace Project.Scripts.Avatar
{
    public class Avatar : MonoBehaviour
    {
        private const int MAX_JUMPS = 2;

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

        [SerializeField]
        private CharacterController characterController;

        [SerializeField]
        private float jumpSpeed = 6f;

        private int jumpsRemaining;
        private float verticalVelocity;

        private void Start()
        {
            jumpsRemaining = MAX_JUMPS;
        }

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
            _offset.localPosition = new Vector3(_offset.localPosition.x,
                Mathf.Sin(Time.time * _offsetSpeed) * _offsetAmplitude, _offset.localPosition.z);

            var adjustedVelocity = _velocityTarget * _movementMultiplier;

            if (_characterController.isGrounded)
            {
                jumpsRemaining = MAX_JUMPS;
            }
            else
            {
                adjustedVelocity += Physics.gravity;
            }

            // Jump
           // adjustedVelocity.y = verticalVelocity;
            //verticalVelocity -= adjustedVelocity.y * Time.deltaTime;

            _characterController.Move(adjustedVelocity * Time.deltaTime);

            var v = adjustedVelocity;
            v.y = 0.0f;

            if (v.sqrMagnitude < _deadZone * _deadZone)
            {
                return;
            }

            var target = Quaternion.LookRotation(v.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, _turnSpeed * Time.deltaTime);
        }

        private void SetVelocity(Vector2 velocity)
        {
            if (!_characterController)
                return;

            _velocityTarget = new Vector3(velocity.x, 0, velocity.y);
        }

        public void Jump()
        {
            if (jumpsRemaining <= 0) return;

            verticalVelocity = jumpSpeed;
            jumpsRemaining--;
        }

    }
}