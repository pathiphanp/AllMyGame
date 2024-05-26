using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingGamePlay : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private Slider sfxSlider;

    [Header("Resolution")]
    //Resolution
    int[] width = { 1920, 1280, 640 };
    int[] height = { 1080, 720, 360 };
    public TMP_Dropdown resolutionDropdown;
    // Start is called before the first frame update
    void Start()
    {
        myMixer.SetFloat("Music", Mathf.Log10(SaveManager.Instance.musicSaveSound) * 20 );
        myMixer.SetFloat("SFX", Mathf.Log10(SaveManager.Instance.sfxSaveSound) * 20 );

        musicSlider.value = PlayerPrefs.GetFloat("MusicValue");
        sfxSlider.value = PlayerPrefs.GetFloat("SfxValue");

        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionSlot");
    }

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

    public void SetFullscreen(Toggle toggle)
    {
        Screen.fullScreen = toggle.isOn;
    }
    #endregion
}
