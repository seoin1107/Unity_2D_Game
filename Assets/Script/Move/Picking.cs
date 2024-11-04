using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class Picking : MonoBehaviour
{
    Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        Vector2 target = mousePos;

        if (target.x < tf.position.x)
        {
            tf.localScale = new Vector2(-1, 1);
        }
        else
        {
            tf.localScale = new Vector2(1, 1);
        }
    }
}
