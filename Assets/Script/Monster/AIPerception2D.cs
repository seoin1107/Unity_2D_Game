using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AIPerception2D : MonoBehaviour
{
    public LayerMask enemyMask;
    public Transform myTarget;
    public UnityEvent<Transform> findAction;
    public UnityEvent lostAtion;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & enemyMask) != 0)
        {
            //Find Enemy
            myTarget = collision.transform;
            findAction?.Invoke(myTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & enemyMask) != 0)
        {
            if (myTarget == collision.transform)
            {
                //Lost Target
                myTarget = null;
                lostAtion?.Invoke();
            }
        }
    }
}
