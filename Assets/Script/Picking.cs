using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class Picking : MonoBehaviour
{
    public SpriteRenderer myCharacter;
    public Transform tf;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        if (mousePos.x < tf.position.x)
        {
            myCharacter.flipX = true;
        }
        else
        {
            myCharacter.flipX = false;
        }
    }
}
