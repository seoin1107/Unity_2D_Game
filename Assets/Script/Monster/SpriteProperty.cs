using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteProperty : AnimatorProperty
{
    SpriteRenderer _render = null;
    protected SpriteRenderer myRenderer
    {
        get
        {
            if (_render == null)
            {
                _render = GetComponentInChildren<SpriteRenderer>();
            }
            return _render;
        }
    }
}
