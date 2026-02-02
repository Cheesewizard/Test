using Project.Scripts.Locator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Avatar
{
	public class RotateTowardsMouse : MonoBehaviour
	{
		[SerializeField]
		private Transform target;

		[SerializeField]
		private float rotationSpeed;

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
		}

		private void Update()
		{
			var mousePosition = Mouse.current.position.ReadValue();
			var ray = CameraLocator.Instance.SceneCamera.ScreenPointToRay(mousePosition);

			Vector3 desiredForward;

			if (IsMouseValid(mousePosition) && Physics.Raycast(ray, out var hit))
			{
				desiredForward = hit.point - target.position;
				desiredForward.y = 0f;
			}
			else
			{
				desiredForward = CameraLocator.Instance.SceneCamera.transform.forward;
				desiredForward.y = 0f;
				desiredForward.Normalize();
			}

			target.rotation = Quaternion.Slerp(
				target.rotation,
				Quaternion.LookRotation(desiredForward),
				rotationSpeed * Time.deltaTime
			);
		}

		private bool IsMouseValid(Vector2 mousePosition)
		{
			if (!Application.isFocused) return false;

			return mousePosition is {x: >= 0, y: >= 0} && mousePosition.x <= Screen.width && mousePosition.y <= Screen.height;
		}
	}
}