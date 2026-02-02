using UnityEngine;

namespace Project.Scripts.Avatar
{
	public class UpdateEmissionsForLocalSafeZoneMaterial : MonoBehaviour
	{
		private static readonly int emitterPos1 = Shader.PropertyToID("_EmitterPos1");

		[SerializeField]
		private Material material;

		[SerializeField]
		private Emission emission;

		private void Update()
		{
			var spherePosition = new Vector4(emission.transform.position.x, emission.transform.position.y,
				emission.transform.position.z, emission.Sphere.localScale.x * 0.5f);

			material.SetVector(emitterPos1, spherePosition);
		}
	}
}