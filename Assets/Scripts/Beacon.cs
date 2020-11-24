using System;
using Mapbox.Unity.Location;
using UnityEngine;

namespace DefaultNamespace
{
    public class Beacon: MonoBehaviour

    {
        [NonSerialized]
        public PlayerView playerView;
        void LateUpdate()
        {
            var map = LocationProviderFactory.Instance.mapManager;
            transform.localPosition = map.GeoToWorldPosition(playerView.LatitudeLongitude);
        }
    }
}