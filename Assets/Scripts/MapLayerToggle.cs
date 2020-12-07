using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using UnityEngine;

public class MapLayerToggle : MonoBehaviour
{
    public AbstractMap _map;

    public void SetMapLayer(string MapURL)
    {
        _map.ImageLayer.SetLayerSource(MapURL);
    }
}
