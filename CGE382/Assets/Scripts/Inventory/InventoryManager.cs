using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotCount;

    // #region PlayerPrefs
    // //PlayerPrefs
    // //Inventory
    // string[] keySaveDataItem1 = {"Player1InventorySlot01" , "Player1InventorySlot02" , "Player1InventorySlot03" ,"Player1InventorySlot04" ,"Player1InventorySlot05",
    //                         "Player1InventorySlot06" , "Player1InventorySlot07" , "Player1InventorySlot08" ,"Player1InventorySlot09" ,"Player1InventorySlot10",
    //                         "Player1InventorySlot11" , "Player1InventorySlot12" , "Player1InventorySlot13" ,"Player1InventorySlot14" ,"Player1InventorySlot15",
    //                         "Player1InventorySlot16" , "Player1InventorySlot17" , "Player1InventorySlot18"};
    // string[] keySaveDataItem2 = {"Player2InventorySlot01" , "Player2InventorySlot02" , "Player2InventorySlot03" ,"Player2InventorySlot04" ,"Player2InventorySlot05",
    //                         "Player2InventorySlot06" , "Player2InventorySlot07" , "Player2InventorySlot08" ,"Player2InventorySlot09" ,"Player2InventorySlot10",
    //                         "Player2InventorySlot11" , "Player2InventorySlot12" , "Player2InventorySlot13" ,"Player2InventorySlot14" ,"Player2InventorySlot15",
    //                         "Player2InventorySlot16" , "Player2InventorySlot17" , "Player2InventorySlot18"};
    // string[] keySaveDataItem3 = {"Player3InventorySlot01" , "Player3InventorySlot02" , "Player3InventorySlot03" ,"Player3InventorySlot04" ,"Player3InventorySlot05",
    //                         "Player3InventorySlot06" , "Player3InventorySlot07" , "Player3InventorySlot08" ,"Player3InventorySlot09" ,"Player3InventorySlot10",
    //                         "Player3InventorySlot11" , "Player3InventorySlot12" , "Player3InventorySlot13" ,"Player3InventorySlot14" ,"Player3InventorySlot15",
    //                         "Player3InventorySlot16" , "Player3InventorySlot17" , "Player3InventorySlot18"};
    // string[] keySaveCountItem1 = {"Player1InventorySlotCount01" , "Player1InventorySlotCount02" , "Player1InventorySlotCount03" ,"Player1InventorySlotCount04" ,"Player1InventorySlotCount05",
    //                         "Player1InventorySlotCount06" , "Player1InventorySlotCount07" , "Player1InventorySlotCount08" ,"Player1InventorySlotCount09" ,"Player1InventorySlotCount10",
    //                         "Player1InventorySlotCount11" , "Player1InventorySlotCount12" , "Player1InventorySlotCount13" ,"Player1InventorySlotCount14" ,"Player1InventorySlotCount15",
    //                         "Player1InventorySlotCount16" , "Player1InventorySlotCount17" , "Player1InventorySlotCount18"};
    // string[] keySaveCountItem2 = {"Player2InventorySlotCount01" , "Player2InventorySlotCount02" , "Player2InventorySlotCount03" ,"Player2InventorySlotCount04" ,"Player2InventorySlotCount05",
    //                         "Player2InventorySlotCount06" , "Player2InventorySlotCount07" , "Player2InventorySlotCount08" ,"Player2InventorySlotCount09" ,"Player2InventorySlotCount10",
    //                         "Player2InventorySlotCount11" , "Player2InventorySlotCount12" , "Player2InventorySlotCount13" ,"Player2InventorySlotCount14" ,"Player2InventorySlotCount15",
    //                         "Player2InventorySlotCount16" , "Player2InventorySlotCount17" , "Player2InventorySlotCount18"};
    // string[] keySaveCountItem3 = {"Player3InventorySlotCount01" , "Player3InventorySlotCount02" , "Player3InventorySlotCount03" ,"Player3InventorySlotCount04" ,"Player3InventorySlotCount05",
    //                         "Player3InventorySlotCount06" , "Player3InventorySlotCount07" , "Player3InventorySlotCount08" ,"Player3InventorySlotCount09" ,"Player3InventorySlotCount10",
    //                         "Player3InventorySlotCount11" , "Player3InventorySlotCount12" , "Player3InventorySlotCount13" ,"Player3InventorySlotCount14" ,"Player3InventorySlotCount15",
    //                         "Player3InventorySlotCount16" , "Player3InventorySlotCount17" , "Player3InventorySlotCount18"};

    // //Toolbar
    // string[] keySaveDataItemToolbar1 = {"Player1ToolbarSlot01" , "Player1ToolbarSlot02" , "Player1ToolbarSlot03" ,"Player1ToolbarSlot04" ,"Player1ToolbarSlot05",
    //                         "Player1ToolbarSlot06" , "Player1ToolbarSlot07" , "Player1ToolbarSlot08", "Player1ToolbarSlot09"};
    // string[] keySaveDataItemToolbar2 = {"Player2ToolbarSlot01" , "Player2ToolbarSlot02" , "Player2ToolbarSlot03" ,"Player2ToolbarSlot04" ,"Player2ToolbarSlot05",
    //                         "Player2ToolbarSlot06" , "Player2ToolbarSlot07" , "Player2ToolbarSlot08", "Player2ToolbarSlot09"};
    // string[] keySaveDataItemToolbar3 = {"Player3ToolbarSlot01" , "Player3ToolbarSlot02" , "Player3ToolbarSlot03" ,"Player3ToolbarSlot04" ,"Player3ToolbarSlot05",
    //                         "Player3ToolbarSlot06" , "Player3ToolbarSlot07" , "Player3ToolbarSlot08", "Player3ToolbarSlot09"};

    // string[] keySaveDataCountToolbar1 = {"Player1ToolbarCountSlot01" , "Player1ToolbarCountSlot02" , "Player1ToolbarCountSlot03" ,"Player1ToolbarSlotCount04" ,"Player1ToolbarCountSlot05",
    //                         "Player1ToolbarCountSlot06" , "Player1ToolbarCountSlot07" , "Player1ToolbarCountSlot08", "Player1ToolbarCountSlot09"};
    // string[] keySaveDataCountToolbar2 = {"Player2ToolbarCountSlot01" , "Player2ToolbarCountSlot02" , "Player2ToolbarCountSlot03" ,"Player2ToolbarSlotCount04" ,"Player2ToolbarCountSlot05",
    //                         "Player2ToolbarCountSlot06" , "Player2ToolbarCountSlot07" , "Player2ToolbarCountSlot08", "Player2ToolbarCountSlot09"};
    // string[] keySaveDataCountToolbar3 = {"Player3ToolbarCountSlot01" , "Player3ToolbarCountSlot02" , "Player3ToolbarCountSlot03" ,"Player3ToolbarSlotCount04" ,"Player3ToolbarCountSlot05",
    //                         "Player3ToolbarCountSlot06" , "Player3ToolbarCountSlot07" , "Player3ToolbarCountSlot08", "Player3ToolbarCountSlot09"};
    // #endregion
    private void Awake()
    {
        if (SaveManager.Instance.isNewGame)
        {
            backpack = new Inventory(backpackSlotCount);
            toolbar = new Inventory(toolbarSlotCount);

            inventoryByName.Add("Backpack", backpack);
            inventoryByName.Add("Toolbar", toolbar);
        }
        else
        {
            backpack = new Inventory(backpackSlotCount);
            toolbar = new Inventory(toolbarSlotCount);

            inventoryByName.Add("Backpack", backpack);
            inventoryByName.Add("Toolbar", toolbar);
        }
    }

    void Start()
    {
        if (!SaveManager.Instance.isNewGame)
        {
            LoadDataJson();
        }
    }

    void Update()
    {
        
    }

    public void Add(string inventoryName, Item item)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item);
        }
    }

    public void Add(string inventoryName, ItemData item, int numToAdd)
    {
        Debug.Log(inventoryName);
        if (inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item, numToAdd);
        }
    }

    public void Add(Inventory inventory, ItemData item, int numToAdd)
    {
            inventory.Add(item, numToAdd);
    }

    public Inventory GetInventoryByName(string inventoryName)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }

        return null;
    }

    #region PlayerPref Save/Load
    // public void SaveInventoryData()
    // {
    //     #region Save Slot01
    //     if (SaveObject.Instance.slotUse == 0)
    //     {
    //         //Invantory
    //         for (int i = 0; i < keySaveDataItem1.Length; i++)
    //         {
    //             PlayerPrefs.SetString(keySaveDataItem1[i], backpack.slots[i].itemName);
    //             if (inventoryByName.TryGetValue("Backpack", out backpack))
    //             {
    //                 PlayerPrefs.SetInt(keySaveCountItem1[i], backpack.slots[i].count);
    //             }
    //         }
    //         //Toolbar
    //         for (int i = 0; i < keySaveDataItemToolbar1.Length; i++)
    //         {
    //             PlayerPrefs.SetString(keySaveDataItemToolbar1[i], toolbar.slots[i].itemName);
    //             if (inventoryByName.TryGetValue("Toolbar", out toolbar))
    //             {
    //                 PlayerPrefs.SetInt(keySaveDataCountToolbar1[i], toolbar.slots[i].count);
    //             }
    //         }
    //     }
    //     #endregion

    //     #region Save Slot02
    //     if (SaveObject.Instance.slotUse == 1)
    //     {
    //         //Inventory
    //         for (int i = 0; i < keySaveDataItem2.Length; i++)
    //         {
    //             PlayerPrefs.SetString(keySaveDataItem2[i], backpack.slots[i].itemName);
    //             if (inventoryByName.TryGetValue("Backpack", out backpack))
    //             {
    //                 PlayerPrefs.SetInt(keySaveCountItem2[i], backpack.slots[i].count);
    //             }
    //         }
    //         //Toolbar
    //         for (int i = 0; i < keySaveDataItemToolbar2.Length; i++)
    //         {
    //             PlayerPrefs.SetString(keySaveDataItemToolbar2[i], toolbar.slots[i].itemName);
    //             if (inventoryByName.TryGetValue("Toolbar", out toolbar))
    //             {
    //                 PlayerPrefs.SetInt(keySaveDataCountToolbar2[i], toolbar.slots[i].count);
    //             }
    //         }
    //     }
    //     #endregion

    //     #region Save Slot03
    //     if (SaveObject.Instance.slotUse == 2)
    //     {
    //         //Inventory
    //         for (int i = 0; i < keySaveDataItem3.Length; i++)
    //         {
    //             PlayerPrefs.SetString(keySaveDataItem3[i], backpack.slots[i].itemName);
    //             if (inventoryByName.TryGetValue("Backpack", out backpack))
    //             {
    //                 PlayerPrefs.SetInt(keySaveCountItem3[i], backpack.slots[i].count);
    //             }
    //         }
    //         //Toolbar
    //         for (int i = 0; i < keySaveDataItemToolbar3.Length; i++)
    //         {
    //             PlayerPrefs.SetString(keySaveDataItemToolbar3[i], toolbar.slots[i].itemName);
    //             if (inventoryByName.TryGetValue("Toolbar", out toolbar))
    //             {
    //                 PlayerPrefs.SetInt(keySaveDataCountToolbar3[i], toolbar.slots[i].count);
    //             }
    //         }
    //     }
    //     #endregion
    // }

    // public void LoadInventoryData()
    // {
    //     #region Load Save01
    //     if (SaveObject.Instance.slotUse == 0)
    //     {
    //         //Inventory
    //         for (int i = 0; i < keySaveDataItem1.Length; i++)
    //         {
    //             ItemData prefabDataItem = Resources.Load<ItemData>(PlayerPrefs.GetString(keySaveDataItem1[i]));
    //             if (prefabDataItem != null)
    //             {
    //                 Add("Backpack", prefabDataItem, PlayerPrefs.GetInt(keySaveCountItem1[i]));
    //             }
    //         }

    //         //Toolbar
    //         for (int i = 0; i < keySaveDataItemToolbar1.Length; i++)
    //         {
    //             ItemData prefabDataItem = Resources.Load<ItemData>(PlayerPrefs.GetString(keySaveDataItemToolbar1[i]));
    //             if (prefabDataItem != null)
    //             {
    //                 Add("Backpack", prefabDataItem, PlayerPrefs.GetInt(keySaveDataCountToolbar1[i]));
    //             }
    //         }

    //     }
    //     #endregion

    //     #region Load Save02
    //     if (SaveObject.Instance.slotUse == 1)
    //     {
    //         //Inventory
    //         for (int i = 0; i < keySaveDataItem2.Length; i++)
    //         {
    //             ItemData prefabDataItem = Resources.Load<ItemData>(PlayerPrefs.GetString(keySaveDataItem2[i]));
    //             if (prefabDataItem != null)
    //             {
    //                 Add("Backpack", prefabDataItem, PlayerPrefs.GetInt(keySaveCountItem2[i]));
    //             }
    //         }
    //         //Toolbar
    //         for (int i = 0; i < keySaveDataItemToolbar2.Length; i++)
    //         {
    //             ItemData prefabDataItem = Resources.Load<ItemData>(PlayerPrefs.GetString(keySaveDataItemToolbar2[i]));
    //             if (prefabDataItem != null)
    //             {
    //                 Add("Backpack", prefabDataItem, PlayerPrefs.GetInt(keySaveDataCountToolbar2[i]));
    //             }
    //         }
    //     }
    //     #endregion

    //     #region Load Save03
    //     if (SaveObject.Instance.slotUse == 2)
    //     {
    //         //Inventory
    //         for (int i = 0; i < keySaveDataItem2.Length; i++)
    //         {
    //             ItemData prefabDataItem = Resources.Load<ItemData>(PlayerPrefs.GetString(keySaveDataItem2[i]));
    //             if (prefabDataItem != null)
    //             {
    //                 Add("Backpack", prefabDataItem, PlayerPrefs.GetInt(keySaveCountItem2[i]));
    //             }
    //         }
    //         //Toolbar
    //         for (int i = 0; i < keySaveDataItemToolbar3.Length; i++)
    //         {
    //             ItemData prefabDataItem = Resources.Load<ItemData>(PlayerPrefs.GetString(keySaveDataItemToolbar3[i]));
    //             if (prefabDataItem != null)
    //             {
    //                 Add("Backpack", prefabDataItem, PlayerPrefs.GetInt(keySaveDataCountToolbar3[i]));
    //             }
    //         }
    //     }
    //     #endregion
    // }
    #endregion

    #region Json Save/Load
    public void SaveDataJson()
    {
        string inventoryFolderName = "Inventory";
        string toolbarFolderName = "Toolbar";
        string path = Path.Combine(Application.persistentDataPath, inventoryFolderName);
        string path2 = Path.Combine(Application.persistentDataPath, toolbarFolderName);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (!Directory.Exists(path2))
        {
            Directory.CreateDirectory(path2);
        }

        if (SaveManager.Instance.slotUse == 0)
        {
            //inventory
            string inventoryData = JsonUtility.ToJson(backpack);
            string filePath = Application.persistentDataPath + "/" + inventoryFolderName + "/" + "invetory0.json";
            System.IO.File.WriteAllText(filePath, inventoryData);

            //toolbar
            string toolBarData = JsonUtility.ToJson(toolbar);
            string filePathToolbar = Application.persistentDataPath + "/" + toolbarFolderName + "/" + "toolbar0.json";
            System.IO.File.WriteAllText(filePathToolbar, toolBarData);
            Debug.Log("Save0");
        }
        if (SaveManager.Instance.slotUse == 1)
        {
            //inventor
            string inventoryData = JsonUtility.ToJson(backpack);
            string filePath = Application.persistentDataPath + "/" + inventoryFolderName + "/" + "invetory1.json";
            System.IO.File.WriteAllText(filePath, inventoryData);

            //toolbar
            string toolBarData = JsonUtility.ToJson(toolbar);
            string filePathToolbar = Application.persistentDataPath + "/" + toolbarFolderName + "/" + "toolbar1.json";
            System.IO.File.WriteAllText(filePathToolbar, toolBarData);
            Debug.Log("Save1");
        }
        if (SaveManager.Instance.slotUse == 2)
        {
            //inventory
            string inventoryData = JsonUtility.ToJson(backpack);
            string filePath = Application.persistentDataPath + "/" + inventoryFolderName + "/" + "invetory2.json";
            System.IO.File.WriteAllText(filePath, inventoryData);

            //toolbar
            string toolBarData = JsonUtility.ToJson(toolbar);
            string filePathToolbar = Application.persistentDataPath + "/" + toolbarFolderName + "/" + "toolbar2.json";
            System.IO.File.WriteAllText(filePathToolbar, toolBarData);
            Debug.Log("Save2");
        }
    }

    public void LoadDataJson()
    {
        if (SaveManager.Instance.slotUse == 0)
        {
            //inventory
            string folderName = "Inventory";
            string filePath = Application.persistentDataPath + "/" + folderName + "/" + "invetory0.json";
            string inventoryData = System.IO.File.ReadAllText(filePath);

            backpack = JsonUtility.FromJson<Inventory>(inventoryData);

            //toolbar
            string folderToolbarName = "Toolbar";
            string filePathToolbar = Application.persistentDataPath + "/" + folderToolbarName + "/" + "toolbar0.json";
            string toolbarData = System.IO.File.ReadAllText(filePathToolbar);
            toolbar = JsonUtility.FromJson<Inventory>(toolbarData);
            Debug.Log("Load0");
        }
        if (SaveManager.Instance.slotUse == 1)
        {
            //inventory
            string folderName = "Inventory";
            string filePath = Application.persistentDataPath + "/" + folderName + "/" + "invetory1.json";
            string inventoryData = System.IO.File.ReadAllText(filePath);

            backpack = JsonUtility.FromJson<Inventory>(inventoryData);

            //toolbar
            string folderToolbarName = "Toolbar";
            string filePathToolbar = Application.persistentDataPath + "/" + folderToolbarName + "/" + "toolbar1.json";
            string toolbarData = System.IO.File.ReadAllText(filePathToolbar);
            toolbar = JsonUtility.FromJson<Inventory>(toolbarData);
            Debug.Log("Load1");
        }
        if (SaveManager.Instance.slotUse == 2)
        {
            //inventory
            string folderName = "Inventory";
            string filePath = Application.persistentDataPath + "/" + folderName + "/" + "invetory2.json";
            string inventoryData = System.IO.File.ReadAllText(filePath);

            backpack = JsonUtility.FromJson<Inventory>(inventoryData);

            //toolbar
            string folderToolbarName = "Toolbar";
            string filePathToolbar = Application.persistentDataPath + "/" + folderToolbarName + "/" + "toolbar2.json";
            string toolbarData = System.IO.File.ReadAllText(filePathToolbar);
            toolbar = JsonUtility.FromJson<Inventory>(toolbarData);
            Debug.Log("Load2");
        }
    }
    #endregion
}
