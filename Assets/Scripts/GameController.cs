using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    public UserInfo userInfo;
    

    // Awake used for initializations before start is called SINGLETON.
    void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (gameController != null)
        {
            Destroy(gameObject);
        }
    }

}
