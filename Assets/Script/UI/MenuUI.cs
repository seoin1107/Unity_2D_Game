using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Save
{
    public Stat myStat;





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
        mySave = FileManager.LoadFormBinary<Save>(Application.dataPath+"/Data/Save/PlayerSave.dat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        FileManager.SaveToBinary<Save>(Application.dataPath+"/Data/Save/PlayerSave.dat", mySave);
    }
}
