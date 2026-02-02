using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.Gameplay
{
	public class MissionServiceSubscriber : MonoBehaviour
	{
		[field: SerializeField]
		public Emission Emission { get; private set; }

		private void Awake()
		{
			MissionService.Instance.Register(this);
		}
	}
}