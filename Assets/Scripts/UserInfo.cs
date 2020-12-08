using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInfo : MonoBehaviour

{
    
    // Added from Photon to pick Select User Beacon
    public static UserInfo instance;
    public int MySelectedAvatar;
    // list of Character options
    public GameObject[] allCharacters;
    
    public List<InputField> Input = new List<InputField>();
    public InputField GroupIDInput;
    public InputField NameInput;
    

    // Scene & Group Functions
    
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
  
    // OnEnable from Photon Project
    private void OnEnable()
    {
        if(UserInfo.instance == null)
        {
            UserInfo.instance = this;
        }
        else
        {
            if(UserInfo.instance != this)
            {
                Destroy(UserInfo.instance.gameObject);
                UserInfo.instance = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MyAvatar"))
        {
            MySelectedAvatar = PlayerPrefs.GetInt("MyAvatar");
        }
        else
        {
            MySelectedAvatar = 0;
            PlayerPrefs.SetInt("MyAvatar", MySelectedAvatar);

        }
    }

    public void SetSelectedAvatar(int selectedAvatar)
    {
        MySelectedAvatar = selectedAvatar;
    }
    
}
