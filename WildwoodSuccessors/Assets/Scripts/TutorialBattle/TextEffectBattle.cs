using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public class TextEffectBattle : MonoBehaviour
{
    [SerializeField] public TMP_Text textTest;

    int charC = 0;
    [SerializeField] float delayRead;
    [SerializeField] float delayDestroy;
    [SerializeField] int newline;
    [HideInInspector] public ControlTutorialBattle controlTutorialBattle;
    Coroutine startTextEffect;

    // Start is called before the first frame update
    void Start()
    {
        RestRead();
    }
    public void StartPlayText(string suptitle, bool check)
    {
        if (startTextEffect != null)
        {
            StopCoroutine(startTextEffect);
        }
        startTextEffect = StartCoroutine(ReadText(suptitle, check));
    }
    public void StartPlayText(string[] suptitle)
    {
        StartCoroutine(ReadText(suptitle));
    }
    public IEnumerator ReadText(string suptitle, bool check)
    {
        yield return new WaitForSeconds(1);
        RestRead();
        while (charC < suptitle.Length)
        {
            yield return new WaitForSeconds(delayRead);
            charC++;
            string text = suptitle.Substring(0, charC);
            textTest.text = text;
        }
        yield return new WaitForSeconds(3);
        //EndCutScenes
        if (check)
        {
        }
    }
    public IEnumerator ReadText(string[] suptitle)
    {

        for (int i = 0; i < suptitle.Length; i++)
        {
            RestRead();
            while (charC < suptitle[i].Length)
            {
                yield return new WaitForSeconds(delayRead);
                charC++;
                string text = suptitle[i].Substring(0, charC);
                textTest.text = text;
            }
            yield return new WaitForSeconds(3);
        }
        //EndCutScenes
    }

    public void RestRead()
    {
        textTest.text = "";
        charC = 0;
    }

    public void SkipTextEffect()
    {
        delayRead = 0;
    }
}
