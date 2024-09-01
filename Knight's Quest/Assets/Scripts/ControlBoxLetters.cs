using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlBoxLetters : MonoBehaviour
{
    public List<char> latters = new List<char>();
    [Header("BoxChar")]
    [SerializeField] GameObject boxCharPrefab;
    [Header("PositionBoxChar")]
    [SerializeField] GameObject[] countBoxChar = new GameObject[15];
    [Header("BtnResetLetters")]
    [SerializeField] public GameObject[] positionLetters;
    [Header("BoxVocabulary")]
    [SerializeField] string vocabulary = "";
    [SerializeField] string vocabularyCanMarge = "";
    [SerializeField] int pointVocabulary;
    public List<BoxLetters> boxLettersOnVocabulary = new List<BoxLetters>();
    [Header("Position Vocabulary")]
    [SerializeField] Transform centerPositionVocabulary;
    // Start is called before the first frame update
    void Start()
    {
        ControlGamePlay._instance.controlBoxLetters = this;
        GetLetters();
    }
    private static Dictionary<char, int> LettersWeights = new Dictionary<char, int>()
    {
        {'A', 8}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 12},
        {'F', 2}, {'G', 2}, {'H', 6}, {'I', 7}, {'J', 1},
        {'K', 1}, {'L', 4}, {'M', 2}, {'N', 7}, {'O', 8},
        {'P', 2}, {'Q', 1}, {'R', 6}, {'S', 6}, {'T', 9},
        {'U', 3}, {'V', 1}, {'W', 2}, {'X', 1}, {'Y', 2},
        {'Z', 1}
    };
    void GetLetters()
    {
        foreach (var lar in LettersWeights)
        {
            for (int i = 0; i < lar.Value; i++)
            {
                latters.Add(lar.Key);
            }
        }
    }
    char RndLetters()
    {
        int rndLatter = UnityEngine.Random.Range(0, latters.Count);
        return latters[rndLatter];
    }

    public void SpawnAllBoxLetters()
    {
        if (countBoxChar[0] == null)
        {
            for (int i = 0; i < positionLetters.Length; i++)
            {
                SpawnBoxLetters(i);
            }
        }
        CheckLettersInPull();
    }
    void SpawnBoxLetters(int _indexBoxLetters)
    {
        BoxLetters _boxLetters;
        //spawn boxLetters
        countBoxChar[_indexBoxLetters] = Instantiate(boxCharPrefab, positionLetters[_indexBoxLetters].transform.position
        , boxCharPrefab.transform.rotation, ControlGamePlay._instance.uiBoxLetters.transform);
        _boxLetters = countBoxChar[_indexBoxLetters].GetComponent<BoxLetters>();
        _boxLetters.SetPositonSpawnLetters(ControlGamePlay._instance, positionLetters[_indexBoxLetters].transform.position, _indexBoxLetters);
        ResetLetters(countBoxChar[_indexBoxLetters]);
    }
    public void ResetLettersAll()
    {
        if (boxLettersOnVocabulary.Count == 0)
        {
            foreach (GameObject cbx in countBoxChar)
            {
                ResetLetters(cbx);
            }
        }
        CheckLettersInPull();
    }
    void ResetLetters(GameObject _boxLetters)
    {
        int _Ratepoint = UnityEngine.Random.Range(0, 101);
        int _point = 1;
        if (_Ratepoint < 75)
        {
            _point = 1;
        }
        else if (_Ratepoint > 75 && _Ratepoint < 92)
        {
            _point = 2;
        }
        else if (_Ratepoint > 92)
        {
            _point = 3;
        }

        int changeUpgradePoint = UnityEngine.Random.Range(0, 101);

        if (changeUpgradePoint <= ControlGamePlay._instance.controlPlayer.chancePointLettersUp)
        {
            if (_point < 3)
            {
                _point++;
            }
        }
        _boxLetters.GetComponent<BoxLetters>().SetLetters(RndLetters(), _point);
    }
    public void SetPositionAndAddBoxLettersOnVocabularyList(BoxLetters _boxLetters)
    {
        //Add boxLetters To List Vocabulary
        if (_boxLetters != null)
        {
            ControlGamePlay._instance.optionInGamePlay.SetActive(false);
            boxLettersOnVocabulary.Add(_boxLetters);
            vocabulary += _boxLetters.Latters();
        }
        float splitBoxLetters = 0;
        float positionLeftBoxLetterss = 0;
        if (boxLettersOnVocabulary.Count % 2 == 0)
        {
            splitBoxLetters = 2.67f;
            positionLeftBoxLetterss = boxLettersOnVocabulary.Count / splitBoxLetters;
        }
        else
        {
            splitBoxLetters = 2f;
            positionLeftBoxLetterss = (boxLettersOnVocabulary.Count - 1) / splitBoxLetters;
        }
        //-1.1f distance boxLetters
        for (int i = 0; i < boxLettersOnVocabulary.Count; i++)//Set all position boxLetters
        {
            if (i < boxLettersOnVocabulary.Count)
            {
                Vector2 target = new Vector2(centerPositionVocabulary.position.x + positionLeftBoxLetterss * -1.1f
                , centerPositionVocabulary.position.y);
                boxLettersOnVocabulary[i].GetComponent<BoxLetters>().StartMoveToPositionTarget(target);
            }
            positionLeftBoxLetterss--;
        }
        ControlGamePlay._instance.dataVocabularyManager.CheckVocabulary(vocabulary);//check vocabulary in list data
    }
    public void ResetSetPositonBoxLettersFromVocabularyList(BoxLetters _boxLetters, Vector2 _lastPosition)
    {
        _boxLetters.Highlight(false);
        _boxLetters.HighlightRepeated(false);
        #region Remove Letters in now Vocabulary list
        int indexRemoveLetters = 0;
        for (int i = 0; i < boxLettersOnVocabulary.Count; i++)
        {
            if (boxLettersOnVocabulary[i] == _boxLetters)
            {
                indexRemoveLetters = i;
            }
        }
        vocabulary = vocabulary.Remove(indexRemoveLetters, 1);
        #endregion
        _boxLetters.StartMoveToPositionTarget(_lastPosition);//return Box Vocabulary to pull
        boxLettersOnVocabulary.Remove(_boxLetters);//remove Box Vocabulary from list Box Vocabulary
        SetPositionAndAddBoxLettersOnVocabularyList(null);//reset position box Vocabulary in list
        ControlGamePlay._instance.dataVocabularyManager.CheckVocabulary(vocabulary);//check Vocabulary
        if (boxLettersOnVocabulary.Count == 0)
        {
            ControlGamePlay._instance.optionInGamePlay.SetActive(true);
        }
    }
    public void OnhighlightBoxLetters(bool _notRepeated)
    {
        OffhighlightBoxLetters();
        foreach (BoxLetters bxt in boxLettersOnVocabulary)
        {
            if (_notRepeated)
            {
                bxt.Highlight(true);
            }
            else
            {
                bxt.HighlightRepeated(true);
            }
        }

    }
    public void OffhighlightBoxLetters()
    {
        foreach (BoxLetters bxt in boxLettersOnVocabulary)
        {
            bxt.Highlight(false);
            bxt.HighlightRepeated(false);
        }
    }

    public void AttackVocabulary(Monster _moster, TMP_Text _textReport, DataVocabularyManager _dataVocabularyManager)
    {
        _dataVocabularyManager.AddVocabularyInGame(_dataVocabularyManager.FindVocabulary);
        List<BoxLetters> _newSpawnBox = new List<BoxLetters>();
        int damage = 0;
        for (int i = 0; i < boxLettersOnVocabulary.Count; i++)
        {
            pointVocabulary += boxLettersOnVocabulary[i].Point();//Total point
            damage = (boxLettersOnVocabulary.Count + pointVocabulary) * 3;//Total damage
        }
        foreach (BoxLetters bx in boxLettersOnVocabulary)
        {
            _newSpawnBox.Add(bx);
            Destroy(bx.gameObject);//Remove BoxVocabulary
        }
        vocabulary = "";
        boxLettersOnVocabulary.Clear();
        damage += ControlGamePlay._instance.controlPlayer.Damage();
        _moster.TakeDamage(damage);
        foreach (BoxLetters nbx in _newSpawnBox)
        {
            SpawnBoxLetters(nbx.IndexBoxLetters());
        }
        #region ShowReportVocabulary
        _textReport.text += damage.ToString() + " : Damage " + '\n';
        _textReport.text += _dataVocabularyManager.FindVocabulary.vocabulary + " ";
        _textReport.text += _dataVocabularyManager.FindVocabulary.reading.ToString() + " ";
        _textReport.text += _dataVocabularyManager.FindVocabulary.meaning.ToString() + '\n';
        _textReport.text += "---------------------------------------" + '\n';
        #endregion
        pointVocabulary = 0;
        ControlGamePlay._instance.optionInGamePlay.SetActive(true);
        CheckLettersInPull();
    }

    void CheckLettersInPull()
    {
        //Check Letters In Pull Can Marge
        //collect ,split and count Letters in pull
        #region DataVocabulary
        Dictionary<char, int> _LettersInVocabulary = new Dictionary<char, int>();
        var _dataVocabulary = ControlGamePlay._instance.dataVocabularyManager.categoryVocabularyData;
        List<Vocabulary> _VocabularyInData = new List<Vocabulary>();
        #endregion
        #region Letters in Pull
        Dictionary<char, int> _DataLettersInPull = new Dictionary<char, int>();
        List<char> _LettersInPull = new List<char>();
        #endregion
        int margeTarget = 0;
        int countMarge = 0;
        string _vocabularyCanMarge = "";
        _DataLettersInPull.Clear();
        _LettersInVocabulary.Clear();

        #region Hint
        bool hintNotRepates = false;
        bool haveMarge = false;
        #endregion
        for (char _categoryPull = 'A'; _categoryPull <= 'Z'; _categoryPull++)//creat category
        {
            _DataLettersInPull[_categoryPull] = 0;
            _LettersInVocabulary[_categoryPull] = 0;
        }
        for (int _countLetters = 0; _countLetters < countBoxChar.Length; _countLetters++)//Split Letters In pull
        {
            BoxLetters _boxLetters = countBoxChar[_countLetters].GetComponent<BoxLetters>();
            string _LettersText = _boxLetters.Latters();
            char _Letters = _LettersText[0];
            _Letters = char.ToUpper(_Letters);
            _DataLettersInPull[_Letters]++;
        }
        foreach (var _LVP in _DataLettersInPull)
        {
            if (_LVP.Value > 0)
            {
                char _key = _LVP.Key;
                _key = char.ToUpper(_key);
                _LettersInPull.Add(_key);
            }
        }

        // Debug.Log(_LettersInPull[0]);
        for (int indexLetters = 0; indexLetters < _LettersInPull.Count; indexLetters++)//Choose Vocabulary
        {
            foreach (Vocabulary _vd in _dataVocabulary[_LettersInPull[indexLetters]])//Add Vocabulary in data To List Vocabulary
            {
                _VocabularyInData.Add(_vd);
            }
            //Cout and Split Vocabulary
            for (int rangeVocabulary = 0; rangeVocabulary < _VocabularyInData.Count; rangeVocabulary++)
            {
                for (int LeLetInVoc = 0; LeLetInVoc < _VocabularyInData[rangeVocabulary].vocabulary.Length; LeLetInVoc++)
                {
                    //add Latters at Split Vocabulary and count Latters
                    char _LettersSplite = _VocabularyInData[rangeVocabulary].vocabulary[LeLetInVoc];
                    _LettersSplite = char.ToUpper(_LettersSplite);
                    // Debug.Log(_LettersSplite);
                    _LettersInVocabulary[_LettersSplite]++;
                    _vocabularyCanMarge = _VocabularyInData[rangeVocabulary].vocabulary;
                }
                foreach (var _LVD in _LettersInVocabulary)
                {
                    if (_LVD.Value > 0)
                    {
                        margeTarget++;
                        if (_DataLettersInPull[_LVD.Key] >= _LVD.Value)
                        {
                            countMarge++;
                        }
                    }
                }
                if (margeTarget == countMarge)
                {
                    haveMarge = true;
                    foreach (Vocabulary vig in ControlGamePlay._instance.dataVocabularyManager.vocabularyInGame)
                    {
                        if (_vocabularyCanMarge.ToUpper() == vig.vocabulary.ToUpper())
                        {
                            hintNotRepates = true;
                        }
                    }
                    if (!hintNotRepates)
                    {
                        vocabularyCanMarge = _vocabularyCanMarge;
                    }
                }
                _LettersInVocabulary.Clear();
                for (char _categoryPull = 'A'; _categoryPull <= 'Z'; _categoryPull++)//creat category
                {
                    _LettersInVocabulary[_categoryPull] = 0;
                }
                _vocabularyCanMarge = "";
                margeTarget = 0;
                countMarge = 0;
                hintNotRepates = false;
            }
            _VocabularyInData.Clear();
        }
        if (!haveMarge)
        {
            Debug.Log("Not Have Marge");
            ControlGamePlay._instance.ResetBoxLetterAll();
        }
    }

    public string Hint()
    {
        return vocabularyCanMarge;
    }
    public void ResetGamePlay()
    {
        ResetLettersAll();

    }
}
