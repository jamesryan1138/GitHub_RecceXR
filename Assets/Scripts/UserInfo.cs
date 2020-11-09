using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInfo : MonoBehaviour

{
    public List<InputField> Input = new List<InputField>();
    public string NameInput;
    public string GroupIDInput;
    public string MapSelectInput;
    public string BeaconSelectInput;
    

    // public string MapIDInput;

    private Scene ActiveScene;

    public void GetName(string Name)
    {
        NameInput = Name;
    }

    public void GetGroup(string GroupID)
    {
        GroupIDInput = GroupID;
    }

    public void GetMap(string MapSelect)
    {
        MapSelectInput = MapSelect;
    }

    public void GetBeacon(string BeaconSelect)
    {
        BeaconSelectInput = BeaconSelect;
    }



    //public void GetMapID(string MapID)
    //{
    //     MapIDInput = MapID;
    //}


    public void SubmitInfo()
    {
        foreach (InputField Info in Input)

        {
            Info.text = "";
        }

        Debug.Log("Name" + NameInput + "GroupID" + GroupIDInput);

        DontDestroyOnLoad(this.gameObject);

    }

    private void Update()
    {
        ActiveScene = SceneManager.GetActiveScene();
        if (ActiveScene.name == "TabletopAR")
        {
        
        }
    }
}
