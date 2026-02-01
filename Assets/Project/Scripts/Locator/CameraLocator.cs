using UnityEngine;

namespace Project.Scripts.Locator
{
	public class CameraLocator : MonoBehaviour
	{
		[field: SerializeField]
		public Camera SceneCamera { get; private set; }
		public static CameraLocator Instance { get; private set; }

		public void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;
		}
	}
}