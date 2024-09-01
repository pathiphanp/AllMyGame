using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ControlGamePlay : Singleton<ControlGamePlay>
{
    [HideInInspector] public ControlBoxLetters controlBoxLetters;
    [HideInInspector] public DataVocabularyManager dataVocabularyManager;
    [HideInInspector] public ControlSpawnMoster controlSpawnMoster;
    [HideInInspector] public ControlEnemyInfo controlEnemyInfo;
    [HideInInspector] public ControlScenes controlScenes;
    [HideInInspector] public ControlUpgrade controlUpgrade;

    [Header("Player")]
    [HideInInspector] public ControlPlayer controlPlayer;
    [HideInInspector] public bool canAtk = false;
    [Header("Button")]
    [SerializeField] Button btnResetLetter;
    [SerializeField] Button btnAttack;
    [SerializeField] Button btnHealPotion;
    [SerializeField] Button btnBuffDamagePotion;
    [SerializeField] Button btnReady;
    [SerializeField] Button btnRestGame;
    [SerializeField] Button btnHint;
    [SerializeField] TMP_Text hint;
    [SerializeField] Button btnResetGameOver;
    [SerializeField] Button btnOutGame;
    [SerializeField] Button btnHowToPlay;
    [Header("UI")]
    [SerializeField] public GameObject allUiInGamePlay;
    [SerializeField] public GameObject uiInGamePlay;
    [SerializeField] public GameObject uiBoxLetters;
    [SerializeField] public GameObject optionInGamePlay;
    [SerializeField] public GameObject uiGamePlay;
    [SerializeField] public GameObject uiUpgrade;
    [SerializeField] public GameObject uigameOver;
    [SerializeField] public GameObject uiHowToPlay;

    [Header("UI Warning")]
    [SerializeField] public GameObject warningHaveBuffDamage;
    Coroutine checkShowWarning;

    [Header("Monster")]
    [HideInInspector] public Monster monster;
    [Header("Text Report")]
    [SerializeField] TMP_Text textReportIngame;
    [SerializeField] TMP_Text textReportGameOver;
    [SerializeField] public GameObject textIntrolStartGame;

    private void Start()
    {
        AudioManager._instance.PlayMusic("BGSound");
        btnResetLetter.onClick.AddListener(OnClickResetBoxLetterAll);
        btnAttack.onClick.AddListener(Attack);
        btnHealPotion.onClick.AddListener(OnClickUseHealPotion);
        btnBuffDamagePotion.onClick.AddListener(OnClickUseBuffDamagePotion);
        btnHint.onClick.AddListener(OnClickBuyHint);
        btnReady.onClick.AddListener(OnClickReadyToPlay);
        btnRestGame.onClick.AddListener(OnClickResetGame);
        btnResetGameOver.onClick.AddListener(OnClickRestGameOver);
        btnOutGame.onClick.AddListener(OnClickOutGame);
        btnHowToPlay.onClick.AddListener(OnClickHowToPlay);
    }

    private void OnClickHowToPlay()
    {
        uiHowToPlay.SetActive(true);
    }

    private void OnClickRestGameOver()
    {
        uigameOver.SetActive(false);
        textReportGameOver.ClearMesh();
        OnClickResetGame();
    }
    private void OnClickOutGame()
    {
        Application.Quit();
    }

    private void OnClickResetGame()
    {
        textReportIngame.text = "";
        if (monster != null)
        {
            Destroy(monster.gameObject);
        }
        controlSpawnMoster.ResetGamePlay();
        controlScenes.ResetGamePlay();
        allUiInGamePlay.SetActive(false);
        uiUpgrade.SetActive(false);
        dataVocabularyManager.ResetGamePlay();
        controlBoxLetters.ResetGamePlay();
        controlPlayer.ResetPlayer();
        controlUpgrade.ResetGamePlay();
    }

    private void OnClickReadyToPlay()
    {
        SetReadyToPlay();
    }
    public void SetReadyToPlay()
    {
        OffMouseControl();
        allUiInGamePlay.SetActive(false);
        uiUpgrade.SetActive(false);
        uiGamePlay.SetActive(true);
        controlSpawnMoster.SpawMonster();
    }
    private void OnClickBuyHint()
    {
        if (controlPlayer.myCrystal >= 300)
        {
            controlPlayer.myCrystal -= 300;
            controlPlayer.RestMyCrystal();
            hint.text = controlBoxLetters.Hint();
        }
    }

    private void OnClickUseBuffDamagePotion()
    {
        controlPlayer.UseBuffDamagePotion();
    }
    private void OnClickUseHealPotion()
    {
        controlPlayer.UseHealPotion();
    }
    void Attack()
    {
        if (canAtk)
        {
            canAtk = false;
            hint.text = "Hint \n 300";
            OffMouseControl();
            StartCoroutine(StartPlayerAttack());
        }
    }
    IEnumerator StartPlayerAttack()
    {
        SetUpUIGamePlay(false);
        yield return new WaitForSeconds(0.5f);
        controlPlayer.Attack();
        controlBoxLetters.AttackVocabulary(monster, textReportIngame, dataVocabularyManager);
    }
    public void EnemyTurn()
    {
        StartCoroutine(StartEnemyTurn());
    }
    IEnumerator StartEnemyTurn()
    {
        yield return new WaitForSeconds(0.5f);
        monster.MosterTurn();
    }

    public void PlayerTurn()
    {
        SetUpUIGamePlay(true);
        OnMouseControl();
    }

    void OnClickResetBoxLetterAll()
    {
        if (controlBoxLetters.boxLettersOnVocabulary.Count == 0)
        {
            AudioManager._instance.PlaySFX("ResetBox");
            ResetBoxLetterAll();
            monster.CountSkillMonster();
        }

    }
    public void ResetBoxLetterAll()
    {
        controlBoxLetters.ResetLettersAll();
    }
    public void StartGamePlay()
    {
        OnMouseControl();
        SetUpUIGamePlay(true);
        allUiInGamePlay.SetActive(true);
    }

    public void SetUpUIGamePlay(bool _setUp)
    {
        uiInGamePlay.SetActive(_setUp);
        uiBoxLetters.SetActive(_setUp);
    }

    public void BreakTurn()
    {
        OnMouseControl();
        uiInGamePlay.SetActive(true);
        uiGamePlay.SetActive(false);
        uiUpgrade.SetActive(true);
    }

    public void StartWarning(GameObject _warning)
    {
        if (checkShowWarning != null)
        {
            StopCoroutine(checkShowWarning);
            StartCoroutine(WarningShow(_warning));
        }
        else
        {
            StartCoroutine(WarningShow(_warning));
        }
    }
    IEnumerator WarningShow(GameObject _warning)
    {
        _warning.SetActive(true);
        yield return new WaitForSeconds(1f);
        _warning.SetActive(false);
    }
    void OffMouseControl()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnMouseControl()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameOver()
    {
        OnMouseControl();
        allUiInGamePlay.SetActive(false);
        textReportGameOver.text = textReportIngame.text;
        uigameOver.SetActive(true);
    }
}
