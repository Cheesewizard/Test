using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Avatar
{
	public class AvatarJumpInput : MonoBehaviour
	{
		[SerializeField]
		private Avatar avatar;

		[SerializeField]
		private InputActionReference actionReference;

		private void OnEnable()
		{
			actionReference.action.performed += HandlePerformed;
		}

		private void HandlePerformed(InputAction.CallbackContext _)
		{
			avatar.Jump();
		}

		private void OnDestroy()
		{
			actionReference.action.performed -= HandlePerformed;
		}
	}
}