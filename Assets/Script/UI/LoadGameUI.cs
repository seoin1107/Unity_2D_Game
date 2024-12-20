using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class LoadGameUI : StartUI
{

    public static LoadGameUI Instance;
    public Button loadButton1;
    public Button loadButton2;
    public Button loadButton3;
    public CharacterStatus characterStatus;

  
    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists(filePath+"Save1"))
        {
            
        }
        Instance = this;
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnLoadSave1()
    {
        mySave.characterStat = FileManager.LoadFromJson<Stat>(filePath+"Save1.dat");
    }
    public void OnLoadSave2()
    {
        mySave.characterStat = FileManager.LoadFromJson<Stat>(filePath+"Save2.dat");
    }
    public void OnLoadSave3()
    {
        mySave.characterStat = FileManager.LoadFromJson<Stat>(filePath+"Save3.dat");
    }


}
