using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlUI : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] public GameObject lookAtEnemyCamera;
    [SerializeField] public GameObject lookAtPlayerStatusCamera;

    [Header("CutScenes")]
    [SerializeField] public GameObject cutScenes;

    [Header("UI Turn")]
    [SerializeField] GameObject showTurn;
    [SerializeField] TMP_Text textShowTurn;
    [Header("Player")]
    [Header("UI Player Status")]
    [SerializeField] GameObject showHpPlayer;
    [SerializeField] TMP_Text hpHeadText;
    [SerializeField] TMP_Text hpArmLeftText;
    [SerializeField] TMP_Text hpArmRightText;
    [SerializeField] TMP_Text hpBodyText;
    [SerializeField] TMP_Text hpLegLeftText;
    [SerializeField] TMP_Text hpLegRightText;
    [Header("BtnAction")]
    [SerializeField] public GameObject playerUi;
    [SerializeField] Button testAttack;
    [SerializeField] public GameObject playerUiSkill;
    [Header("Axe")]
    [SerializeField] Button axeSlashBtn;
    [SerializeField] Button axeDoubleSlashBtn;
    [SerializeField] Button axeBreaktheshieldBtn;
    [Header("BigSword")]
    [SerializeField] Button DargonBreathBtn;
    [SerializeField] Button DargonBiteBtn;
    [Header("DargonArm")]

    [Header("Shield")]
    [SerializeField] Button defendBtn;

    private void Start()
    {
        testAttack.onClick.AddListener(ControlGamePlay._instance.PlayerChoosePartMode);
    }

    public void ShowUiTurn(bool _status, string _Description)
    {
        textShowTurn.text = _Description;
        showTurn.SetActive(_status);
    }



}
