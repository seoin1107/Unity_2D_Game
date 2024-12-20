using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCard : ImageProperty, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 dragOffset = Vector2.zero;
    public Transform myParent
    {
        get; private set;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)transform.position - eventData.position;
        myParent = transform.parent;
        transform.SetParent(transform.parent.parent);
        myImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(myParent);
        myImage.raycastTarget = true;
        transform.localPosition = Vector3.zero;
    }

    public void OnChangeParent(Transform p, bool change = false)
    {

        if (change)
        {
            transform.SetParent(p);
            transform.localPosition = Vector3.zero;
            p.GetComponent<DropCard>().OnChangeChild(this);
        }
        else
        {
            myParent.GetComponent<DropCard>()?.OnChangeChild(null);
        }
        myParent = p;

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
