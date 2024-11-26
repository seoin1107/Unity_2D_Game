using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    Vector2 dragOffset = Vector2.zero;
    Rect rootSize = Rect.zero;
    public Transform myRoot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClose()
    {
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        rootSize = (myRoot as RectTransform).rect;
        dragOffset = (Vector2)myRoot.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position + dragOffset;
        pos.x = Mathf.Clamp(pos.x, rootSize.width * 0.5f, Screen.width - rootSize.width * 0.5f);
        pos.y = Mathf.Clamp(pos.y, rootSize.height * 0.5f, Screen.height - rootSize.height * 0.5f);
        myRoot.position = pos;
    }


}
