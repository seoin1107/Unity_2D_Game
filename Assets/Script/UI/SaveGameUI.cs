using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class SaveGameUI : UIManager
{
   public string filePath = Application.dataPath + "/Data/Save/"; //세이브 파일 경로

    public static SaveGameUI Instance;
    public Button saveButton1;
    public Button saveButton2;
    public Button saveButton3;


    // Start is called before the first frame update
    void Start()
    {    
        SaveGameUI.Instance = this;
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnSave1()
    {
        FileManager.SaveToJson<Stat>(filePath+"Save1.dat", player.characterStat);
    }
    public void OnSave2()
    {
        FileManager.SaveToJson<Stat>(filePath+"Save2.dat", player.characterStat);
    }
    public void OnSave3()
    {
        FileManager.SaveToJson<Stat>(filePath+"Save3.dat", player.characterStat);
    }

 


}
