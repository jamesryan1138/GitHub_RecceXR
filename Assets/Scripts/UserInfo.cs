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
    public int FieldLength = 10;

    public GameObject arMapButton;
    public GameObject transmitButton;
    
    private string UserID;
    private string GroupID;

    // Scene & Group Functions
    
    private Scene ActiveScene;
    
    public string GetName()
    {
        
        return UserID;
    }

    public string GetGroupID()
    {
        return GroupID;
    }
        
    public void SubmitInfo()
    {
        UserID = NameInput.text;
        GroupID = GroupIDInput.text;
        
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

        NameInput.characterLimit = FieldLength;
        GroupIDInput.characterLimit = FieldLength;
        CheckInputs();
        
    }

    private void CheckInputs()
    {
        if( string.IsNullOrEmpty( NameInput.text ))
        {
            arMapButton.SetActive( false );
            transmitButton.SetActive(false);
        }
        else
            arMapButton.SetActive( true );
        transmitButton.SetActive(true);
    }
    
    
    public void SetSelectedAvatar(int selectedAvatar)
    {
        MySelectedAvatar = selectedAvatar;
    }
    
}
