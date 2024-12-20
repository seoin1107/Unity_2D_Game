using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCard : MonoBehaviour, IDropHandler
{
    public DragCard myChild = null;
    public void OnDrop(PointerEventData eventData)
    {
        DragCard newChild = eventData.pointerDrag.GetComponent<DragCard>();
        if (newChild == null) return;

        Transform p = newChild.myParent;
        newChild.OnChangeParent(transform);

        if (myChild != null)
        {
            myChild.OnChangeParent(p, true);
        }
        myChild = newChild;

    }

    public void OnChangeChild(DragCard child)
    {
        myChild = child;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    protected void CheckChild() => myChild = GetComponentInChildren<DragCard>();

    // Update is called once per frame
    void Update()
    {

    }
}
