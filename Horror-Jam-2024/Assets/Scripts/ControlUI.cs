using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class ControlUI : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] public CinemachineBrain cinemachineBrain;
    CinemachineBlendDefinition.Style blendStyle = CinemachineBlendDefinition.Style.EaseInOut;
    [SerializeField] public GameObject lookAtEnemyCamera;
    [SerializeField] public GameObject lookAtPlayerStatusCamera;
    [SerializeField] public GameObject lookAtPlayerAttackCamera;
    [SerializeField] public GameObject lookAtEnemyAttackCamera;

    [Header("CutScenes")]
    [SerializeField] public GameObject cutScenes;
    [SerializeField] public Button btnSkip;

    [Header("UI Turn")]
    [SerializeField] GameObject showTurn;
    [SerializeField] TMP_Text textShowTurn;
    [Header("Player")]
    [Header("UI Player Status")]
    [SerializeField] GameObject showHpPlayer;
    [SerializeField] TMP_Text[] playerStatusText;
    [Header("BtnAction")]
    [SerializeField] public GameObject playerUi;
    [SerializeField] public Button attackBtn;
    [SerializeField] public Button defendBtn;
    [Header("ShowDmage")]
    [SerializeField] public GameObject showDamage;
    [SerializeField] TMP_Text[] showDamageText;
    public Coroutine callShowDamage;

    [Header("GameOver")]
    [SerializeField] public GameObject gameOver;
    [SerializeField] public GameObject playerWin;
    [SerializeField] public GameObject enemyWin;
    [SerializeField] Button resetGame;

    private void Start()
    {
        attackBtn.onClick.AddListener(ControlGamePlay._instance.PlayerChoosePartMode);
        defendBtn.onClick.AddListener(ControlGamePlay._instance.PlayerDefendMode);
        resetGame.onClick.AddListener(ControlGamePlay._instance.ResetGame);
        btnSkip.onClick.AddListener(ControlGamePlay._instance.EndCutScenes);
    }

    public void ShowUiTurn(bool _status, string _Description)
    {
        textShowTurn.text = _Description;
        showTurn.SetActive(_status);
    }

    public void SetCinemachineBrainDefaultBlend(float blendDuration)
    {
        CinemachineBlendDefinition defaultBlend = new CinemachineBlendDefinition();
        defaultBlend.m_Style = blendStyle;  // รูปแบบ : EaseInOut, Cut, etc.
        defaultBlend.m_Time = blendDuration;  // ระยะเวลาการ Blend
        cinemachineBrain.m_DefaultBlend = defaultBlend;
    }

    public void OffAllCamera()
    {
        lookAtEnemyCamera.SetActive(false);
        lookAtPlayerAttackCamera.SetActive(false);
        lookAtPlayerStatusCamera.SetActive(false);
        lookAtEnemyAttackCamera.SetActive(false);
    }
    public void ShowDamageUI(int _damage, string _nameParts)
    {
        callShowDamage = StartCoroutine(StartShowDamageUI(_damage, _nameParts));
    }
    IEnumerator StartShowDamageUI(int _damage, string _nameParts)
    {
        string report = "";
        if (_damage == 0)
        {
            report = "Can dodge";
        }
        else
        {
            report = _damage.ToString() + " To " + _nameParts;
        }
        foreach (TMP_Text d in showDamageText)
        {
            d.text = report;
        }
        showDamage.SetActive(true);
        yield return new WaitForSeconds(ControlGamePlay._instance.waitEndAttack);
        showDamage.SetActive(false);
    }

    public void ShowPlayerStatus()
    {
        for (int i = 0; i < ControlGamePlay._instance.controlPlayer.parts.Length; i++)
        {
            playerStatusText[i].text = ControlGamePlay._instance.controlPlayer.parts[i].hpPart.ToString();
        }
    }
    public void SetShowPlayerStatus(bool onOff)
    {
        playerUi.SetActive(!onOff);
        lookAtPlayerStatusCamera.SetActive(onOff);
        showHpPlayer.SetActive(onOff);
    }
}
