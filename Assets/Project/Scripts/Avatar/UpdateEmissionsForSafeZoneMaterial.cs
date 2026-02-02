using UnityEngine;

namespace Project.Scripts.Avatar
{
	public class UpdateEmissionsForSafeZoneMaterial : MonoBehaviour
	{
		[SerializeField]
		private Material material;

		[SerializeField]
		private Emission[] emissions;

		private void Update()
		{
			for (var i = 0; i < emissions.Length; i++)
			{
				var spherePosition = new Vector4(
					emissions[i].transform.position.x,
					emissions[i].transform.position.y,
					emissions[i].transform.position.z,
					emissions[i].Sphere.localScale.x * 0.5f
				);

				material.SetVector($"_EmitterPos{i + 1}", spherePosition);
			}
		}

		private void OnDisable()
		{
			for (var i = 0; i < emissions.Length; i++)
			{
				var spherePosition = new Vector4(
					emissions[i].transform.position.x,
					emissions[i].transform.position.y,
					emissions[i].transform.position.z,
					0f
				);

				material.SetVector($"_EmitterPos{i + 1}", spherePosition);
			}
		}
	}
}