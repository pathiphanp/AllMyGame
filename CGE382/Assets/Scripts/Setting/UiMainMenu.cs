using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UiMainMenu : MonoBehaviour
{
    public TMP_InputField inputNameSave;
    public GameObject[] titleMenuObjs;
    public int slotNo;
    public GameObject checkWantNewGame;
    [SerializeField] GameObject audioManager;

    [SerializeField] ControlSlotSave[] controlSlotSaves;

    void OnEnable()
    {
        SaveManager.Instance.LoadSlotPlayer();
        GetData();
        Debug.Log("a");
    }
    public void BtnEnable(Button b)
    {
        b.interactable = true;
    }
    public void BtnDisable(Button b)
    {
        b.interactable = false;
    }

    public void ShowDisplay(GameObject g)
    {
        g.SetActive(true);
    }

    public void DisableDispla(GameObject g)
    {
        g.SetActive(false);
    }

    public void Exitgame()
    {
        Application.Quit();
    }

    public void SetTitleObjOff(bool t)
    {
        foreach (GameObject obj in titleMenuObjs)
        {
            obj.SetActive(t);
        }
    }

    public void NewGame(bool n)
    {
        SaveManager.Instance.isNewGame = n;
    }

    public void GetSaveName()
    {
        if (slotNo == 0)
        {
            SaveManager.Instance.nameSave[slotNo] = inputNameSave.text;
            PlayerPrefs.SetString("Slot01", SaveManager.Instance.nameSave[slotNo]);
            PlayerPrefs.SetString("daySlot01", 0.ToString());
            PlayerPrefs.SetString("moneySlot01", 0.ToString());
            PlayerPrefs.Save();
            inputNameSave.text = "";
            SceneManager.LoadScene(1);
        }

        if (slotNo == 1)
        {
            SaveManager.Instance.nameSave[slotNo] = inputNameSave.text;
            PlayerPrefs.SetString("Slot02", SaveManager.Instance.nameSave[slotNo]);
            PlayerPrefs.SetString("daySlot02", 0.ToString());
            PlayerPrefs.SetString("moneySlot02", 0.ToString());
            PlayerPrefs.Save();
            inputNameSave.text = "";
            SceneManager.LoadScene(1);
        }

        if (slotNo == 2)
        {
            SaveManager.Instance.nameSave[slotNo] = inputNameSave.text;
            PlayerPrefs.SetString("Slot03", SaveManager.Instance.nameSave[slotNo]);
            PlayerPrefs.SetString("daySlot02", 0.ToString());
            PlayerPrefs.SetString("moneySlot02", 0.ToString());
            PlayerPrefs.Save();
            inputNameSave.text = "";
            SceneManager.LoadScene(1);
        }
        if (audioManager != null)
        {
            Destroy(audioManager.gameObject);
        }
    }

    public void GetSlotNumber(int i)
    {
        slotNo = i;
        PlayerPrefs.SetInt("SlotInt", i);
    }

    public void SaveLoadCheckNewGame(Button b)
    {
        if (SaveManager.Instance.nameSave[slotNo] != "" && SaveManager.Instance.isNewGame)
        {
            b.interactable = false;
            checkWantNewGame.SetActive(true);
        }
        else
        {
            b.interactable = true;
        }
    }

    public void SaveLoadCheckNewGame(GameObject obj)
    {
        if (SaveManager.Instance.nameSave[slotNo] == "")
        {
            obj.SetActive(true);
        }
    }

    public void LoadGame()
    {
        if (!SaveManager.Instance.isNewGame)
        {
            SceneManager.LoadScene(1);
            if (audioManager != null)
            {
                Destroy(audioManager.gameObject);
            }
        }

    }

    public void ResetPlayerPref()
    {
        PlayerPrefs.SetString("Slot01", "");
        PlayerPrefs.SetString("Slot02", "");
        PlayerPrefs.SetString("Slot03", "");
    }

    public void GetData()
    {
        for (int i = 0; i < SaveManager.Instance.nameSave.Length; i++)
        {
            if (SaveManager.Instance.nameSave[i] != "")
            {
                controlSlotSaves[i].newGameTitle.SetActive(false);
                controlSlotSaves[i].nameTitle.SetActive(true);
                controlSlotSaves[i].textNamePlayer.text = SaveManager.Instance.nameSave[i];
                controlSlotSaves[i].dayTitle.SetActive(true);
                controlSlotSaves[i].textDay.text = SaveManager.Instance.daySave[i].ToString();
                controlSlotSaves[i].moneyitle.SetActive(true);
                controlSlotSaves[i].textMoney.text = SaveManager.Instance.moneySave[i].ToString();
            }
        }
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene(2);
        if (audioManager != null)
        {
            Destroy(audioManager.gameObject);
        }
    }
}
