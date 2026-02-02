using Project.Scripts.UI;
using UnityEngine;

namespace Project.Scripts.Locator
{
	public class UiLocator : MonoBehaviour
	{
		[field: SerializeField]
		public TextPopupUiEntry uiEntry { get; private set; }

		public static UiLocator Instance { get; private set; }

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