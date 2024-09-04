using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlSetting : Singleton<ControlSetting>
{
    [Header("Volume Display")]
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [Header("Setting Display")]
    [SerializeField] int[,] resolution = new int[3, 2];
    [SerializeField] Button setResolutionLeft;
    [SerializeField] Button setResolutionRight;
    [SerializeField] int indexRasolution = 0;
    [SerializeField] TMP_Text textResolution;
    // Start is called before the first frame update
    void Start()
    {
        SetUpResolution();
        setResolutionLeft.onClick.AddListener(SettingResolutionDown);
        setResolutionRight.onClick.AddListener(SettingResolutionUp);
        sfxSlider.onValueChanged.AddListener(SFXSetting);
        musicSlider.onValueChanged.AddListener(MusicSetting);
    }
    //1024x576 | 1280x720 | 1920x1080
    void SetUpResolution()
    {
        resolution[0, 0] = 1024;
        resolution[0, 1] = 576;
        resolution[1, 0] = 1280;
        resolution[1, 1] = 720;
        resolution[2, 0] = 1920;
        resolution[2, 1] = 1080;
        Screen.SetResolution(resolution[2, 0], resolution[2, 1], Screen.fullScreenMode);
        textResolution.text = resolution[2, 0].ToString() + " x " + resolution[2, 1].ToString();
    }
    void SettingResolutionUp()
    {
        SettingResolution(+1);
    }
    void SettingResolutionDown()
    {
        SettingResolution(-1);
    }
    void SettingResolution(int _indexResolution)
    {
        indexRasolution += _indexResolution;
        if (indexRasolution > resolution.GetLength(0) - 1)
        {
            indexRasolution = 0;
        }
        else if (indexRasolution < 0)
        {
            indexRasolution = resolution.GetLength(0) - 1;
        }
        Screen.SetResolution(resolution[indexRasolution, 0], resolution[indexRasolution, 1], Screen.fullScreenMode);
        textResolution.text = resolution[indexRasolution, 0].ToString() + " x " + resolution[indexRasolution, 1].ToString();
    }
    void SFXSetting(float value)
    {
        AudioManager._instance.sfxSource.volume = value;
    }
    void MusicSetting(float value)
    {
        AudioManager._instance.musicSource.volume = value;
    }
}
