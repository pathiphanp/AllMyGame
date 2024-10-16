using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    PLAYER, ENEMY
}
public class ControlGamePlay : Singleton<ControlGamePlay>
{
    [HideInInspector] public ControlPlayer controlPlayer;
    [HideInInspector] public ControlEnemy controlEnemy;
    ControlUI controlUI;
    ControlUiSkill controlUiSkill;

    public GameObject partPlayerSelect;

    public bool canSelectPart;
    public override void Awake()
    {
        base.Awake();
        controlPlayer = FindObjectOfType<ControlPlayer>();
        controlEnemy = FindObjectOfType<ControlEnemy>();
        controlUI = FindObjectOfType<ControlUI>();
        controlUiSkill = FindObjectOfType<ControlUiSkill>();
    }
    private void Start()
    {
        StartGamePlay();
    }

    void StartGamePlay()
    {
        StartCoroutine(CallStartGamePlay());
    }
    IEnumerator CallStartGamePlay()
    {
        OffMouseControl();
        controlUI.cutScenes.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        controlUI.cutScenes.SetActive(false);
        PlayerTurn();
    }

    //Player Turn
    void PlayerTurn()
    {
        StartCoroutine(StartPlayerTurn());
    }
    IEnumerator StartPlayerTurn()
    {
        controlUI.ShowUiTurn(true, TurnState.PLAYER.ToString() + " Turn");
        yield return new WaitForSeconds(1);
        controlUI.ShowUiTurn(false, "");
        yield return new WaitForSeconds(1);
        controlUI.playerUi.SetActive(true);
        OnMouseControl();
    }

    public void PlayerChoosePartMode()
    {
        controlUI.playerUi.SetActive(false);
        controlUI.lookAtEnemyCamera.SetActive(true);
        canSelectPart = true;
    }

    public void PlayerSelectPart(GameObject _part)
    {
        if (_part != null)
        {
            canSelectPart = false;
            partPlayerSelect = _part;
            controlUiSkill.ShowSkill(_part.transform.position);
        }
    }
    public void ShowSkillUi()
    {
        controlUI.playerUiSkill.SetActive(true);
    }
    /*----------------------------------------------------------------------------------------*/
    //Enemy
    //Player can't do everything
    void EnemyTurn()
    {
        StartCoroutine(StartEnemyTurn());
    }
    IEnumerator StartEnemyTurn()
    {
        controlUI.ShowUiTurn(true, TurnState.ENEMY.ToString() + " Turn");
        yield return new WaitForSeconds(0);
        OffMouseControl();
    }

    void EndTurn()
    {

    }

    void OnMouseControl()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void OffMouseControl()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
