using System;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine;

namespace DefaultNamespace
{
    public class Beacon: MonoBehaviour

    {
        bool _isInitialized;
        
        [NonSerialized]
        public PlayerView playerView;

        private AbstractMap _abstractMap;
        void Start()
        {
            _abstractMap = FindObjectOfType<PlaceMapboxMap>()._map;
                _abstractMap.OnInitialized += () => _isInitialized = true;
        }
        void LateUpdate()
        {
            if (_isInitialized)
            {
                transform.localPosition = _abstractMap.GeoToWorldPosition(playerView.LatitudeLongitude);
            }

        }
    }
}