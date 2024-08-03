using UnityEngine;
using System;
using System.IO;
public enum Skill
{
    //Fire Skill
    FLAMETHROWER, METEOR, BALLFIRE, FLAMEWAVE, SHURIKENFLAME,
    //Water Skill
    RAIN, VOTEXWAVE, WATERBOMB, WATERWAVE, BUBBLE,
    //Wind Skill
    SPEARWIND, VACUUMBOMB, WINDSLASH, GUSTWIND, TORNADO,
    //Earth Skill
    EARTHSPIKE, MOUNTION, ROCK, EARTHWALL, EARTHQUAKE
}
public class SkillManager : Singleton<SkillManager>
{
    public static event Action UpdateDataSKill;
    [Header("List DataSkill")]
    [SerializeField] public ListDataSkill[] listDataSkills;
    int indexDataSkill;
    string foldername = "DataSkill";
    public override void Awake()
    {
        if (!SaveManager.Instance.isNewGame)
        {
            LoadSaveDataSkill();
        }
    }
    void Start()
    {
        SkillManager.StartUpdateDataSkill();
        indexDataSkill = listDataSkills.Length;
    }

    public static void StartUpdateDataSkill()
    {
        UpdateDataSKill?.Invoke();
    }
    public void UpgradeDataSkill(DataSkill dataSkill)
    {
        foreach (ListDataSkill ls in listDataSkills)
        {
            if (ls.dataSkill[0].skill == dataSkill.skill)
            {
                ls.dataSkill[0] = dataSkill;
            }
        }
        SkillManager.StartUpdateDataSkill();
    }
    public DataSkill PullDataSkill(Skill skill)
    {
        DataSkill skillPull = null;
        foreach (ListDataSkill ls in listDataSkills)
        {
            if (ls.dataSkill[0].skill == skill)
            {
                skillPull = ls.dataSkill[0];
            }
        }
        return skillPull;
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
        string dataSkill = "";
        string filePath = "";
        for (int i = 0; i < indexDataSkill; i++)
        {
            dataSkill = JsonUtility.ToJson(listDataSkills[i]);
            string namefile = "/" + listDataSkills[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            filePath = Application.persistentDataPath + "/" + foldername + namefile;
            System.IO.File.WriteAllText(filePath, dataSkill);
        }
        #endregion
        // Debug.Log("Save");
    }
    public void LoadSaveDataSkill()
    {
        for (int i = 0; i < indexDataSkill; i++)
        {
            string namefile = "/" + listDataSkills[i].nameSkill + SaveManager.Instance.slotUse + ".json";
            string filePath = Application.persistentDataPath + "/" + foldername + namefile;
            string dataSkill = System.IO.File.ReadAllText(filePath);
            listDataSkills[i] = JsonUtility.FromJson<ListDataSkill>(dataSkill);
        }
        // Debug.Log("Load");
    }

}
