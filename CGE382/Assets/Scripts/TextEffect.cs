using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField] TMP_Text textTest;
    [SerializeField] DescriptionData[] supText;
    int charC = 0;
    [SerializeField] float delayRead;
    [SerializeField] float delayDestroy;
    bool read = true;
    int datanewline;
    [SerializeField] int newline;

    // Start is called before the first frame update
    void Start()
    {
        RestRead();
        StartCoroutine(ReadText(supText[0].description[0]));
    }

    IEnumerator ReadText(string suptitle)
    {
        while (charC < suptitle.Length)
        {
            yield return new WaitForSeconds(delayRead);
            charC++;
            string text = suptitle.Substring(0, charC);
            textTest.text = text;
        }
        //EndCutScenes
    }

    void RestRead()
    {
        textTest.text = "";
        charC = 0;
    }

    public void SkipTextEffect()
    {
        delayRead = 0;
    }
}
