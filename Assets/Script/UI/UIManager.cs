using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;


public class UIManager : MonoBehaviour
{
    public StatusUI myStatusUI;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StatusUI.Instance.gameObject.SetActive(!StatusUI.Instance.gameObject.activeSelf);
            myStatusUI.UpdateStatusUI();
        }
    }
}