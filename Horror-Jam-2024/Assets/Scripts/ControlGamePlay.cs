using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TurnState
{
    PLAYER, ENEMY
}
public class ControlGamePlay : Singleton<ControlGamePlay>
{
    [HideInInspector] public ControlPlayer controlPlayer;
    [HideInInspector] public ControlEnemy controlEnemy;
    [HideInInspector] public ControlUI controlUI;
    ControlUiSkill controlUiSkill;

    [HideInInspector] public GameObject partPlayerSelect;

    [HideInInspector] public bool canSelectPart;
    [HideInInspector] public bool canShowPlayerStatus = true;


    [Header("Time Control")]
    [SerializeField] public float waitDescriptionGame;
    [SerializeField] public float waitEndAttack;

    [HideInInspector] public bool playerIsDie;
    [HideInInspector] public bool boosIsDie;
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
        yield return new WaitForSeconds(waitDescriptionGame);
        controlUI.cutScenes.SetActive(false);
        PlayerTurn();
    }

    //Player Turn
    void PlayerTurn()
    {
        if (!playerIsDie)
        {
            canShowPlayerStatus = true;
            controlUI.OffAllCamera();
            StartCoroutine(StartPlayerTurn());
        }
    }
    IEnumerator StartPlayerTurn()
    {
        controlPlayer.ReturnToIdel();
        controlPlayer.spriteRenderer.sortingLayerName = "Top";
        controlPlayer.CheckPart();
        controlUI.ShowUiTurn(true, TurnState.PLAYER.ToString() + " Turn");
        yield return new WaitForSeconds(1);
        controlUI.ShowUiTurn(false, "");
        yield return new WaitForSeconds(0.5f);
        if (!controlPlayer.isStun)
        {
            controlUI.playerUi.SetActive(true);
            _instance.OnMouseControl();
        }
        else
        {
            controlPlayer.isStun = false;
            PlayerEndTurn();
        }

    }
    public void PlayerDefendMode()
    {

        OffControlPlayer();
        StartCoroutine(DefendAction());
    }
    IEnumerator DefendAction()
    {
        controlPlayer.UseShield();
        controlPlayer.spriteRenderer.sprite = controlPlayer.skillDefend.spriteSkillHaveShield;
        yield return new WaitForSeconds(1.5f);
        PlayerEndTurn();
    }
    public void PlayerChoosePartMode()
    {
        canShowPlayerStatus = false;
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
    public void PlayerAttack(Skill _skill)
    {
        OffControlPlayer();
        StartCoroutine(AttackAction(_skill));
    }
    IEnumerator AttackAction(Skill _skill)
    {
        Sprite actionSprite = null;
        foreach (PartData pD in controlPlayer.parts)
        {
            if (pD.part.typePart[0] == TypePart.Shield && pD.canUsePart)
            {
                actionSprite = _skill.spriteSkillHaveShield;
            }
            else
            {
                actionSprite = _skill.spriteSkillNotHaveShield;
            }
        }
        yield return new WaitForSeconds(0.2f);
        controlUI.SetCinemachineBrainDefaultBlend(0.1f);
        controlUI.lookAtPlayerAttackCamera.SetActive(true);
        controlPlayer.spriteRenderer.sprite = actionSprite;
        controlPlayer.gameObject.transform.position = controlPlayer.positionAttack.transform.position;
        if (_skill.effectSkill == EffectSkill.DodgeShields)
        {
            EffectSkill _effskill = EffectSkill.None;
            for (int i = 0; i < 2; i++)
            {
                controlEnemy.TakeDamage(_skill.damage, _effskill, ControlGamePlay._instance.partPlayerSelect);
                _skill.damage /= 2;
                _effskill = EffectSkill.DodgeShields;
                yield return new WaitForSeconds(0.5f);
                StopCoroutine(controlUI.callShowDamage);
                controlUI.showDamage.SetActive(false);
                yield return new WaitForSeconds(0.25f);
            }
        }
        else
        {
            controlEnemy.TakeDamage(_skill.damage, _skill.effectSkill, ControlGamePlay._instance.partPlayerSelect);
        }
        yield return new WaitForSeconds(waitEndAttack);
        controlPlayer.ReturnToIdel();
        controlPlayer.transform.position = controlPlayer.positionIdel;
        controlUI.SetCinemachineBrainDefaultBlend(0.5f);
        PlayerEndTurn();
    }
    public void PlayerEndTurn()
    {
        controlEnemy.ReturnToIdel();
        controlPlayer.spriteRenderer.sortingLayerName = "Under";
        EnemyTurn();
    }
    public void PlayerDie()
    {
        //Game Over
        controlUI.OffAllCamera();
        playerIsDie = true;
        controlUI.enemyWin.SetActive(true);
        controlUI.gameOver.SetActive(true);
        OnMouseControl();
    }

    void OffControlPlayer()
    {
        OffMouseControl();
        canSelectPart = false;
        ControlGamePlay._instance.controlUI.attackBtn.gameObject.SetActive(false);
        ControlGamePlay._instance.controlUI.defendBtn.gameObject.SetActive(false);
    }
    /*----------------------------------------------------------------------------------------*/
    //Enemy
    //Player can't do everything
    void EnemyTurn()
    {
        if (!boosIsDie)
        {
            StartCoroutine(StartEnemyTurn());
        }
    }
    IEnumerator StartEnemyTurn()
    {
        controlUI.OffAllCamera();
        yield return new WaitForSeconds(0.5f);
        controlUI.ShowUiTurn(true, TurnState.ENEMY.ToString() + " Turn");
        yield return new WaitForSeconds(2f);
        controlUI.ShowUiTurn(false, "");
        controlEnemy.StartEnemyTurn();
    }
    public void EnemyAttack(int _damage, EffectSkill _effectSkill, PartData _partTarget)
    {
        controlEnemy.transform.position = controlEnemy.positionAttack.transform.position;
        controlUI.lookAtEnemyAttackCamera.SetActive(true);
        controlPlayer.TakeDamage(_damage, _effectSkill, _partTarget);
        StartCoroutine(EnemyEndTurn());
    }
    IEnumerator EnemyEndTurn()
    {
        yield return new WaitForSeconds(waitEndAttack);
        controlUI.OffAllCamera();
        controlEnemy.transform.position = controlEnemy.positionIdel;
        controlEnemy.ReturnToIdel();
        PlayerTurn();
    }
    /*----------------------------------------------------------------------------*/
    public void BoosDie()
    {
        //Player Win
        controlUI.OffAllCamera();
        boosIsDie = true;
        controlUI.playerWin.SetActive(true);
        controlUI.gameOver.SetActive(true);
        OnMouseControl();
    }
    /*----------------------------------------------------------------------------*/
    public void OnMouseControl()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void OffMouseControl()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
