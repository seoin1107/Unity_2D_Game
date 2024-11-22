using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class MenuUI : CharacterStatus
{

    public CharacterStatus mySave;
    public static MenuUI Instance
    {
        get;  set;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
        //mySave = FileManager.LoadFromJson<CharacterStatus>(Application.dataPath+"/Data/Save/PlayerSave.dat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        FileManager.SaveToJson<CharacterStatus>(Application.dataPath+"/Data/Save/PlayerSave.dat", mySave);
    }
}
