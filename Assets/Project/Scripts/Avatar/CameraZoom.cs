using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Avatar
{
	public class CameraZoom : MonoBehaviour
	{
		[SerializeField]
		private CinemachineFollowZoom followZoom;

		[SerializeField]
		private float zoomSpeed = 10f;

		[SerializeField]
		private float minFov = 50f;

		[SerializeField]
		private float maxFov = 110f;

		private Vector2 targetFov;

		private void Start()
		{
			targetFov = followZoom.FovRange;
		}

		private void Update()
		{
			var scrollDelta = Mouse.current.scroll.ReadValue();
			var zoomDelta = scrollDelta.y;

			if (Mathf.Abs(zoomDelta) > 0.01f)
			{
				var fovRange = followZoom.FovRange;

				fovRange.x -= zoomDelta * zoomSpeed;
				fovRange.y -= zoomDelta * zoomSpeed;

				fovRange.x = Mathf.Clamp(fovRange.x, minFov, maxFov);
				fovRange.y = Mathf.Clamp(fovRange.y, minFov, maxFov);

				targetFov = fovRange;
			}

			followZoom.FovRange = Vector2.Lerp(followZoom.FovRange, targetFov, Time.deltaTime * zoomSpeed);
		}
	}
}