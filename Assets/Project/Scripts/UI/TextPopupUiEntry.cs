using Project.Scripts.Services;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI
{
	public class TextPopupUiEntry : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI text;

		[SerializeField]
		private CanvasGroup canvasGroup;

		private bool isFading;
		private float targetAlpha;
		private float fadeSpeed = 10f;

		private void Start()
		{
			MissionService.Instance.OnGameComplete += HandleGameCompleted;
			MissionService.Instance.OnObjectiveUpdated += HandleObjectiveUpdated;

			canvasGroup.alpha = 0f;
		}

		private void HandleObjectiveUpdated(int remainingCount)
		{
			text.text = $"{remainingCount.ToString()} objectives remaining";
			PLay();
		}

		private void HandleGameCompleted()
		{
			text.text = "All objectives completed. You have saved everyone but your princess lives in another castle...";
		}

		private void Update()
		{
			if (!isFading) return;

			canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
		}

		private void PLay()
		{
			targetAlpha = 1f;
			isFading = true;
		}

		public void Stop()
		{
			targetAlpha = 0f;
			isFading = true;
		}

		private void OnDestroy()
		{
			MissionService.Instance.OnGameComplete -= HandleGameCompleted;
			MissionService.Instance.OnObjectiveUpdated -= HandleObjectiveUpdated;
		}
	}
}