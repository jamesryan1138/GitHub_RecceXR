using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInfo : MonoBehaviour

{
    public InputField GroupIDInput;
    public InputField NameInput;
    
    public List<InputField> Input = new List<InputField>();
   public string MapSelectInput;
    public string BeaconSelectInput;
    

    // public string MapIDInput;

    private Scene ActiveScene;

    /*
     * public string GetName()
   
    {
        return NameInput.text;
    }
*/
    public string GetGroupID()
    {
        return GroupIDInput.text;
    }

    public void GetMap(string MapSelect)
    {
        MapSelectInput = MapSelect;
    }

    public void GetBeacon(string BeaconSelect)
    {
        BeaconSelectInput = BeaconSelect;
    }


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
