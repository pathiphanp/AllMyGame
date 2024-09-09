using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "", menuName = "CreatCategoryVocabulary")]
public class CategoryVocabulary : ScriptableObject
{
    // public Dictionary<char, List<Vocabulary>> categoryVocabularyData = new Dictionary<char, List<Vocabulary>>();
    public List<Vocabulary> vocabularies = new List<Vocabulary>();
}
