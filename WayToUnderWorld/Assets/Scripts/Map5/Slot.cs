using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] int idSlot;
    [SerializeField] int idItem;
    [SerializeField] int point;

    public void OnDrop(PointerEventData eventData)
    {
        if (!Gamemanager.gamemanager.onSlot)
        {
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<Roll>().id = idSlot;
                Gamemanager.gamemanager.onSlot = true;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
                if (eventData.pointerDrag.GetComponent<Roll>().idItem == idItem)
                {
                    eventData.pointerDrag.GetComponent<Roll>().idSlotTrue = true;
                    Gamemanager.gamemanager.pointPassword++;
                }
            }
        }
    }
}
