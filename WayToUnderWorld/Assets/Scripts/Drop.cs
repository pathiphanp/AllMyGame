    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour,IDropHandler
{
    public int id;
    public int mapNum;
    private void Update()
    {
        
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<Drag>().onSlot = true;
            Gamemanager.gamemanager.countPassword += 1;
            if (eventData.pointerDrag.GetComponent<Drag>().id == id)
            {
                Gamemanager.gamemanager.idItem = eventData.pointerDrag.GetComponent<Drag>().id;
                eventData.pointerDrag.GetComponent<Drag>().slotIdTrue = true;
                Gamemanager.gamemanager.pointPassword += 1;
            }
            else if(eventData.pointerDrag.GetComponent<Drag>().id != id)
            {
                Gamemanager.gamemanager.notMatch = true;
            }
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            if(Gamemanager.gamemanager.mapNum == 1)
            {
                AudioManager.Instance.PlaySFX("DropItemMap1");
            }
            else if(Gamemanager.gamemanager.mapNum == 2)
            {
                AudioManager.Instance.PlaySFX("DropItemMap2");
            }
        }
    }
}
