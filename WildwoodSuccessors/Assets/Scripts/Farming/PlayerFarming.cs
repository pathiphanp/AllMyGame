using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static PlayerFarmingInputAction;

public class PlayerFarming : MonoBehaviour, IFarmingActions
{
    public Camera mainCamera;
    //Compinent
    [Header("Inventory")]
    public InventoryUI inventoryUI;
    public GameObject pauseMenu;
    PlayerFarmingInputAction playerFarmingInput;

    public PlantsType plantsType;

    [Header("Toolbar")]
    public Toolbar_UI toolbarUI;
    public int no;
    public bool dragSingle;

    [Header("Manager")]
    public InventoryManager inventoryManager;
    public UIManager uiManager;

    [Header("Fishing")]
    public GameObject fishingMinigame;
    public float minsCountTimeFish;
    public float maxCountTimeFish;
    public bool isFishsing;
    public bool isCatchFish;
    internal bool isFishingAgain; // Make for animation in bait
    public GameObject bait;

    [Header("Farming")]
    public GameObject roofFarm;



    private void OnEnable()
    {
        playerFarmingInput.Farming.Enable();
    }

    void Disable()
    {

    }

    void Awake()
    {
        playerFarmingInput = new PlayerFarmingInputAction();
        playerFarmingInput.Farming.SetCallbacks(this);

        //Fishing
        bait.SetActive(false);
        isFishsing = false;
        isCatchFish = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameManagerGameplay.Instance.inputInFarming = this;
    }

    // Update is called once per frame
    void Update()
    {
        FishingMiniGameActive();
    }

    #region Fishing Count กดตกปลาแล้วรอ
    public void FishingMiniGameActive()
    {
        if (isFishsing)
        {
            isFishsing = false;
            StartCoroutine(FishingCountDown());
        }
    }

    IEnumerator FishingCountDown()
    {
        float rnd = UnityEngine.Random.Range(minsCountTimeFish, maxCountTimeFish);
        yield return new WaitForSeconds(rnd);
        StartCoroutine(CatchFishCountDown());

    }

    IEnumerator CatchFishCountDown()
    {
        AudioManager.Instance.PlaySFX("FishBiteBait");
        isFishingAgain = false; // Make for animation in bait
        isCatchFish = true;
        yield return new WaitForSeconds(2f);
        if (isCatchFish)
        {
            isCatchFish = false;
            StartCoroutine(FishingCountDown());
            isFishingAgain = true;
        }
        else
        {

        }
    }

    #endregion
    public void CheckItemInSlot(int i)
    {
        if (toolbarUI.toolbarSlots[i].itemTpye == ItemType.Seed)
        {
            plantsType = toolbarUI.toolbarSlots[i].plantsType;
        }

        if (toolbarUI.toolbarSlots[i].itemTpye != ItemType.Seed)
        {
            plantsType = null;
        }
    }

