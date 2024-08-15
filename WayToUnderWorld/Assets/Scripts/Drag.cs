using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
{
    [SerializeField] Canvas myCanvas;
    CanvasGroup myCanvasGroup;

    [Header("All")]
    public int id;
    public int point;
    public int countpoint = 1;

    public bool onDarg;
    public bool onSlot;
    public bool nullSlot;
    public bool slotIdTrue;
    public bool restPositon;

    Vector3 startPosition;
    RectTransform recttrans;
    GameObject dragIteam;
    private void Awake()
    {
        recttrans = GetComponent<RectTransform>();
        myCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Start()
    {
        startPosition = transform.localPosition;
    }
    public void Update()
    {
        ChackReset();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Gamemanager.gamemanager.resetItemMap1 = false;
        Gamemanager.gamemanager.resetItemMap2 = false;
        restPositon = false;
        onDarg = true;
        dragIteam = null;
        myCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (eventData.pointerDrag.GetComponent<Drop>() == null && slotIdTrue == true)
        {
            Gamemanager.gamemanager.pointPassword -= 1;
            slotIdTrue = false;
        }

        if(onDarg == true && onSlot == true)
        {
            onSlot = false;
            onDarg = false;
            Gamemanager.gamemanager.countPassword -= 1;
        }
        recttrans.anchoredPosition += eventData.delta / myCanvas.scaleFactor;
    }

    public  void OnEndDrag(PointerEventData eventData)
    {
        if(onSlot == false)
        {
            NotOnSlot();
        }
        dragIteam = gameObject;
        myCanvasGroup.blocksRaycasts = true;
    }

    public virtual void ResetPositionMap1()
    {
        Gamemanager.gamemanager.pointPassword = 0;
        Gamemanager.gamemanager.countPassword = 0;
        onSlot = false;
        onDarg = false;
        slotIdTrue = false;
        transform.localPosition = startPosition;
    }

    public void ResetPositionMap2()
    {
        Gamemanager.gamemanager.countPassword--;
        onSlot = false;
        onDarg = false;
        slotIdTrue = false;
        transform.localPosition = startPosition;
    }
    public virtual void ResetPositionMap3()
    {
        Gamemanager.gamemanager.pointPassword = 0;
        Gamemanager.gamemanager.countPassword = 0;
        onSlot = false;
        onDarg = false;
        slotIdTrue = false;
    }

    public virtual void NotOnSlot()
    {
        transform.localPosition = startPosition;
    }

    void ChackReset()
    {
        //Map1
        if(Gamemanager.gamemanager.resetItemMap1 == true && restPositon == false)
        {
            restPositon = true;
            ResetPositionMap1();
        }

        //Map2
        if(Gamemanager.gamemanager.resetItemMap2 == true && restPositon == false)
        {
            restPositon = true;
            ResetPositionMap2();
        }
        if(Gamemanager.gamemanager.resetItemMap3 == true && restPositon == false)
        {
            restPositon = true;
            ResetPositionMap3();
        }
        if(Gamemanager.gamemanager.offItem == true && id == Gamemanager.gamemanager.idItem)
        {
            Destroy(gameObject);
            Gamemanager.gamemanager.offItem = false;
        }
    }


}
