using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlantFarming : MonoBehaviour
{
    public PlantData plantData = new PlantData();
    [Header("Status")]
    public bool havePlant;
    public bool wantWater;
    public bool complete;
    float timeDiasbleCount;

    [Header("Age")]
    float growthSpeed;
    [SerializeField] int token;
    [SerializeField] float seconds;
    [SerializeField] int mins;
    [SerializeField] bool isGrowth;

    //Test
    public GameObject sickle;
    public GameObject wantWaterObj;
    public PlantsType seedType;
    PlayerFarming player;
    public NotificationFarming notification;
    //InventoryManager
    public InventoryManager inventoryManager;

    //ItemData
    public ItemData itemData;

    SpriteRenderer sprite;

    [HideInInspector] public PlantedArea plantedArea;
    //Old
    float ogSpeed;
    float age;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = FindAnyObjectByType<PlayerFarming>();
        notification = FindAnyObjectByType<NotificationFarming>();

        if (!SaveManager.Instance.isNewGame)
        {
            LoadData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        NewScriptUpdate();
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadData();
        }
    }

    private void FixedUpdate()
    {
        //Disable self
        DisableThis();

        //Age plant
        //age += growthSpeed * Time.deltaTime;
        Growing();
        if (isGrowth)
        {
            growthSpeed = GameManagerPor.Instance.timeCycle.useTick;
        }
        else
        {
            growthSpeed = 0;
        }
    }

    //Watering Plants (Make this fuction for PLayerFarming Script to interact when Plants need to water)
    public void Watering()
    {
        token += 1;
        wantWater = false;
        isGrowth = true;
        notification.haveAction = false;
        sprite.sprite = seedType.stage02;
    }

    public void Harvesting()
    {
        //Instantiate(seedType.item, this.transform);
        RandomDrop();
        seedType = null;
        sprite.enabled = false;
        complete = false;
        sickle.gameObject.SetActive(false);
        notification.haveAction = false;
    }

    public void Planting()
    {
        seedType = player.plantsType;
        sprite.sprite = seedType.stage01;
        sprite.enabled = true;
        itemData = seedType.itemData;
    }

    public void RandomDrop()
    {
        int rnd = Random.Range(1, 4);
        //inventoryManager.backpack.AddItem(seedType.name, seedType.HarvestIcon, rnd, itemData, itemData.itemTpye);
        InventoryData.Instance.inventory.Add(InventoryData.Instance.inventory.backpack, itemData, rnd);
        Debug.Log(rnd);
    }

    public void DisableThis()
    {
        if (!havePlant)
        {
            timeDiasbleCount += Time.deltaTime;
        }
        else
        {
            timeDiasbleCount = 0f;
        }

        if (timeDiasbleCount >= 45f)
        {
            gameObject.GetComponentInParent<PlantedArea>().boxCollider.enabled = true;
            gameObject.GetComponentInParent<PlantedArea>().field.SetActive(false);
            gameObject.GetComponentInParent<PlantedArea>().plants.SetActive(false);
            timeDiasbleCount = 0f;
        }
    }

    public void OldScript()
    {
        if (seedType != null)
        {
            havePlant = true;
            //Growth
            if (age >= seedType.ageWatering && age < seedType.ageWatering + 1)
            {
                wantWater = true;
            }

            if (age > seedType.maxAge)
            {
                complete = true;
                sprite.sprite = seedType.stage03;
            }

            //Watering
            if (wantWater)
            {
                growthSpeed = 0;
                wantWaterObj.SetActive(true);
                notification.haveAction = true;
            }

            else
            {
                growthSpeed = ogSpeed;
                wantWaterObj.SetActive(false);
                notification.haveAction = false;
            }

            //Harvest
            if (complete)
            {
                sickle.SetActive(true);
                notification.haveAction = true;
                growthSpeed = 0;
            }
        }
        else
        {
            age = 0;
            growthSpeed = 0;
            havePlant = false;
        }
    }

    public void NewScriptUpdate()
    {
        if (seedType != null)
        {
            havePlant = true;
            //Growth
            if (token >= seedType.ageWatering && token < seedType.ageWatering + 1)
            {
                wantWater = true;
            }

            if (token > seedType.maxAge)
            {
                complete = true;
                sprite.sprite = seedType.stage03;
            }

            //Watering
            if (wantWater)
            {
                isGrowth = false;
                wantWaterObj.SetActive(true);
                notification.haveAction = true;
            }

            else
            {
                isGrowth = true;
                wantWaterObj.SetActive(false);
                //notification.haveAction = false;
            }

            //Harvest
            if (complete)
            {
                sickle.SetActive(true);
                notification.haveAction = true;
                isGrowth = false;
            }
        }
        else
        {
            token = 0;
            isGrowth = false;
            havePlant = false;
        }
    }

    public void Growing()
    {
        seconds += Time.fixedDeltaTime * growthSpeed;
        if (seconds >= 60)
        {
            seconds = 0;
            mins += 1;
        }
        if (mins >= 60)
        {
            mins = 0;
            token += 1;
        }
    }

    public void SaveData()
    {
        plantData.havePlant = havePlant;
        plantData.wantWater = wantWater;
        plantData.complete = complete;
        plantData.timeDiasbleCount = timeDiasbleCount;

        plantData.growthSpeed = growthSpeed;
        plantData.token = token;
        plantData.seconds = seconds;
        plantData.mins = mins;
        plantData.isGrowth = isGrowth;

        plantData.seedType = seedType;
        plantData.player = player;
        plantData.notification = notification;

        plantData.itemData = itemData;

        plantData.sprite = sprite.sprite;

        string folderName = "PlantsBox";
        string path = Path.Combine(Application.persistentDataPath, folderName);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (SaveManager.Instance.slotUse == 0)
        {
            string data = JsonUtility.ToJson(plantData);
            string filePath = Application.persistentDataPath + "/" + folderName + "/" + this.name + "0.json";
            System.IO.File.WriteAllText(filePath, data);
        }

        if (SaveManager.Instance.slotUse == 1)
        {
            string data = JsonUtility.ToJson(plantData);
            string filePath = Application.persistentDataPath + "/" + folderName + "/" + this.name + "1.json";
            System.IO.File.WriteAllText(filePath, data);
        }

        if (SaveManager.Instance.slotUse == 2)
        {
            string data = JsonUtility.ToJson(plantData);
            string filePath = Application.persistentDataPath + "/" + folderName + "/" + this.name + "2.json";
            System.IO.File.WriteAllText(filePath, data);
        }

        Debug.Log("Save");
    }

    public void LoadData()
    {
        if (SaveManager.Instance.slotUse == 0)
        {
            string folderName = "PlantsBox";
            string filePath = Application.persistentDataPath + "/" + folderName + "/" + this.name + "0.json";
            string data = System.IO.File.ReadAllText(filePath);

            plantData = JsonUtility.FromJson<PlantData>(data);
        }
        if (SaveManager.Instance.slotUse == 1)
        {
            string folderName = "PlantsBox";
            string filePath = Application.persistentDataPath + "/" + folderName + "/" + this.name + "1.json";
            string data = System.IO.File.ReadAllText(filePath);

            plantData = JsonUtility.FromJson<PlantData>(data);
        }
        if (SaveManager.Instance.slotUse == 2)
        {
            string folderName = "PlantsBox";
            string filePath = Application.persistentDataPath + "/" + folderName + "/" + this.name + "2.json";
            string data = System.IO.File.ReadAllText(filePath);

            plantData = JsonUtility.FromJson<PlantData>(data);
        }
        

        havePlant = plantData.havePlant;
        wantWater = plantData.wantWater;
        complete = plantData.complete;
        timeDiasbleCount = plantData.timeDiasbleCount;

        growthSpeed = plantData.growthSpeed;
        token = plantData.token;
        seconds = plantData.seconds;
        mins = plantData.mins;
        isGrowth = plantData.isGrowth;

        seedType = plantData.seedType;
        //player = plantData.player;
        //notification = plantData.notification;

        itemData = plantData.itemData;

        sprite.sprite = plantData.sprite;

        Debug.Log("Load");

        if (havePlant)
        {
            this.gameObject.SetActive(true);
            plantedArea.field.SetActive(true);
            sprite.enabled = true;
            plantedArea.boxCollider.enabled = false;
            Debug.Log("have" + this.gameObject.name);
        }
        else
        {
            this.gameObject.SetActive(false);
            timeDiasbleCount = 0f;
        }
    }
}
