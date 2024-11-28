using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Player;

[System.Serializable]

public class MenuUI : UIManager
{
    

    public static MenuUI Instance
    {
        get;  set;
    }


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOffSaveUI()
    {
        if (SaveGameUI.Instance != null)
        {
            SaveGameUI.Instance.gameObject.SetActive(!SaveGameUI.Instance.gameObject.activeSelf);
        }
    }

}
