using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int money;
    public int rent;
    public bool isDept;

    //PlayerPrefs
    string[] saveDataMoney = { "Player1MoneySave", "Player2MoneySave", "Player3MoneySave" };
    string[] saveDataRent = { "Player1RentSave", "Player2RentSave", "Player3RentSave" };
    void Start()
    {
        if (SaveManager.Instance.isNewGame)
        {
            if (SaveManager.Instance.slotUse == 0)
            {
                money = 200;
                rent = 50;
                PlayerPrefs.SetInt(saveDataMoney[0], money);
                PlayerPrefs.SetInt(saveDataRent[0], rent);
            }
            if (SaveManager.Instance.slotUse == 1)
            {
                money = 200;
                rent = 50;
                PlayerPrefs.SetInt(saveDataMoney[1], money);
                PlayerPrefs.SetInt(saveDataRent[1], rent);
            }
            if (SaveManager.Instance.slotUse == 2)
            {
                money = 200;
                rent = 50;
                PlayerPrefs.SetInt(saveDataMoney[2], money);
                PlayerPrefs.SetInt(saveDataRent[2], rent);
            }
        }
        else
        {
            if (SaveManager.Instance.slotUse == 0)
            {
                money = PlayerPrefs.GetInt(saveDataMoney[0]);
                rent = PlayerPrefs.GetInt(saveDataRent[0]);
            }
            if (SaveManager.Instance.slotUse == 1)
            {
                money = PlayerPrefs.GetInt(saveDataMoney[1]);
                rent = PlayerPrefs.GetInt(saveDataRent[1]);
            }
            if (SaveManager.Instance.slotUse == 2)
            {
                money = PlayerPrefs.GetInt(saveDataMoney[2]);
                rent = PlayerPrefs.GetInt(saveDataRent[2]);
            }
        }
    }

    public void SaveData()
    {
        if (SaveManager.Instance.slotUse == 0)
        {
            PlayerPrefs.SetInt(saveDataMoney[0], money);
            PlayerPrefs.SetInt(saveDataRent[0], rent);
        }
        if (SaveManager.Instance.slotUse == 1)
        {
            PlayerPrefs.SetInt(saveDataMoney[1], money);
            PlayerPrefs.SetInt(saveDataRent[2], rent);
        }
        if (SaveManager.Instance.slotUse == 2)
        {
            PlayerPrefs.SetInt(saveDataMoney[2], money);
            PlayerPrefs.SetInt(saveDataRent[2], rent);
        }
    }
}