    #region Input action
    //Control input action
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            //Check Ray when hit collider
            if (hit.collider != null)
            {
                #region Planted area

                //Ground to plant
                if (hit.collider.tag == "PlantArea")
                {
                    PlantedArea _plantedArea = hit.collider.gameObject.GetComponent<PlantedArea>();

                    if (_plantedArea.isReady == false)
                    {
                        _plantedArea.harvest();

                        AudioManager.Instance.PlaySFX("Hoe");
                    }
                }

                #endregion

                #region Plants interact
                //Click plants
                if (hit.collider.tag == "Plants")
                {
                    PlantFarming _plantsFarming = hit.collider.gameObject.GetComponent<PlantFarming>();
                    Debug.Log("Plants");

                    //Check object have plants
                    if (!_plantsFarming.havePlant && plantsType != null)
                    {
                        _plantsFarming.Planting();
                        inventoryManager.toolbar.Remove(no, 1);
                        uiManager.RefreshAll();
                        CheckItemInSlot(no);

                        AudioManager.Instance.PlaySFX("PlantSeed");
                    }

                    //Watering plants
                    if (_plantsFarming.wantWater)
                    {
                        _plantsFarming.Watering();

                        AudioManager.Instance.PlaySFX("Watering");

                    }

                    //Harvest plants
                    if (_plantsFarming.complete)
                    {
                        _plantsFarming.Harvesting();

                        AudioManager.Instance.PlaySFX("Harvest");
                    }

                    if (plantsType == null)
                    {
                        Debug.Log("No Seed");
                    }
                }
                #endregion

                #region Item interact
                //Click Item
                if (hit.collider.tag == "Item")
                {
                    Item _item = hit.collider.gameObject.GetComponent<Item>();

                    //Debug.Log(_item);
                    _item.GetItem();
                }
                #endregion
            }
            else
            {
                Debug.Log("Noting");
            }
        }
    }

    public void OnSlot01(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            no = 0;
            toolbarUI.SelectSlot(0);
            CheckItemInSlot(0);

            //toolbarUI.
        }
    }

    public void OnSlot02(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            no = 1;
            toolbarUI.SelectSlot(1);
            CheckItemInSlot(1);
        }
    }

    public void OnSlot03(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            no = 2;
            toolbarUI.SelectSlot(2);
            CheckItemInSlot(2);
        }
    }

    public void OnSlot04(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            no = 3;
            toolbarUI.SelectSlot(3);
            CheckItemInSlot(3);
        }
    }

    public void OnSlot05(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            no = 4;
            toolbarUI.SelectSlot(4);
            CheckItemInSlot(4);
        }
    }

    public void OnSlot06(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            no = 5;
            toolbarUI.SelectSlot(5);
            CheckItemInSlot(5);
        }
    }

    public void OnSlot07(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            no = 6;
            toolbarUI.SelectSlot(6);
            CheckItemInSlot(6);
        }
    }

    public void OnSlot08(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            no = 7;
            toolbarUI.SelectSlot(7);
            CheckItemInSlot(7);
        }
    }

    public void OnSlot09(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            no = 8;
            toolbarUI.SelectSlot(8);
            CheckItemInSlot(8);
        }
    }

    public void OnTab(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inventoryUI.inventoryUI != null)
            {
                if (GameManagerPor.Instance.stateCamera == StateInGame.FarmingArea)
                {
                    if (inventoryUI.inventoryUI.activeSelf == false)
                    {
                        roofFarm.SetActive(true);
                        inventoryUI.inventoryUI.SetActive(true);
                        GameManagerPor.Instance.uiManager.RefreshInventoryUI("Backpack");
                    }
                    else
                    {
                        roofFarm.SetActive(false);
                        inventoryUI.inventoryUI.SetActive(false);
                    }
                }
            }

            if (inventoryUI.inventoryUI != null)
            {
                if (GameManagerPor.Instance.stateCamera == StateInGame.WorldArea)
                {
                    if (pauseMenu.activeSelf == false)
                    {
                        pauseMenu.SetActive(true);
                        GameManagerPor.Instance.uiManager.RefreshInventoryUI("Backpack");
                    }
                    else
                    {
                        pauseMenu.SetActive(false);
                    }
                }
            }

        }
    }

    public void OnLeftShift(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dragSingle = true;
        }

        if (context.canceled)
        {
            dragSingle = false;
        }
    }

    public void OnSpacebar(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GameManagerPor.Instance.stateCamera == StateInGame.FishingArea && !isFishsing && !isCatchFish)
            {
                Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
                isFishsing = true;

                bait.SetActive(true);
                bait.transform.position = mousePos;
            }

            if (GameManagerPor.Instance.stateCamera == StateInGame.FishingArea && isCatchFish)
            {
                fishingMinigame.SetActive(true);
                isCatchFish = false;
            }
        }

        if (context.canceled)
        {

        }
    }
    #endregion
}
