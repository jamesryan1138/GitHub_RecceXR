using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInfo : MonoBehaviour

{
    public List<InputField> Input = new List<InputField>();
    public string NameInput;
    public string FreqInput;
    public string CallSignInput;
    public string GroupIDInput;

    // public string MapIDInput;

    private Scene ActiveScene;

    public void GetName(string Name)
    {
        NameInput = Name;
    }

    public void GetFrequency(string Frequency)
    {
        FreqInput = Frequency;
    }

    public void GetCS(string CallSign)
    {
        CallSignInput = CallSign;
    }

    public void GetGroup(string GroupID)
    {
        GroupIDInput = GroupID;
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

        Debug.Log("Name" + NameInput + "GroupInputID" + GroupIDInput);

        DontDestroyOnLoad(this.gameObject);

    }

    private void Update()
    {
        ActiveScene = SceneManager.GetActiveScene();
        if (ActiveScene.name == "ARPlacement")
        {
        
        }
    }
}
