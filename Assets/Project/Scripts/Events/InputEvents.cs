using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyhook
{
    namespace Events
    {
        public abstract class InputEvents
        {
            public static Action<Vector2> OnMouseDelta;
            public static Action OnMouseMoved;
            public static Action<Vector2> OnMousePosition;
            public static Action<Vector2> OnMouseScroll;

            public static Action<bool> OnPrimaryAction;
            public static Action<bool> OnSecondaryAction;
            public static Action<bool> OnTertiaryAction;

            public static Action<Vector2> OnMove;
            public static Func<Vector2> OnGetMousePosition;
            public static Func<Vector2> OnGetMouseDelta;
            public static Func<Vector2> OnGetMouseDistanceFromCenter;

            public static Func<bool> OnGetPrimaryActionDown;
            public static Func<bool> OnGetSecondaryActionDown;
            public static Func<bool> OnGetTertiaryActionDown;

            public static Action<float> OnRotate;
            public static Action<float> OnZoom;
        }
    }
}