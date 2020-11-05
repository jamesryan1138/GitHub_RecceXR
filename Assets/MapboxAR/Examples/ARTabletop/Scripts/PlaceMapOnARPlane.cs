namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Unity.Map;
	using UnityARInterface;

	public class PlaceMapOnARPlane : MonoBehaviour
	{

		[SerializeField]
		private Transform _mapTransform;

		void Start()
		{
			ARPlaneHandler.returnARPlane += PlaceMap;
			ARPlaneHandler.resetARPlane += ResetPlane;
		}

		void PlaceMap(BoundedPlane plane)
		{
			/*
			 * 
			if (!_mapTransform.gameObject.activeSelf && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)

			 * 		
			 */

			if (!_mapTransform.gameObject.activeSelf)
			{
				_mapTransform.gameObject.SetActive(true);
			}

			/*
			 * 
			_mapTransform.position = map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude);

			 * 		
			 */

			_mapTransform.position = plane.center;

			// Look here for changes...
			//UpdateShaderValues(plane.center, new Vector3(1000, 1000, 1000), Quaternion.identity);
		}

		void ResetPlane()
		{
			_mapTransform.gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			ARPlaneHandler.returnARPlane -= PlaceMap;
		}

		void UpdateShaderValues(Vector3 position, Vector3 localScale, Quaternion rotation)
		{

			Shader.SetGlobalVector("_Origin", new Vector4(
				  position.x,
				  position.y,
				  position.z,
				  0f));
			Shader.SetGlobalVector("_BoxRotation", new Vector4(
				   rotation.eulerAngles.x,
				   rotation.eulerAngles.y,
				   rotation.eulerAngles.z,
				   0f));

			Shader.SetGlobalVector("_BoxSize", new Vector4(
				localScale.x,
				localScale.y,
				localScale.z,
				0f));
		}
	}
}
