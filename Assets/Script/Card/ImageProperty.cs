using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageProperty : MonoBehaviour
{

    Image _img = null;
    public Image myImage
    {
        get
        {
            if (_img == null)
            {
                Image[] list = GetComponentsInChildren<Image>();
                if (list != null)
                {
                    if (list.Length == 1)
                    {
                        _img = list[0];
                    }
                    else
                    {
                        _img = list[list.Length - 1];
                    }
                }
            }
            return _img;
        }
    }


}
