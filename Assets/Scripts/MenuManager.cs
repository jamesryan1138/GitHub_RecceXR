using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public List<GameObject> MenuItems = new List<GameObject>();

    public void LaunchShowMenu()
    {
        foreach (GameObject Button in MenuItems)
        {
            Button.SetActive(true);
        }
    }
    public void LaunchTabletopAR()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2, LoadSceneMode.Single);
    }
    public void LaunchUserInfo()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1, LoadSceneMode.Single);
    }
    public void LaunchMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0, LoadSceneMode.Single);
    }

}
