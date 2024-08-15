using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public GameObject setting;
    bool open;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!Gamemanager.gamemanager.onMiniGame)
            {
                open = !open;
                setting.SetActive(open);
            }
        }
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    public void Menu()
    {
        setting.SetActive(false);
        SceneManager.LoadScene("MenuStart");
    }
}
