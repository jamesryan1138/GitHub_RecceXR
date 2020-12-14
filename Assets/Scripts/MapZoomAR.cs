using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class MapZoomAR : MonoBehaviour
{
    public AbstractMap _mapManager;

    public void IncreaseZoom()
    {
        SetZoom(_mapManager.Zoom + 1);
        Debug.Log("Increase Zoom");
    }

    public void DecreaseZoom()
    {
        SetZoom(_mapManager.Zoom - 1);
        Debug.Log("Decrease Zoom");
    }

    public void UpdateMap()
    {
        //
    }
    public void SetZoom(float NewZoom)
    {
        var zoom = Mathf.Max(0.0f, Mathf.Min(NewZoom, 21));

        _mapManager.UpdateMap(_mapManager.CenterLatitudeLongitude, zoom);
      
    }

}
