using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeCycle : MonoBehaviour
{
    [Header("Detail about time")]
    public float tick; //Tick rate ความของหน่วยวินาที
    public float seconds;
    public int mins;
    public int hours;
    public int days;
    public bool isPause;
    public int totalTime;

    internal float useTick;
    internal int round;

    [Header("Manual")]
    public bool activateLight;
    public GameObject[] lights;

    [Header("Time skip")]
    public bool isTimeSkip;
    public float timeScaleValue;

    [Header("Check merchant and wizard come")]
    public int dayMerchantValue; //Value to check day. Merchant come or wizard come.

    [Header("Text")]
    public TMP_Text[] textTimes;
    public TMP_Text[] textDays;
    public GameObject warningText;
    public GameObject completeText;
    public GameObject worldCanvas;
    public Volume volume;

    //PlayerPrefs
    string[] saveDataDays = { "Player1DaySave", "Player2DaySave", "Player3DaySave" };
    string[] saveDayMerchantValue = { "Player1Daymerchant", "Player2Daymerchant", "Player3Daymerchant" };
    string[] saveIsDept = { "Player1Isdepth", "Player2Isdepth", "Player2Isdepth" };
    private void Start()
    {
        round = 10;
        isTimeSkip = false;

        if (SaveManager.Instance.isNewGame)
        {
            if (SaveManager.Instance.slotUse == 0)
            {
                days = 0;
                hours = 7;
                mins = 0;
                seconds = 0;
                PlayerPrefs.SetInt(saveDataDays[0], days);
                PlayerPrefs.SetInt(saveDayMerchantValue[0], dayMerchantValue);
                PlayerPrefs.SetInt(saveIsDept[0], (GameManagerPor.Instance.dataManager.isDept ? 1 : 0));
            }
            if (SaveManager.Instance.slotUse == 1)
            {
                days = 0;
                hours = 7;
                mins = 0;
                seconds = 0;
                PlayerPrefs.SetInt(saveDataDays[1], days);
                PlayerPrefs.SetInt(saveDayMerchantValue[1], dayMerchantValue);
                PlayerPrefs.SetInt(saveIsDept[1], (GameManagerPor.Instance.dataManager.isDept ? 1 : 0));
            }
            if (SaveManager.Instance.slotUse == 2)
            {
                days = 0;
                hours = 7;
                mins = 0;
                seconds = 0;
                PlayerPrefs.SetInt(saveDataDays[2], days);
                PlayerPrefs.SetInt(saveDayMerchantValue[2], dayMerchantValue);
                PlayerPrefs.SetInt(saveIsDept[2], (GameManagerPor.Instance.dataManager.isDept ? 1 : 0));
            }
        }
        else
        {
            if (SaveManager.Instance.slotUse == 0)
            {
                days = PlayerPrefs.GetInt(saveDataDays[0]);
                dayMerchantValue = PlayerPrefs.GetInt(saveDayMerchantValue[0]);
                GameManagerPor.Instance.dataManager.isDept = (PlayerPrefs.GetInt(saveIsDept[0]) == 1 ? true : false);
            }
            if (SaveManager.Instance.slotUse == 1)
            {
                days = PlayerPrefs.GetInt(saveDataDays[1]);
                dayMerchantValue = PlayerPrefs.GetInt(saveDayMerchantValue[1]);
                GameManagerPor.Instance.dataManager.isDept = (PlayerPrefs.GetInt(saveIsDept[1]) == 1 ? true : false);
            }
            if (SaveManager.Instance.slotUse == 2)
            {
                days = PlayerPrefs.GetInt(saveDataDays[2]);
                dayMerchantValue = PlayerPrefs.GetInt(saveDayMerchantValue[2]);
                GameManagerPor.Instance.dataManager.isDept = (PlayerPrefs.GetInt(saveIsDept[2]) == 1 ? true : false);
            }
        }
    }

    private void FixedUpdate()
    {
        CalcTime();
        if (round >= 10)
        {
            round = 0;
            DisplayTime();
        }
        ChangeFate();
        DayNightControl();

        if (isPause)
        {
            useTick = 0;
        }
        else
        {
            useTick = tick;
        }

        //Time Skip
        if (!isTimeSkip)
        {
            Time.timeScale = 1;
        }
        if (isTimeSkip)
        {
            Time.timeScale = timeScaleValue;
        }
    }

    public void CalcTime()
    {
        seconds += Time.fixedDeltaTime * useTick;

        if (seconds >= 60)
        {
            seconds = 0;
            round += 1;
            mins += 1;
        }

        if (mins >= 60)
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 24)
        {
            hours = 0;
            days += 1;
            GameManagerPor.Instance.shopManager.RandomItemInShop();

            if (dayMerchantValue == 0)
            {
                dayMerchantValue += 1;
                GameManagerPor.Instance.dataManager.isDept = true;
                GameManagerPor.Instance.dataManager.rent += 50;
            }
            else if (dayMerchantValue == 1 && GameManagerPor.Instance.dataManager.isDept == false)
            {
                dayMerchantValue = 0;
            }
        }
    }

    public void DayNightControl()
    {
        //ppv.weight = 0;
        if (hours >= 18 && hours < 19) // dusk at 21:00 / 9pm    -   until 22:00 / 10pm
        {
            volume.weight = (float)mins / 60; // since dusk is 1 hr, we just divide the mins by 60 which will slowly increase from 0 - 1

            if (!activateLight)
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(true); // turn them all on
                }
                activateLight = true;
            }
        }



        if (hours >= 6 && hours < 7) // Dawn at 6:00 / 6am    -   until 7:00 / 7am
        {
            volume.weight = 1 - (float)mins / 60; // we minus 1 because we want it to go from 1 - 0

            if (mins > 45) // wait until pretty bright
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(false); // shut them off
                }
                activateLight = false;
            }
        }
    }

    public void DisplayTime()
    {
        foreach (TMP_Text t in textTimes)
        {
            t.text = string.Format("{0:00}:{1:00}", hours, mins);
        }

        foreach (TMP_Text d in textDays)
        {
            d.text = string.Format("Day {0}", days);
        }
    }

    public void ChangeFate()
    {
        //Change to day
        if (hours == 6 && mins >= 0 && mins < 0.5f)
        {
            GameManagerGameplay.Instance.spawnEnemy.DestroyAllMoster();
            GameManagerGameplay.Instance.inputInCombat.SetHpTower();
            completeText.SetActive(true);
            worldCanvas.SetActive(true);
            isPause = true;
        }

        //Change to night and no dept
        if (hours == 20 && mins >= 0 && mins < 0.5f && GameManagerPor.Instance.dataManager.isDept == false)
        {
            GameManagerPor.Instance.stateCamera = StateInGame.WorldArea;
            GameManagerPor.Instance.stateManager.SetState();
            warningText.SetActive(true);
            isPause = true;
        }

        //Change to night and have dept. Game over
        if (hours == 20 && mins >= 0 && mins < 0.5f && GameManagerPor.Instance.dataManager.isDept == true)
        {
            Debug.Log("Game over");
            //warningText.SetActive(true);
            GameManagerPor.Instance.GameOver();
            isPause = true;
        }
    }

    public void SaveData()
    {
        if (SaveManager.Instance.slotUse == 0)
        {
            PlayerPrefs.SetInt(saveDataDays[0], days);
            PlayerPrefs.SetInt(saveDayMerchantValue[0], dayMerchantValue);
            PlayerPrefs.SetInt(saveIsDept[0], (GameManagerPor.Instance.dataManager.isDept ? 1 : 0));
        }
        if (SaveManager.Instance.slotUse == 1)
        {
            PlayerPrefs.SetInt(saveDataDays[1], days);
            PlayerPrefs.SetInt(saveDayMerchantValue[1], dayMerchantValue);
            PlayerPrefs.SetInt(saveIsDept[1], (GameManagerPor.Instance.dataManager.isDept ? 1 : 0));
        }
        if (SaveManager.Instance.slotUse == 2)
        {
            PlayerPrefs.SetInt(saveDataDays[2], days);
            PlayerPrefs.SetInt(saveDayMerchantValue[2], dayMerchantValue);
            PlayerPrefs.SetInt(saveIsDept[2], (GameManagerPor.Instance.dataManager.isDept ? 1 : 0));
        }
    }
}
