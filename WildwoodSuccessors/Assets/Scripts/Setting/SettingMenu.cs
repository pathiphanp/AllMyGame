using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System;
using Unity.VisualScripting;
using System.Net;

public class SettingMenu : MonoBehaviour
{
    //String check first time in game
    public string firstTimeCheck;
    //Screen
    public TMP_Dropdown resolutionDropdown;
    //public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    int[] width = { 1920, 1280, 640 };
    int[] height = { 1080, 720, 360 };
    //Audio
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private Slider sfxSlider;
    
    void Awake()
    {
        // //Check Player First time in game
        // firstTimeCheck = PlayerPrefs.GetString("FirstTimeCheck");
        // if (firstTimeCheck == null)
        // {
        //     firstTimeCheck = "a";
        //     //Set resolution
        //     resolutionDropdown.value = 0;
        //     Screen.SetResolution(width[resolutionDropdown.value], height[resolutionDropdown.value], Screen.fullScreen);
        //     PlayerPrefs.SetInt("ResolutionWidth", width[resolutionDropdown.value]);
        //     PlayerPrefs.SetInt("ResolutionHeight", height[resolutionDropdown.value]);

        //     //Audiuo
        //     myMixer.SetFloat("Music", 0.5f);
        //     myMixer.SetFloat("SFX", 0.5f);
        //     PlayerPrefs.SetFloat("MusicValue", 0.5f);
        //     PlayerPrefs.SetFloat("SfxValue", 0.5f);
        //     musicSlider.value = 0.5f;
        //     sfxSlider.value = 0.5f;

        //     //Set first time false
        //     PlayerPrefs.SetString("FirstTimeCheck", firstTimeCheck);
        //     PlayerPrefs.Save();

        //     Debug.Log("First time");
        // }
        // else
        // {
        //     //Screen/Resolution
        //     Screen.SetResolution(SaveObject.Instance.resolutionSaveWidth, SaveObject.Instance.resolutionSaveHeight, Screen.fullScreen);
        //     resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionSlot");

        //     //Audio
        //     myMixer.SetFloat("Music", Mathf.Log10(SaveObject.Instance.musicSaveSound) * 20 );
        //     myMixer.SetFloat("SFX", Mathf.Log10(SaveObject.Instance.sfxSaveSound) * 20 );
        //     musicSlider.value = PlayerPrefs.GetFloat("MusicValue");
        //     sfxSlider.value = PlayerPrefs.GetFloat("SfxValue");

        //     Debug.Log("Hello again");
        // }

        //Set UI auidio option

    }
    void Start()
    {
        AudioManager.Instance.PlayMusic("MainmenuTheme");
        myMixer.SetFloat("Music", Mathf.Log10(SaveManager.Instance.musicSaveSound) * 20 );
        myMixer.SetFloat("SFX", Mathf.Log10(SaveManager.Instance.sfxSaveSound) * 20 );

        musicSlider.value = PlayerPrefs.GetFloat("MusicValue");
        sfxSlider.value = PlayerPrefs.GetFloat("SfxValue");

        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionSlot");
    }

    #region Function Screen
    public void SetFullscreen(Toggle toggle)
    {
        Screen.fullScreen = toggle.isOn;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetResolutionReal()
    {
        Screen.SetResolution(width[resolutionDropdown.value], height[resolutionDropdown.value], Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionWidth", width[resolutionDropdown.value]);
        PlayerPrefs.SetInt("ResolutionHeight", height[resolutionDropdown.value]);
        PlayerPrefs.SetInt("ResolutionSlot", resolutionDropdown.value);
        PlayerPrefs.Save();

        SaveManager.Instance.resolutionSlot = PlayerPrefs.GetInt("ResolutionSlot");
        SaveManager.Instance.resolutionSaveWidth = PlayerPrefs.GetInt("ResolutionWidth");
        SaveManager.Instance.resolutionSaveHeight = PlayerPrefs.GetInt("ResolutionHeight");

    }
    #endregion

    #region Function Audio
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicValue", volume);
        PlayerPrefs.Save();

        SaveManager.Instance.musicSaveSound = PlayerPrefs.GetFloat("MusicValue");
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SfxValue", volume);
        PlayerPrefs.Save();

        SaveManager.Instance.sfxSaveSound = PlayerPrefs.GetFloat("SfxValue");
    }
    #endregion

    public void ResetJoinGame()
    {
        PlayerPrefs.SetString("FirstTimeCheck", "");
        Debug.Log("Reset");
    }
    //PlayerPref want 1. Sound Volume 2. Resolution.x & .y 3. Fullscreen

}
