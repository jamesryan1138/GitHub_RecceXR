using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class SpawnObjPoint : MonoBehaviour
{
    public AbstractMap map;
    // Start is called before the first frame update
    void Start() {
        addSphere(25.7575, -80.491111); // TMIA

    }

    private GameObject addSphere(double lat, double lon) {
        Vector2d coord = new Vector2d(lat, lon);
        Vector3 pos = map.GeoToWorldPosition(coord, false);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        sphere.transform.localScale = new Vector3(.02f, .02f, .02f);
        sphere.transform.parent = this.transform;
        return sphere;
    }
}
