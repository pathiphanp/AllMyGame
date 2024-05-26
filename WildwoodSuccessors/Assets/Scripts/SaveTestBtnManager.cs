using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTestBtnManager : MonoBehaviour
{
    public Button saveBtn;
    public DataManager dataManager;
    public TimeCycle timeCycle;
    public InventoryManager inventoryManager;
    public SkillManager skillManager;
    public DataUpgradeSkill dataUpgradeSkill;
    public PlantFarming[] allPlantFarming;

    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        //saveBtn.onClick.AddListener(AllSaveFunction);
        allPlantFarming = FindObjectsOfType<PlantFarming>();
    }

    public void AllSaveFunction()
    {
        dataManager.SaveData();
        timeCycle.SaveData();
        skillManager.SaveDataSkill();
        dataUpgradeSkill.SaveDataSkill();
        inventoryManager.SaveDataJson();
        foreach (PlantFarming p in allPlantFarming)
        {
            p.SaveData();
        }
        //inventoryManager.SaveInventoryData();
    }

    public void LoadGame()
    {
        if (audioManager != null)
        {
            Destroy(audioManager.gameObject);
        }
    }
}
