using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropMap3 : Drop
{
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DragMap3>().fullJigSaw.SetActive(true);
        }
        base.OnDrop(eventData);
    }
}
