using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Save
{
    public CharacterStatus myCharacter;





}
public class MenuUI : MonoBehaviour
{

    public Save mySave;
    public static MenuUI Instance
    {
        get; set;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
        mySave = FileManager.LoadFromJson<Save>(Application.dataPath+"/Data/Save/PlayerSave.dat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        FileManager.SaveToJson<Save>(Application.dataPath+"/Data/Save/PlayerSave.dat", mySave);
    }
}
