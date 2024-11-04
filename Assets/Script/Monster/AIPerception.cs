using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerception : MonoBehaviour
{
    public LayerMask mask;
    public GameObject target;
    public UnityEvent<GameObject> findAction;
    public UnityEvent lostAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((1 << other.gameObject.layer & mask) != 0)
        {
            target = other.gameObject;
            findAction?.Invoke(target);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (target == other.gameObject)
        {
            target = null;
            lostAction?.Invoke(); // ?=널값이 아니라면, invoke 실행

        }
    }
}
