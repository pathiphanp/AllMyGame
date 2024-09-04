using System.Collections;
using TMPro;
using UnityEngine;
public class BoxLetters : MonoBehaviour
{
    [HideInInspector] ControlGamePlay controlGamePlay;
    [Header("Vocabulary")]
    [SerializeField] TMP_Text letter;
    Vector2 myLatterPoint;
    int indexBoxLetters;
    public int IndexBoxLetters()
    {
        return indexBoxLetters;
    }
    public string Latters()
    {
        return letter.text;
    }
    [SerializeField] int pointLatters;
    public int Point()
    {
        return pointLatters;
    }
    bool canClick = true;
    [SerializeField] float speedmove;
    [SerializeField] bool isOnVocabulary = false;
    Coroutine moveToPositionTarget;
    [SerializeField] GameObject[] pointprefab;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnMouseUp()
    {
        if (canClick)
        {
            canClick = false;
            AudioManager._instance.PlaySFX("ClickBox");
            if (!isOnVocabulary)
            {
                controlGamePlay.controlBoxLetters.SetPositionAndAddBoxLettersOnVocabularyList(this);
            }
            else if (isOnVocabulary)
            {
                controlGamePlay.controlBoxLetters.ResetSetPositonBoxLettersFromVocabularyList(this, myLatterPoint);
            }
            isOnVocabulary = !isOnVocabulary;
        }
    }
    public void SetPositonSpawnLetters(ControlGamePlay _controlGamePlay, Vector2 _positionLetter, int _indexBoxLetter)
    {
        controlGamePlay = _controlGamePlay;
        myLatterPoint = _positionLetter;
        indexBoxLetters = _indexBoxLetter;
    }
    public void SetLetters(char _letter, int _point)
    {
        letter.text = _letter.ToString();
        foreach (GameObject pf in pointprefab)//off all point perfab
        {
            pf.SetActive(false);
        }
        pointLatters = _point;
        if (pointLatters == 1)
        {
            pointprefab[_point - 1].SetActive(true);
        }
        else if (pointLatters == 2)
        {
            pointprefab[_point - 1].SetActive(true);
        }
        else if (pointLatters == 3)
        {
            pointprefab[_point - 1].SetActive(true);
        }
    }
    public void StartMoveToPositionTarget(Vector3 _positionTaget)
    {
        if (moveToPositionTarget == null)
        {
            moveToPositionTarget = StartCoroutine(MoveToPositionTarget(_positionTaget));
        }
        else
        {
            StopCoroutine(moveToPositionTarget);
        }
    }

    IEnumerator MoveToPositionTarget(Vector3 _positionTaget)
    {
        while (transform.position != _positionTaget)
        {
            transform.position = Vector2.MoveTowards(transform.position, _positionTaget, speedmove * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }
        canClick = true;
        moveToPositionTarget = null;
        yield break;
    }

    public void Highlight(bool setHighlight)
    {
        anim.SetBool("SetHighlight", setHighlight);
    }

    public void HighlightRepeated(bool setHighlight)
    {
        anim.SetBool("SetHighlightRepeated", setHighlight);
    }
}
