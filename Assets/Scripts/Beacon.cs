using System;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Beacon: MonoBehaviour

    {
        bool _isInitialized;
        
        [NonSerialized]
        public PlayerView playerView;

        public Text UserIDText;

        private AbstractMap _abstractMap;
        void Start()
        {
            _abstractMap = FindObjectOfType<PlaceMapboxMap>()._map;
            _abstractMap.OnInitialized += () => _isInitialized = true;
            _isInitialized = _abstractMap.isInitialized;
        }

        public void SetUp(PlayerView view)
        {
            playerView = view;
            UserIDText.text = playerView.UserID;
        }
        
        void LateUpdate()
        {
            if (_isInitialized && playerView != null)
            {
                transform.localPosition = _abstractMap.GeoToWorldPosition(playerView.LatitudeLongitude);
            }

        }
    }
}