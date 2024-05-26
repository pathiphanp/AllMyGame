using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataUpgradeSkill : MonoBehaviour
{
    Element element;
    [Header("Fire Upgrade Data")]
    [SerializeField] List<ListDataSkill> fireUpgrade;
    [Header("Water Upgrade Data")]
    [SerializeField] List<ListDataSkill> waterUpgrade;
    [Header("Wind Upgrade Data")]
    [SerializeField] List<ListDataSkill> windUpgrade;
    [Header("Earth Upgrade Data")]
    [SerializeField] List<ListDataSkill> earthUpgrade;
    [Header("UI")]
    [SerializeField] GameObject UIUpgrade;
    [Header("Card Upgrade")]
    [SerializeField] CardControl[] card;
    [Header("Data Skills Upgrade")]
    DataSkill[] dataSkillsUpgrade = new DataSkill[5];
    string foldername = "DataUpgradeSkill";
    void Awake()
    {
        if (!SaveManager.Instance.isNewGame)
        {
            LoadSaveUpgradeDataSkill();
        }
        // GameManagerGameplay.Instance.upgradeSkill = this;
    }
    public void CreateUpgradeSkill(Element _element)
    {
        element = _element;
        CheckElement(_element);
    }
    void CheckElement(Element _element)
    {
        List<ListDataSkill> _elementUpgrade = new List<ListDataSkill>();
        if (_element == Element.FIRE)
        {
            _elementUpgrade = fireUpgrade;
        }
        if (_element == Element.WATER)
        {
            _elementUpgrade = waterUpgrade;
        }
        if (_element == Element.WIND)
        {
            _elementUpgrade = windUpgrade;
        }
        if (_element == Element.EARTH)
        {
            _elementUpgrade = earthUpgrade;
        }
        if (_elementUpgrade.Count > 0)
        {
            UIUpgrade.SetActive(true);
            CreateCard(_elementUpgrade);
        }
    }
    void CreateCard(List<ListDataSkill> _elementUpgrade)
    {
        //Add Upgrade to Card
        for (int i = 0; i < _elementUpgrade.Count; i++)
        {
            int rndUpgrade = UnityEngine.Random.Range(0, _elementUpgrade.Count);
            while (dataSkillsUpgrade[rndUpgrade] != null)
            {
                rndUpgrade = (rndUpgrade + 1) % _elementUpgrade.Count;
            }
            dataSkillsUpgrade[rndUpgrade] = _elementUpgrade[i].dataSkill[0];
        }
        //Send Update To Card 
        for (int i = 0; i < card.Length; i++)
        {
            if (dataSkillsUpgrade[i] != null)
            {
                card[i].AddCardUpgrade(dataSkillsUpgrade[i]);
            }
            else
            {
                card[i].ClearCard();
            }
        }
    }
    public void RemoveFromListUpgrade(DataSkill _dataSkill)
    {
        List<ListDataSkill> listRemove = new List<ListDataSkill>();
        listRemove.Clear();
        if (element == Element.FIRE)
        {
            listRemove = fireUpgrade;
        }
        if (element == Element.WATER)
        {
            listRemove = waterUpgrade;
        }
        if (element == Element.WIND)
        {
            listRemove = windUpgrade;
        }
        if (element == Element.EARTH)
        {
            listRemove = earthUpgrade;
        }
        for (int i = 0; i < listRemove.Count; i++)
        {
            listRemove[i].dataSkill.Remove(_dataSkill);
            if (listRemove[i].dataSkill.Count == 0)
            {
                listRemove.Remove(listRemove[i]);
            }
        }
        foreach (CardControl c in card)
        {
            c.ClearCard();
        }
        dataSkillsUpgrade = new DataSkill[5];
        UIUpgrade.SetActive(false);
    }

    public void SaveDataSkill()
    {
        #region New Folder Save
        string path = Path.Combine(Application.persistentDataPath, foldername);
        Debug.Log(Application.persistentDataPath);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        #endregion
        #region SaveGame
        for (int i = 0; i < 5; i++)
        {
            //Fire
            string dataSkillFire = JsonUtility.ToJson(fireUpgrade[i]);
            string namefileFire = "/" + fireUpgrade[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            string filePathFire = Application.persistentDataPath + "/" + foldername + namefileFire;
            System.IO.File.WriteAllText(filePathFire, dataSkillFire);
            //Water
            string dataSkillWater = JsonUtility.ToJson(waterUpgrade[i]);
            string namefileWater = "/" + waterUpgrade[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            string filePathWater = Application.persistentDataPath + "/" + foldername + namefileWater;
            System.IO.File.WriteAllText(filePathWater, dataSkillWater);
            //Wind
            string dataSkillWind = JsonUtility.ToJson(windUpgrade[i]);
            string namefileWind = "/" + windUpgrade[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            string filePathWind = Application.persistentDataPath + "/" + foldername + namefileWind;
            System.IO.File.WriteAllText(filePathWind, dataSkillWind);
            //Earth
            string dataSkillEarth = JsonUtility.ToJson(earthUpgrade[i]);
            string namefileEarth = "/" + earthUpgrade[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            string filePathEarth = Application.persistentDataPath + "/" + foldername + namefileEarth;
            System.IO.File.WriteAllText(filePathEarth, dataSkillEarth);

        }
        #endregion
        // Debug.Log("Save");
    }
    public void LoadSaveUpgradeDataSkill()
    {
        for (int i = 0; i < 5; i++)
        {
            //Fire
            string namefilefire = "/" + fireUpgrade[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            string filePathfire = Application.persistentDataPath + "/" + foldername + namefilefire;
            string dataSkillfire = System.IO.File.ReadAllText(filePathfire);
            fireUpgrade[i] = JsonUtility.FromJson<ListDataSkill>(dataSkillfire);
            //Water
            string namefileWater = "/" + waterUpgrade[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            string filePathWater = Application.persistentDataPath + "/" + foldername + namefileWater;
            string dataSkillWater = System.IO.File.ReadAllText(filePathWater);
            waterUpgrade[i] = JsonUtility.FromJson<ListDataSkill>(dataSkillWater);
            //Wind
            string namefileWind = "/" + windUpgrade[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            string filePathWind = Application.persistentDataPath + "/" + foldername + namefileWind;
            string dataSkillWind = System.IO.File.ReadAllText(filePathWind);
            windUpgrade[i] = JsonUtility.FromJson<ListDataSkill>(dataSkillWind);
            //Earth
            string namefileEarth = "/" + earthUpgrade[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            string filePathEarth = Application.persistentDataPath + "/" + foldername + namefileEarth;
            string dataSkillEarth = System.IO.File.ReadAllText(filePathEarth);
            earthUpgrade[i] = JsonUtility.FromJson<ListDataSkill>(dataSkillEarth);
        }
        // Debug.Log("Load");
    }
}
