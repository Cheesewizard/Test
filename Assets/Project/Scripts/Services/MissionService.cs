using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Gameplay;
using UnityEngine;

namespace Project.Scripts.Services
{
	public class MissionService
	{
		private static MissionService instance;
		public static MissionService Instance => instance ??= new MissionService();

		private List<MissionServiceSubscriber> emitterEntries =  new();

		public event Action OnGameComplete;
		public event Action<int> OnObjectiveUpdated;
		public void Register(MissionServiceSubscriber subscriber)
		{
			Debug.Log($"[MissionService] Registering {subscriber}");

			subscriber.Emission.OnEmit += () => HandleOnEmitTriggered(subscriber);
			emitterEntries.Add(subscriber);
		}

		private void HandleOnEmitTriggered(MissionServiceSubscriber subscriber)
		{
			Debug.Log($"[MissionService] Removing {subscriber}");

			emitterEntries.Remove(subscriber);
			CheckIfWon(emitterEntries);
		}

		private void CheckIfWon(IEnumerable<MissionServiceSubscriber> subscribers)
		{
			var count = subscribers.Count();
			if (count <= 0f)
			{
				OnGameComplete?.Invoke();
			}
			else
			{
				OnObjectiveUpdated?.Invoke(count);
			}
		}

	}
}