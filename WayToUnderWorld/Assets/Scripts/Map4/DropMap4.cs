using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropMap4 : MonoBehaviour, IDropHandler
{
    public int id;
    [SerializeField] GameObject checkTrue;
    [SerializeField] GameObject checkFalse;
    private void Update()
    {
        ChackId();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            AudioManager.Instance.PlaySFX("DropItemToPot");
            eventData.pointerDrag.GetComponent<Drag>().onSlot = true;
            Gamemanager.gamemanager.countPassword += 1;
            if (eventData.pointerDrag.GetComponent<Drag>().id == id)
            {
                StartCoroutine(ShowCheck(checkTrue));
                Gamemanager.gamemanager.idItem = eventData.pointerDrag.GetComponent<Drag>().id;
                eventData.pointerDrag.GetComponent<Drag>().slotIdTrue = true;
                Gamemanager.gamemanager.wightItem += eventData.pointerDrag.GetComponent<DragMap4>().wigth;
                Gamemanager.gamemanager.pointPassword += 1;
            }
            else
            {
                StartCoroutine(ShowCheck(checkFalse));
                AudioManager.Instance.PlaySFX("FailPot");
                Gamemanager.gamemanager.resetFormola = true;
            }
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<Drag>().onSlot = false;
        }

    }
    IEnumerator ShowCheck(GameObject checkObj)
    {
        checkObj.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        checkObj.SetActive(false);
    }
    void ChackId()
    {
        id = Gamemanager.gamemanager.idItem;
    }
}
