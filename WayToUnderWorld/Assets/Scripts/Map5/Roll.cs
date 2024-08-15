using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Roll : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] Canvas canvasMap5;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    Vector3 startRe;
    public int id;
    public int idItem;

    Camera myCam;
    public float z;
    bool onSlot;
    public bool idSlotTrue;
    [SerializeField] bool imageTrue;

    [SerializeField] float min;//0.02
    [SerializeField] float max;//-0.02


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        startRe = transform.localPosition;
        z = Random.Range(1, -1f);
        transform.rotation = new Quaternion(0, 0, z, transform.rotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        roll();
    }
    void roll()
    {
        if (Gamemanager.gamemanager.candrag)
        {
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.blocksRaycasts = true;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        Gamemanager.gamemanager.candrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<Slot>() == null && idSlotTrue == true)
        {
            Gamemanager.gamemanager.pointPassword--;
            idSlotTrue = false;
        }

        if (Input.GetMouseButton(0))
        {
            rectTransform.anchoredPosition += eventData.delta / canvasMap5.scaleFactor;
        }
        if (Input.GetMouseButton(1))
        {
            if (!imageTrue)
            {
                imageTrue = true;
                Gamemanager.gamemanager.pointImageTrue--;
            }
            Vector3 mousePos = Input.mousePosition;
            Vector2 direction = new Vector2(mousePos.x - transform.position.x,
                                            mousePos.y - transform.position.y);
            transform.up = direction;
            Gamemanager.gamemanager.onSlot = true;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.rotation.z <= max && transform.rotation.z >= min && imageTrue)
        {
            imageTrue = false;
            Gamemanager.gamemanager.pointImageTrue++;
        }
        if (!Gamemanager.gamemanager.onSlot)
        {
            id = 0;
            rectTransform.sizeDelta = new Vector2(290, 290);
            transform.localPosition = startRe;
        }
        else
        {
            Gamemanager.gamemanager.onSlot = false;
            if (id == 1)//224
            {
                rectTransform.sizeDelta = new Vector2(224, 224);
            }
            if (id == 2)//440
            {
                rectTransform.sizeDelta = new Vector2(440, 440);
            }
            if (id == 3)//655
            {
                rectTransform.sizeDelta = new Vector2(655, 655);
            }
            if (id == 4)//865
            {
                rectTransform.sizeDelta = new Vector2(865, 865);
            }
            if (id == 5)//1080
            {
                rectTransform.sizeDelta = new Vector2(1080, 1080);
            }
        }
        canvasGroup.alpha = 1f;
        Gamemanager.gamemanager.candrag = false;
        canvasGroup.blocksRaycasts = true;
    }
}
