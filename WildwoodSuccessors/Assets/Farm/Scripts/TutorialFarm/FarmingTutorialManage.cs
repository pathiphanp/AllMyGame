using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FarmingTutorialManage : MonoBehaviour
{
    public int indexOfEvent;
    public int indexOfEvent2;
    public GameObject[] textLog;

    public GameObject[] textLog2;
    public GameObject[] textLog3;
    public GameObject[] textLog4;
    public bool isDialogue;
    public GameObject tutorialCanvas;
    public ItemData spSeed;

    [Header("Icon")]
    public GameObject fishingIcon;
    public GameObject farmingIcon;
    public GameObject houseIcon;
    public GameObject shopIcon;

    public void Update()
    {
        if (indexOfEvent2 == 0)
        {
            textUpdate1();
        }
        if (indexOfEvent2 == 1)
        {
            textUpdate2();
        }
        if (indexOfEvent2 == 2)
        {
            textUpdate3();
        }
        if (indexOfEvent2 == 3)
        {
            textUpdate4();
        }
    }

    public void textUpdate1()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isDialogue)
        {
            if (indexOfEvent != 6)
            {
                indexOfEvent++;
                ShowText(indexOfEvent, textLog);
                disableText(indexOfEvent - 1, textLog);
            }
            if (indexOfEvent == 6)
            {
                fishingIcon.SetActive(true);
            }
        }
    }

    public void textUpdate2()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isDialogue)
        {
            if (indexOfEvent != 1)
            {
                indexOfEvent++;
                ShowText(indexOfEvent, textLog2);
                disableText(indexOfEvent - 1, textLog2);
            }
            if (indexOfEvent == 1)
            {
                farmingIcon.SetActive(true);
            }
        }
    }

    public void textUpdate3()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isDialogue)
        {
            if (indexOfEvent != 5)
            {
                indexOfEvent++;
                ShowText(indexOfEvent, textLog3);
                disableText(indexOfEvent - 1, textLog3);
            }
            if (indexOfEvent == 5)
            {
                shopIcon.SetActive(true);
            }
        }
    }

    public void textUpdate4()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isDialogue)
        {
            if (indexOfEvent != 3)
            {
                indexOfEvent++;
                ShowText(indexOfEvent, textLog4);
                disableText(indexOfEvent - 1, textLog4);
            }
            if (indexOfEvent == 3)
            {
                houseIcon.SetActive(true);
            }
        }
    }

    public void ShowText(int i, GameObject[] g)
    {
        g[i].SetActive(true);
    }
    public void disableText(int i, GameObject[] g)
    {
        g[i].SetActive(false);
    }

    public void PlusIndex(int i)
    {
        indexOfEvent++;
    }

    public void SetIndexOfEvent(int i)
    {
        indexOfEvent = i;
    }

    public void SetIndexOfEvent2(int i)
    {
        indexOfEvent2 = i;
    }

    public void SetCanDialogue(bool b)
    {
        isDialogue = b;
    }

    public void DelayDialouge()
    {
        StartCoroutine(DelaySetIsDialouge());
    }
    IEnumerator DelaySetIsDialouge()
    {
        yield return new WaitForSeconds(2f);
        isDialogue = true;
    }
    public void AddSeed()
    {
        InventoryData.Instance.inventory.Add("Backpack", spSeed, 1);
    }

    public void timeSet(int i)
    {
        GameManagerPor.Instance.timeCycle.hours = i;
    }
}
