using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoNPC : MonoBehaviour
{
    public Image Textimage;
    public Canvas myCanvas;
        

    public void Commu()
    {
        List<string> commu = new List<string>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Textimage.gameObject.SetActive(true);
        }
    }
}