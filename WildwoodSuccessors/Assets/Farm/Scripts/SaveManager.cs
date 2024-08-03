using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    public GameObject saveObj;
    bool prefabSuccess;
    [Header("New game")]
    public bool isNewGame;

    //Save local path
    string localPath;

    [Header("Slot")]
    public int slotUse;
    public string[] nameSave = new string[3];
    public int[] daySave = new int[3];
    public int[] moneySave = new int[3];

    [Header("Relolution in game save")]
    public int resolutionSaveWidth;
    public int resolutionSaveHeight;
    public int resolutionSlot;

    [Header("Audio in game save")]
    public float sfxSaveSound;
    public float musicSaveSound;

    void Start()
    {
        LoadSlotPlayer();
    }

    void Update()
    {
        slotUse = PlayerPrefs.GetInt("SlotInt");
    }

    void SaveSlotPlayer()
    {

    }
    public void LoadSlotPlayer()
    {
        //Get save Name
        nameSave[0] = PlayerPrefs.GetString("Slot01");
        nameSave[1] = PlayerPrefs.GetString("Slot02");
        nameSave[2] = PlayerPrefs.GetString("Slot03");
        //Get save Day
        daySave[0] = PlayerPrefs.GetInt("Player1DaySave");
        daySave[1] = PlayerPrefs.GetInt("Player2DaySave");
        daySave[2] = PlayerPrefs.GetInt("Player3DaySave");
        //Get save Money
        moneySave[0] = PlayerPrefs.GetInt("Player1MoneySave");
        moneySave[1] = PlayerPrefs.GetInt("Player2MoneySave");
        moneySave[2] = PlayerPrefs.GetInt("Player3MoneySave");
        //Get resolution
        resolutionSaveWidth = PlayerPrefs.GetInt("ResolutionWidth");
        resolutionSaveHeight = PlayerPrefs.GetInt("ResolutionHeight");
        resolutionSlot = PlayerPrefs.GetInt("ResolutionSlot");

        slotUse = PlayerPrefs.GetInt("SlotInt");

        //Get sound
        sfxSaveSound = PlayerPrefs.GetFloat("SfxValue");
        musicSaveSound = PlayerPrefs.GetFloat("MusicValue");

    }
}
