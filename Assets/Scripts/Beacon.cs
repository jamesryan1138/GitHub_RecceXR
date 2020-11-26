using System;
using Mapbox.Unity.Location;
using UnityEngine;

namespace DefaultNamespace
{
    public class Beacon: MonoBehaviour

    {
        bool _isInitialized;
        
        [NonSerialized]
        public PlayerView playerView;
        void Start()
        {
            LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
        }
        void LateUpdate()
        {
            if (_isInitialized)
            {
                var map = LocationProviderFactory.Instance.mapManager;
                transform.localPosition = map.GeoToWorldPosition(playerView.LatitudeLongitude);
            }

        }
    }
}