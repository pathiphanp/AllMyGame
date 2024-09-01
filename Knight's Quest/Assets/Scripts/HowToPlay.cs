using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] GameObject allHowToPlay;
    [SerializeField] VideoPlayer videoPlayerHowToPlay;
    [SerializeField] RawImage slotsVideo;
    [SerializeField] VideoClip videohowToSelect;
    [SerializeField] VideoClip videohowToAttack;
    [SerializeField] VideoClip videohowToButHint;
    [SerializeField] GameObject howToSelect;
    [SerializeField] GameObject howToAttack;
    [SerializeField] GameObject howToButHint;
    [SerializeField] GameObject repeated;
    [SerializeField] GameObject potionHeal;
    [SerializeField] GameObject potionBuffDamage;

    [SerializeField] GameObject AllBtn;
    [SerializeField] Button btnhowToSelect;
    [SerializeField] Button btnhowToAttack;
    [SerializeField] Button btnhowToButHint;
    [SerializeField] Button btnrepeated;
    [SerializeField] Button btnpotionHeal;
    [SerializeField] Button btnpotionBuffDamage;

    [SerializeField] Button btnOutHowToPlay;
    [SerializeField] Button btnOffHowToPlay;
    private void Start()
    {
        btnhowToSelect.onClick.AddListener(OnClickhowToSelect);
        btnhowToAttack.onClick.AddListener(OnClickhowToAttack);
        btnhowToButHint.onClick.AddListener(OnClickhowToButHint);
        btnrepeated.onClick.AddListener(OnClickrepeated);
        btnpotionHeal.onClick.AddListener(OnClickpotionHeal);
        btnpotionBuffDamage.onClick.AddListener(OnClickpotionBuffDamage);
        btnOutHowToPlay.onClick.AddListener(OnClickOutHowToPlay);
        btnOffHowToPlay.onClick.AddListener(OnClickOffHowToPlay);
    }

    private void OnClickOffHowToPlay()
    {
        OffAll();
        btnOutHowToPlay.gameObject.SetActive(true);
        AllBtn.SetActive(true);
        btnOffHowToPlay.gameObject.SetActive(false);
    }

    private void OnClickOutHowToPlay()
    {
        allHowToPlay.SetActive(false);
    }

    private void OnClickpotionBuffDamage()
    {
        OffAll();
        AllBtn.SetActive(false);
        btnOffHowToPlay.gameObject.SetActive(true);
        potionBuffDamage.SetActive(true);
    }

    private void OnClickpotionHeal()
    {
        OffAll();
        AllBtn.SetActive(false);
        btnOffHowToPlay.gameObject.SetActive(true);
        potionHeal.SetActive(true);
    }

    private void OnClickrepeated()
    {
        OffAll();
        AllBtn.SetActive(false);
        btnOffHowToPlay.gameObject.SetActive(true);
        repeated.SetActive(true);
    }

    private void OnClickhowToButHint()
    {
        OffAll();
        videoPlayerHowToPlay.clip = videohowToButHint;
        AllBtn.SetActive(false);
        btnOffHowToPlay.gameObject.SetActive(true);
        howToButHint.SetActive(true);
        slotsVideo.gameObject.SetActive(true);
    }

    private void OnClickhowToAttack()
    {
        OffAll();
        videoPlayerHowToPlay.clip = videohowToAttack;
        AllBtn.SetActive(false);
        btnOffHowToPlay.gameObject.SetActive(true);
        howToAttack.SetActive(true);
        slotsVideo.gameObject.SetActive(true);
    }

    private void OnClickhowToSelect()
    {
        OffAll();
        videoPlayerHowToPlay.clip = videohowToSelect;
        AllBtn.SetActive(false);
        btnOffHowToPlay.gameObject.SetActive(true);
        howToSelect.SetActive(true);
        slotsVideo.gameObject.SetActive(true);
    }

    void OffAll()
    {
        slotsVideo.gameObject.SetActive(false);
        howToSelect.SetActive(false);
        howToAttack.SetActive(false);
        howToButHint.SetActive(false);
        repeated.SetActive(false);
        potionHeal.SetActive(false);
        potionBuffDamage.SetActive(false);
    }

}
