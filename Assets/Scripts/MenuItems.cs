using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItems : MonoBehaviour
{
    public static MenuItems instance;

    private void Awake()
    {


        // if there is an existing instance of Menu, then don't create a new one
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(this.gameObject);


    }

}
