using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;


public class UIManager : MonoBehaviour
{
    public CharacterStatus player;
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
            OnOffStatusUI();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOffMenuUI();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOffInvenUI();
        }
    }
    public void OnOffStatusUI()
    {
        if (StatusUI.Instance != null)
        {
            StatusUI.Instance.gameObject.SetActive(!StatusUI.Instance.gameObject.activeSelf);
            myStatusUI.UpdateStatusUI();
        }
        
    }

    public void OnOffMenuUI()
    {
        if (MenuUI.Instance != null)
        {
            MenuUI.Instance.gameObject.SetActive(!MenuUI.Instance.gameObject.activeSelf);
        }
    }

    public void OnOffInvenUI()
    {
        if (CardInventory.Instance != null)
        {
            CardInventory.Instance.gameObject.SetActive(!CardInventory.Instance.gameObject.activeSelf);
        }
    }

}
