using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInfo : MonoBehaviour

{
    
    // Added from Photon to pick Select User Beacon
   
    // list of Character options
    

    public List<InputField> Input = new List<InputField>();
    public InputField GroupIDInput;
    public InputField NameInput;
    
    /*
    public static UserInfo PI;
    public int MySelectedCharacter;
    
    public GameObject[] allCharacters;
    */
        
    // public string MapIDInput;

    private Scene ActiveScene;

    
    public string GetName()
    {
        return NameInput.text;
    }

    public string GetGroupID()
    {
        return GroupIDInput.text;
    }
        
    public void SubmitInfo()
    {
        foreach (InputField Info in Input)

        {
            Info.text = "";
        }

        Debug.Log("Name: " + NameInput.text + " // " + "GroupID: " + GroupIDInput.text);

        DontDestroyOnLoad(this.gameObject);

    }
  
    /* Start function from Photon Project
    void Start()
    {
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            MySelectedCharacter = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            MySelectedCharacter = 0;
            PlayerPrefs.SetInt("MyCharacter", MySelectedCharacter);

        }
    }
    */
    
    private void Update()
    {
        ActiveScene = SceneManager.GetActiveScene();
        if (ActiveScene.name == "TabletopAR")
        {
        
        }
    }
}
