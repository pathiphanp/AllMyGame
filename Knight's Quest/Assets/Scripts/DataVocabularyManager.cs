using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;
using System.Threading;
public class DataVocabularyManager : Singleton<DataVocabularyManager>
{
    #region Vocabulary Data
    [Header("DataVocabulary")]
    string[] rawdata;
    Vocabulary[] dataVocabularyA_Z;
    // public Dictionary<char, List<Vocabulary>> categoryVocabularyData = new Dictionary<char, List<Vocabulary>>();
    public List<CategoryVocabulary> categoryVocabulary;
    #endregion
    public List<Vocabulary> vocabularyNotRepeated = new List<Vocabulary>();
    string testSaveText;

    [Header("DataVocabularyInGame")]
    [SerializeField] public List<Vocabulary> vocabularyInGame = new List<Vocabulary>();
    [SerializeField] Vocabulary findVocabulary;

    [SerializeField] GameObject[] waitGameStart;
    public Vocabulary FindVocabulary
    {
        get
        {
            return findVocabulary;
        }
    }
    public override void Awake()
    {
        base.Awake();
        LoadDeta();
    }
    // Start is called before the first frame update
    void Start()
    {
        ControlGamePlay._instance.dataVocabularyManager = this;
        CreateDictionaryVocabularyData();
    }
    void CreateDictionaryVocabularyData()
    {
        #region Create Dictionary Vocabulary Data
        for (char _category = 'A'; _category <= 'Z'; _category++)
        {
            categoryVocabulary.Add(Resources.Load<CategoryVocabulary>(_category.ToString()));
        }
        #endregion
        StartCoroutine(SplitVocabularyreadingmeaning());
    }
    private void LoadDeta()
    {
        TextAsset textFile = Resources.Load("Vocabulary") as TextAsset; //load Data'
        int countVocabulary = textFile.text.Split('\n').Length; // CountData Lenght
        rawdata = textFile.text.Split('|', '\n');// Split Data
        dataVocabularyA_Z = new Vocabulary[countVocabulary];
    }

    IEnumerator SplitVocabularyreadingmeaning()
    {
        #region  Split Vocabulary reading meaning
        int indexVocabulary = 0;
        for (int i = 0; i < rawdata.Length; i += 3)
        {
            dataVocabularyA_Z[indexVocabulary] = new Vocabulary
            {
                vocabulary = rawdata[i] = rawdata[i].Replace(" ", string.Empty)
                ,
                reading = rawdata[i + 1]
                ,
                meaning = rawdata[i + 2]
            };
            SplitCategoryVocabularyA_Z(dataVocabularyA_Z[indexVocabulary]);
            indexVocabulary++;
        }
        #endregion
        foreach (GameObject wi in waitGameStart)
        {
            wi.SetActive(true);
        }
        // RemoveRepeatedVocabulary();
        yield break;
    }
    void SplitCategoryVocabularyA_Z(Vocabulary _vocabulary)
    {
        #region Split Category Vocabulary A_Z
        string _category = _vocabulary.vocabulary[0].ToString().ToUpper();
        foreach (CategoryVocabulary _ct in categoryVocabulary)
        {
            if (_ct.name == _category.ToString())
            {
                _ct.vocabulary.Add(_vocabulary);
            }
        }
        #endregion
    }

    // void RemoveRepeatedVocabulary()
    // {
    //     int tatalVocabulary = 0;
    //     List<int> _repeatedVocabularyIndex = new List<int>();
    //     for (char category = 'A'; category <= 'Z'; category++)//category a - z 
    //     {
    //         for (int sV = 0; sV < categoryVocabularyData[category].Count; sV++) //choose vocobulary
    //         {
    //             string _repeated = categoryVocabularyData[category][sV].vocabulary;
    //             int categoryCount = categoryVocabularyData[category].Count;
    //             for (int tV = sV; tV < categoryCount; tV++)//check repeated vocobulary
    //             {
    //                 if (sV != tV)
    //                 {
    //                     if (_repeated == categoryVocabularyData[category][tV].vocabulary)
    //                     {
    //                         _repeatedVocabularyIndex.Add(tV);
    //                     }
    //                 }
    //             }
    //             for (int r = _repeatedVocabularyIndex.Count - 1; r >= 0; r--)//remove repeated Vocabulary
    //             {
    //                 categoryVocabularyData[category].RemoveAt(_repeatedVocabularyIndex[r]);
    //             }
    //             _repeatedVocabularyIndex.Clear();
    //         }
    //         //Add vorabulary not have repeated
    //         foreach (Vocabulary vcr in categoryVocabularyData[category])
    //         {
    //             vocabularyNotRepeated.Add(vcr);
    //         }
    //         tatalVocabulary += categoryVocabularyData[category].Count();
    //     }
    //     foreach (Vocabulary vcs in vocabularyNotRepeated)
    //     {
    //         testSaveText += vcs.vocabulary + "|" + vcs.reading + "|" + vcs.meaning + '\n';
    //     }
    //     // SaveStringListToFile("output.txt", testSaveText);
    //     foreach (GameObject wi in waitGameStart)
    //     {
    //         wi.SetActive(true);
    //     }
    //     AudioManager._instance.PlayMusic("BGSound");
    // }
    // void SaveStringListToFile(string fileName, string list)
    // {
    //     // สร้าง path ที่จะบันทึกไฟล์
    //     string path = Path.Combine(Application.persistentDataPath, fileName);
    //     // ใช้ StreamWriter เพื่อเขียนลงไฟล์
    //     using (StreamWriter writer = new StreamWriter(path))
    //     {
    //         writer.WriteLine(list);
    //     }
    // }
    public void CheckVocabulary(string _vocabularyInput)
    {
        findVocabulary = null;
        bool _repeated = false;
        if (_vocabularyInput.Count() > 0)
        {
            char _category = _vocabularyInput[0];
            _category = Char.ToUpper(_category);
            for (int allVocabulary = 0; allVocabulary < categoryVocabulary.Count; allVocabulary++)
            {
                foreach (Vocabulary cv in categoryVocabulary[allVocabulary].vocabulary)
                {
                    if (_vocabularyInput.ToUpper() == cv.vocabulary.ToUpper())
                    {
                        // Debug.Log("เจอ");
                        foreach (Vocabulary cvg in vocabularyInGame)
                        {
                            // Check repeated vocabulary in listVocabularyInGame
                            if (_vocabularyInput.ToUpper() == cvg.vocabulary.ToUpper())
                            {
                                _repeated = true;
                            }
                        }
                        if (!_repeated)
                        {
                            // Debug.Log("not reperted");
                            ControlGamePlay._instance.controlBoxLetters.OnhighlightBoxLetters(true);
                            ControlGamePlay._instance.canAtk = true;
                            findVocabulary = cv;
                        }
                        else
                        {
                            // Debug.Log("reperted");
                            ControlGamePlay._instance.controlBoxLetters.OnhighlightBoxLetters(false);
                        }
                        // Debug.Log(d.vocabulary + " : " + d.reading + " : " + d.meaning);
                        return;
                    }
                }
            }
            // Debug.Log("ไม่เจอ");
            ControlGamePlay._instance.canAtk = false;
            ControlGamePlay._instance.controlBoxLetters.OffhighlightBoxLetters();
        }
    }
    public void AddVocabularyInGame(Vocabulary _vocabulary)
    {
        vocabularyInGame.Add(_vocabulary);
    }
    public void ResetGamePlay()
    {
        vocabularyInGame.Clear();
    }
}