using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Skyhook.Events;

namespace Skyhook
{
    namespace Management
    {
        public class InputManager : Manager
        {
            private Vector2 _mousePosition = Vector2.zero;
            private Vector2 _mouseDelta = Vector2.zero;
            private Vector2 _movementDelta = Vector2.zero;

            private bool _primaryActionDown;
            private bool _secondaryActionDown;
            private bool _tertiaryActionDown;

            protected override void OnEnable()
            {
                base.OnEnable();

                InputEvents.OnGetMousePosition += OnGetMousePosition;
                InputEvents.OnGetMouseDelta += () => _mouseDelta;

                InputEvents.OnGetPrimaryActionDown += GetPrimaryActionDown;
                InputEvents.OnGetSecondaryActionDown += GetSecondaryActionDown;
                InputEvents.OnGetTertiaryActionDown += GetTertiaryActionDown;

                InputEvents.OnGetMouseDistanceFromCenter += OnGetMouseDistanceFromCenter;
            }

            protected override void OnDisable()
            {
                base.OnDisable();

                InputEvents.OnGetMousePosition -= OnGetMousePosition;

                InputEvents.OnGetPrimaryActionDown -= GetPrimaryActionDown;
                InputEvents.OnGetSecondaryActionDown -= GetSecondaryActionDown;
                InputEvents.OnGetTertiaryActionDown -= GetTertiaryActionDown;

                InputEvents.OnGetMouseDistanceFromCenter -= OnGetMouseDistanceFromCenter;
            }

            protected override void Awake()
            {
                base.Awake();
            }

            protected override void Update()
            {
                base.Update();
            }

            private bool GetPrimaryActionDown() => _primaryActionDown;
            private bool GetSecondaryActionDown() => _secondaryActionDown;
            private bool GetTertiaryActionDown() => _tertiaryActionDown;

            private void OnMouseDelta(InputValue value)
            {
                _mouseDelta = value.Get<Vector2>();

                InputEvents.OnMouseDelta?.Invoke((_mouseDelta / new Vector2(Screen.width, Screen.height)) * 1000.0f);
                InputEvents.OnMouseMoved?.Invoke();
            }

            private void OnMousePosition(InputValue value)
            {
                _mousePosition = value.Get<Vector2>();
                InputEvents.OnMousePosition?.Invoke(_mousePosition);
            }

            private void OnMouseScroll(InputValue value) => InputEvents.OnMouseScroll?.Invoke(value.Get<Vector2>());

            private void OnPrimaryAction(InputValue value)
            {
                _primaryActionDown = (int)value.Get<float>() == 1;
                InputEvents.OnPrimaryAction?.Invoke(_primaryActionDown);
            }

            private void OnSecondaryAction(InputValue value)
            {
                _secondaryActionDown = (int)value.Get<float>() == 1;
                InputEvents.OnSecondaryAction?.Invoke(_secondaryActionDown);
            }

            private void OnTertiaryAction(InputValue value)
            {
                _tertiaryActionDown = (int)value.Get<float>() == 1;
                InputEvents.OnTertiaryAction?.Invoke(_tertiaryActionDown);
            }

            private void OnRotate(InputValue value)
            {
                InputEvents.OnRotate?.Invoke(value.Get<float>());
            }

            private void OnMove(InputValue value)
            {
                Vector2 tValueIn = value.Get<Vector2>();
                _movementDelta = tValueIn;
                InputEvents.OnMove?.Invoke(_movementDelta);
            }

            private void OnZoom(InputValue value)
            {
                float tValueIn = value.Get<float>();
                InputEvents.OnZoom?.Invoke(tValueIn);
            }

            private Vector2 OnGetMousePosition() => _mousePosition;

            private Vector2 OnGetMouseDistanceFromCenter()
            {
                float maxDistancePercentage = 0.5f;

                Vector2 tScreenCenter = new(Screen.width / 2.0f, Screen.height / 2.0f);
                Vector2 tDifference = (InputEvents.OnGetMousePosition?.Invoke() ?? Vector2.zero) - tScreenCenter;

                Vector2 maxDistance = tScreenCenter * maxDistancePercentage;

                float normalizedX = Mathf.Clamp(tDifference.x / maxDistance.x, -1.0f, 1.0f);
                float normalizedY = Mathf.Clamp(tDifference.y / maxDistance.y, -1.0f, 1.0f);

                return new Vector2(normalizedX, normalizedY);
            }
        }
    }
}