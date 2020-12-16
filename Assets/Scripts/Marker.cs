using Mapbox.Unity.Map;
using Mapbox.Utils;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Marker : MonoBehaviourPun
    {
        
        bool _isInitialized;
        
        public Vector2d LatitudeLongitude;
        public string MarkerID;
        public Text MyMarkerLabel;
        private AbstractMap _abstractMap;
    
//Create RPC function [PunRPC] - Data type has to be serialized
        [PunRPC]
        void RPC_SetAvatar(double latitude, double longitude, string markerID)
        {
            LatitudeLongitude = new Vector2d(latitude, longitude);
            //Instantiates the non-local players selected character/beacon to local player client
            MarkerID = MyMarkerLabel.text;
            MarkerID = markerID;
            Debug.Log("Marker Created!!");
        }
    
        //
        public void Start()
        {
            _abstractMap = FindObjectOfType<PlaceMapboxMap>()._map;
            _abstractMap.OnInitialized += () => _isInitialized = true;
            _isInitialized = _abstractMap.isInitialized;
        }

        public void SetUp(string markerID, Vector2d latlon)
        {
            if (photonView.IsMine)
            {
                photonView.RPC(nameof(RPC_SetAvatar),
                    RpcTarget.AllBuffered,
                    latlon.x, latlon.y, markerID);
            }
        }
        
        void LateUpdate()
        {
            if (_isInitialized)
            {
                transform.localPosition = _abstractMap.GeoToWorldPosition(LatitudeLongitude);
            }

        }
    }
}