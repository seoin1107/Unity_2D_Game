using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class Picking : MonoBehaviour
{
    public SpriteRenderer myCharacter;
    public Transform tf;
    public Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//마우스 위치

        if (mousePos.x < tf.position.x)     //오른쪽 보기
        {
            myCharacter.flipX = true;
        }
        else           //왼쪽 보기
        {
            myCharacter.flipX = false;
        }


    }



}
