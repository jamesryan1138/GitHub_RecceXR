using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ActiveTransmit : MonoBehaviour
{
    public GameController gameController;
    public float latitude;
    public float longitude;

    // public float altitude;
    public string Coordinates;
    // reference DisplayGridLocation
    public Text DisplayGrid;

    public Text displayMyName;
    public Text displayMyGroup;
    
    //public string myUserName;
    //public string myGroupID;

    public void Awake()
    {
        GameController gameControllers = GameObject.FindObjectOfType <GameController>();
        //PlayerView[] playerViews = GameObject.FindObjectsOfType <PlayerView>();
        displayMyName.text = gameControllers.userInfo.GetName();
        displayMyGroup.text = gameControllers.userInfo.GetGroupID();
    }

    // Update is called once per frame
    void Update()
    {
        
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        Coordinates = latitude.ToString() + "  ,  " + longitude.ToString();
        Debug.Log(latitude.ToString() + "  ,  " + longitude.ToString());
        DisplayGrid.text = Coordinates;

    }

    private IEnumerator GetLocation()
    {
        Input.location.Start();
        while(Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);
        }
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        yield break;

    }
}
