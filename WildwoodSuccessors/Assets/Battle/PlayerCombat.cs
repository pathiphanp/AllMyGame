using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static PlayerCombatInputt;

enum ModeAttack
{
    NORMAIL, CASTSPELL
}
public enum Element
{
    FIRE, WATER, WIND, EARTH
}
public enum TypesSkill
{
    NONE, DPS, RND, NORMAL, KNOCKBACK, STUN
}
public class PlayerCombat : MonoBehaviour, ICombatModeActions, TakedamageS
{
    public PlayerCombatInputt playerCombatInput;
    [SerializeField] ModeAttack modeAttack;
    #region ComboSkill
    [Header("ComboSkill")]
    List<Combo> comboList = new List<Combo>();
    bool oneClick;
    bool holdClick;
    [SerializeField] bool holdSkill;
    bool canCallSpell;
    #endregion
    #region Icon
    [Header("SlotIconMouse")]
    [SerializeField] GameObject slotMouse;
    [SerializeField] GameObject[] twoSlotIcon;
    [SerializeField] GameObject[] threeSlotIcon;
    [SerializeField] GameObject[] fourSlotIcon;
    [Header("IconMouse")]
    [SerializeField] Combo[] comboMouse;
    [SerializeField] GameObject bgIconCombo;
    Coroutine resetCombo;
    [Header("Delay")]
    [SerializeField] float delayRestartIcon;
    [SerializeField] float _delayRestartIcon;
    #endregion
    #region Skill
    #region Element
    [Header("Element")]
    [SerializeField] public Element element;
    bool canChangeElement = true;
    #endregion
    [Header("Skill")]
    #region Fire Skill
    [Header("FireSkill")]
    [Header("Flamethorwer")]
    [SerializeField] public Flamethrower flamethrower;
    [Header("Meteor")]
    [SerializeField] SpawnSkill[] meteor;
    [Header("FlameWaves")]
    [SerializeField] SpawnSkill flamewaves;
    [Header("BallFire")]
    [SerializeField] SpawnSkill ballFire;
    [Header("ShurikenFlame")]
    [SerializeField] SpawnSkill spawnShurikenFlame;
    ShurikenFlame shurikenFlame;
    #endregion
    #region Water Skill
    [Header("Water Skill")]
    [SerializeField] Rain rain;
    [SerializeField] SpawnSkill votexWaves;
    [SerializeField] WaterWaves waterWaves;
    [SerializeField] SpawnSkill waterBoom;
    [SerializeField] BubbleSpawn bubbleSpawn;
    #endregion
    #region Wind Skill
    [Header("Wind Skill")]
    [SerializeField] SpawnSpearWind[] spearWind;
    [SerializeField] SpawnSkill vacuumBomb;
    [SerializeField] GustWind gustWind;
    [SerializeField] SpawnSkill windSlash;
    [SerializeField] Tornado tornado;
    #endregion
    #region Earth Skill
    [Header("Earth Skill")]
    [SerializeField] EarthSpike spike;
    [SerializeField] SpawnSkill mountain;
    //Knockback
    [SerializeField] SpawnSkill rock;
    [SerializeField] EarthWall earthWall;
    [SerializeField] Eartquake earthquake;

    #endregion
    CancelSkill cancelSkill;
    List<CancelSkill> cancelSkillsList = new List<CancelSkill>();
    TypesSkill cancelTypesSkill;
    #endregion
    #region Cooldown Skill
    [Header("Cooldown Skill")]
    [Header("Dps")]
    [SerializeField] float dpsCooldown;
    [SerializeField] Image showCooldownDps;
    bool skillDpsCooldown;
    [Header("Rnd")]
    [SerializeField] float rndCooldown;
    [SerializeField] Image showCooldownRnd;
    bool skillRndCooldown;
    [Header("Normal")]
    [SerializeField] float normalCooldown;
    [SerializeField] Image showCooldownNormal;
    [SerializeField] bool skillNormalCooldown;
    [Header("Knockback")]
    [SerializeField] float knockbackCooldown;
    [SerializeField] Image showCooldownKnockback;
    bool skillKnockbackCooldown;
    [Header("Stun")]
    [SerializeField] float stunCooldown;
    [SerializeField] Image showCooldownStun;
    bool skillStunCooldown;
    [Header("Crystal")]
    [SerializeField] CrystalControl crystalControl;
    [Header("Cooldown")]
    #region CooldownSetting
    float realTime;
    #endregion
    #endregion
    #region Warning
    [Header("Warning")]
    [SerializeField] GameObject failNotification;
    Coroutine callFailNotification;
    [SerializeField] GameObject warning;
    [SerializeField] float delayWarningUp;
    bool canWarning = true;
    #endregion
    [Header("Animation")]
    [SerializeField] Animator dadAnimation;
    #region Tower
    [Header("Tower")]
    [SerializeField] float maxTowerHp;
    float towerHp;
    [SerializeField] Slider towerHpSlider;
    #endregion

    void OnEnable()
    {
        playerCombatInput.CombatMode.Enable();
    }
    void OnDisable()
    {
        playerCombatInput.CombatMode.Disable();
    }
    void Awake()
    {
        playerCombatInput = new PlayerCombatInputt();
        playerCombatInput.CombatMode.SetCallbacks(this);
    }
    void Start()
    {
        SetHpTower();
        _delayRestartIcon = delayRestartIcon;
        // GameManagerGameplay.Instance.inputInCombat = this;
    }
    void Update()
    {
        CastSkill();
    }
    void FixedUpdate()
    {
        realTime = Time.time;
    }
    public void OnChangeMode(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (modeAttack == ModeAttack.NORMAIL && canChangeElement)
            {
                modeAttack = ModeAttack.CASTSPELL;
                crystalControl.ringSprite.gameObject.SetActive(true);
                bgIconCombo.SetActive(true);
            }
            else
            {
                modeAttack = ModeAttack.NORMAIL;
                crystalControl.ringSprite.gameObject.SetActive(false);
            }
        }
    }
    public void OnElementFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ChangeElement(Element.FIRE);
        }
    }
    public void OnElementWater(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ChangeElement(Element.WATER);
        }
    }
    public void OnElementWind(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ChangeElement(Element.WIND);
        }
    }
    public void OnElementEarth(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ChangeElement(Element.EARTH);
        }
    }
    public void OnComboSkillLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            oneClick = true;
        }
        else if (context.performed)
        {
            // Debug.Log("Hold Click left");
            oneClick = false;
            holdSkill = true;
            SpawnIconCombo(1);
        }
        if (context.canceled && oneClick)
        {
            //Debug.Log("Click left");
            if (modeAttack == ModeAttack.CASTSPELL)
            {
                SpawnIconCombo(0);
            }
            else
            {
                CastNormalAttack();
            }
        }
        if (context.canceled)
        {
            holdSkill = false;
        }
    }
    public void OnComboSkillRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            oneClick = true;
        }
        else if (context.performed)
        {
            oneClick = false;
            holdSkill = true;
            SpawnIconCombo(3);
        }
        if (context.canceled && oneClick)
        {
            //Debug.Log("Click Right");
            holdSkill = false;
            SpawnIconCombo(2);
        }
        if (context.canceled)
        {
            holdSkill = false;
        }
    }
    void SpawnIconCombo(int numMouse)
    {
        if (modeAttack == ModeAttack.CASTSPELL)
        {
            canChangeElement = false;
            if (comboList.Count < 4)
            {
                if (resetCombo != null)
                {
                    StopCoroutine(resetCombo);
                }
                GameObject icon = Instantiate(comboMouse[numMouse].iconCombo, slotMouse.transform);
                Combo newCombo = new Combo();
                newCombo.nameCombo = comboMouse[numMouse].nameCombo;
                newCombo.iconCombo = icon;
                comboList.Add(newCombo);
                if (comboList.Count == 1)
                {
                    comboList[0].iconCombo.transform.localPosition = Vector3.zero;
                }
                if (comboList.Count == 2)
                {
                    for (int i = 0; i < comboList.Count; i++)
                    {
                        comboList[i].iconCombo.transform.localPosition = twoSlotIcon[i].transform.localPosition;
                    }
                }
                if (comboList.Count == 3)
                {
                    for (int i = 0; i < comboList.Count; i++)
                    {
                        comboList[i].iconCombo.transform.localPosition = threeSlotIcon[i].transform.localPosition;
                    }
                }
                if (comboList.Count == 4)
                {
                    for (int i = 0; i < comboList.Count; i++)
                    {
                        comboList[i].iconCombo.transform.localPosition = fourSlotIcon[i].transform.localPosition;
                    }
                }
                CheckCombo();
            }
        }
    }
    void CheckCombo()
    {
        if (comboList.Count == 4)
        {
            //Call spell
            canCallSpell = true;
        }
        else
        {
            //Wait restart combo
            resetCombo = StartCoroutine(RestartCombo());
        }
    }
    IEnumerator RestartCombo()
    {
        //Reset combo list if add combo not full
        if (comboList.Count < 3)
        {
            _delayRestartIcon *= 0.5f;
        }
        else
        {
            _delayRestartIcon = delayRestartIcon;
        }
        yield return new WaitForSeconds(delayRestartIcon);
        StartCoroutine(DestroyListCombo());
    }
    public void CancelCombo()
    {
        modeAttack = ModeAttack.NORMAIL;
        bgIconCombo.SetActive(false);
        canChangeElement = true;
        foreach (Combo i in comboList)
        {
            Destroy(i.iconCombo.gameObject);
        }
        holdSkill = false;
    }
    public IEnumerator DestroyListCombo()
    {
        crystalControl.ringSprite.gameObject.SetActive(false);
        cancelTypesSkill = TypesSkill.NONE;
        if (comboList.Count > 4)
        {
            if (callFailNotification != null)
            {
                StopCoroutine(callFailNotification);
            }
            callFailNotification = StartCoroutine(FailComboNotification());
        }
        canCallSpell = false;
        //Rest AllCombolist 
        for (int i = 0; i < comboList.Count; i++)
        {
            Destroy(comboList[i].iconCombo.gameObject);
        }
        comboList.Clear();
        cancelSkill = null;
        cancelSkillsList.Clear();
        canChangeElement = true;
        modeAttack = ModeAttack.NORMAIL;
        yield return true;
    }
    IEnumerator FailComboNotification()
    {
        failNotification.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        failNotification.SetActive(false);
    }
    void ChangeElement(Element _element)
    {
        if (canChangeElement)
        {
            if (_element == Element.FIRE)
            {
                element = Element.FIRE;
            }
            if (_element == Element.WATER)
            {
                element = Element.WATER;
            }
            if (_element == Element.WIND)
            {
                element = Element.WIND;
            }
            if (_element == Element.EARTH)
            {
                element = Element.EARTH;
            }
            crystalControl.ChangeCrystalColor(element);
        }
    }
    void CastSkillAnimation()
    {
        dadAnimation.Play("CastSpell");
    }
    void CastNormalAttack()
    {
        //NormalAttack Skill
        if (!skillNormalCooldown)
        {
            CastSkillAnimation();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            //Check Element
            if (element == Element.FIRE)
            {
                ballFire.CastSkill(mousePos);
            }
            else if (element == Element.WATER)
            {
                waterBoom.CastSkill(mousePos);
            }
            else if (element == Element.WIND)
            {
                windSlash.CastSkill(mousePos);
            }
            else if (element == Element.EARTH)
            {
                rock.CastSkill(mousePos);
            }
            StartCoroutine(StartCooldownSkill(normalCooldown, TypesSkill.NORMAL));
        }
    }
    void CastSkill()
    {
        if (canCallSpell)
        {
            //DPS Skill
            if (comboList[0].nameCombo == "RightClick" && comboList[1].nameCombo == "RightClick" &&
            comboList[2].nameCombo == "RightClick" && comboList[3].nameCombo == "HoldRightClick" && holdSkill)
            {
                CastSkillAnimation();
                //Check Element
                if (!skillDpsCooldown)
                {
                    cancelTypesSkill = TypesSkill.DPS;
                    if (element == Element.FIRE)
                    {
                        flamethrower.CastSkill(this);
                        cancelSkill = flamethrower.GetComponent<CancelSkill>();
                    }
                    else if (element == Element.WATER)
                    {
                        rain.CastSkill(this);
                        cancelSkill = rain.GetComponent<CancelSkill>();
                    }
                    else if (element == Element.WIND)
                    {
                        foreach (SpawnSpearWind w in spearWind)
                        {
                            w.CastSkill(this);
                            cancelSkillsList.Add(w.GetComponent<CancelSkill>());
                        }
                    }
                    else if (element == Element.EARTH)
                    {
                        spike.CastSpell(this);
                        cancelSkill = spike.GetComponent<CancelSkill>();
                    }
                }
                else
                {
                    WarningSkillCooldown();
                }
            }
            //RandomAttack Skill
            else if (comboList[0].nameCombo == "LeftClick" && comboList[1].nameCombo == "RightClick" &&
               comboList[2].nameCombo == "LeftClick" && comboList[3].nameCombo == "RightClick")
            {
                CastSkillAnimation();
                if (!skillRndCooldown)
                {
                    //Check Element
                    if (element == Element.FIRE)
                    {
                        foreach (SpawnSkill m in meteor)
                        {
                            m.CastSkill(Vector3.zero);
                        }
                    }
                    else if (element == Element.WATER)
                    {
                        votexWaves.CastSkill(Vector3.zero);
                    }
                    else if (element == Element.WIND)
                    {
                        vacuumBomb.CastSkill(Vector3.zero);
                    }
                    else if (element == Element.EARTH)
                    {
                        mountain.CastSkill(Vector3.zero);
                    }
                    StartCoroutine(DestroyListCombo());
                    StartCoroutine(StartCooldownSkill(rndCooldown, TypesSkill.RND));
                }
                else
                {
                    WarningSkillCooldown();
                }
                StartCoroutine(DestroyListCombo());
            }
            //KnockBack Skill
            else if (comboList[0].nameCombo == "RightClick" && comboList[1].nameCombo == "LeftClick" &&
               comboList[2].nameCombo == "RightClick" && comboList[3].nameCombo == "LeftClick")
            {
                CastSkillAnimation();
                if (!skillKnockbackCooldown)
                {
                    //Check Element
                    if (element == Element.FIRE)
                    {
                        flamewaves.CastSkill(flamewaves.transform.localPosition);
                    }
                    else if (element == Element.WATER)
                    {
                        waterWaves.CastSkill();
                    }
                    else if (element == Element.WIND)
                    {
                        gustWind.CastSkill();
                    }
                    else if (element == Element.EARTH)
                    {
                        earthWall.CastSkill();
                    }
                    StartCoroutine(StartCooldownSkill(knockbackCooldown, TypesSkill.KNOCKBACK));
                }
                else
                {
                    WarningSkillCooldown();
                }
                StartCoroutine(DestroyListCombo());
            }
            //Stun Skill
            else if (comboList[0].nameCombo == "LeftClick" && comboList[1].nameCombo == "LeftClick" &&
               comboList[2].nameCombo == "LeftClick" && comboList[3].nameCombo == "HoldLeftClick" && holdSkill)
            {
                CastSkillAnimation();
                //Check Element
                if (!skillStunCooldown)
                {
                    cancelTypesSkill = TypesSkill.STUN;
                    if (element == Element.FIRE)
                    {
                        // Debug.Log("กรงจักไฟ");
                        if (shurikenFlame == null)
                        {
                            spawnShurikenFlame.CastSkill(Vector3.zero);
                            if (spawnShurikenFlame.instan != null)
                            {
                                shurikenFlame = spawnShurikenFlame.instan.GetComponent<ShurikenFlame>();
                                shurikenFlame.playerCombat = this;
                            }
                        }
                        else
                        {
                            shurikenFlame.CastSpell();
                            cancelSkill = shurikenFlame.GetComponent<CancelSkill>();
                        }
                    }
                    else if (element == Element.WATER)
                    {
                        bubbleSpawn.CastSkill(this);
                        cancelSkill = bubbleSpawn.GetComponent<CancelSkill>();
                    }
                    else if (element == Element.WIND)
                    {
                        tornado.CastSkill(this);
                        cancelSkill = tornado.GetComponent<CancelSkill>();
                    }
                    else if (element == Element.EARTH)
                    {
                        earthquake.CastSkill(this);
                        cancelSkill = earthquake.GetComponent<CancelSkill>();
                    }
                }
                else
                {
                    WarningSkillCooldown();
                }
            }
            //Not use Skill
            else
            {
                //เมื่อไม่กดสกิลจะสั่งให้สกิลล่าสุดที่ใช้รีเซ็ต
                if (cancelSkill != null)
                {
                    cancelSkill.CancelSkill();
                }
                if (cancelSkillsList.Count > 0)
                {
                    foreach (CancelSkill c in cancelSkillsList)
                    {
                        c.CancelSkill();
                    }
                }
                if (cancelTypesSkill == TypesSkill.DPS)
                {
                    StartCoroutine(StartCooldownSkill(dpsCooldown, TypesSkill.DPS));
                }
                else if (cancelTypesSkill == TypesSkill.STUN)
                {
                    StartCoroutine(StartCooldownSkill(stunCooldown, TypesSkill.STUN));
                }
                if (comboList.Count > 0)
                {
                    StartCoroutine(DestroyListCombo());
                }
            }
        }
    }
    public IEnumerator StartCooldownSkill(float duration, TypesSkill typeSkill)
    {
        if (typeSkill == TypesSkill.DPS)
        {
            skillDpsCooldown = true;
            StartCoroutine(ShowCooldown(showCooldownDps, duration));
            yield return new WaitForSeconds(duration);
            skillDpsCooldown = false;
        }
        if (typeSkill == TypesSkill.RND)
        {
            skillRndCooldown = true;
            StartCoroutine(ShowCooldown(showCooldownRnd, duration));
            yield return new WaitForSeconds(duration);
            skillRndCooldown = false;
        }
        if (typeSkill == TypesSkill.NORMAL)
        {
            skillNormalCooldown = true;
            StartCoroutine(ShowCooldown(showCooldownNormal, duration));
            yield return new WaitForSeconds(duration);
            skillNormalCooldown = false;
        }
        if (typeSkill == TypesSkill.KNOCKBACK)
        {
            skillKnockbackCooldown = true;
            StartCoroutine(ShowCooldown(showCooldownKnockback, duration));
            yield return new WaitForSeconds(duration);
            skillKnockbackCooldown = false;
        }
        if (typeSkill == TypesSkill.STUN)
        {
            skillStunCooldown = true;
            StartCoroutine(ShowCooldown(showCooldownStun, duration));
            yield return new WaitForSeconds(duration);
            skillStunCooldown = false;
        }
    }
    IEnumerator ShowCooldown(Image showTypesCooldown, float tartgetTime)
    {
        float durationCooldown = 0;
        float timeCooldown = 0;
        float targetCooldown = 0;
        float use = 0;
        //SetfillAmount SetDurationCooldown
        showTypesCooldown.fillAmount = 1;
        float _dataTarget = realTime + tartgetTime;
        targetCooldown = _dataTarget;
        timeCooldown = targetCooldown - realTime;
        bool cooldown = false;
        while (!cooldown)
        {
            //Start Cooldown
            durationCooldown = targetCooldown - realTime;
            use = durationCooldown / timeCooldown;
            showTypesCooldown.fillAmount = use;
            //Check Cooldown Complete
            if (realTime >= targetCooldown)
            {
                timeCooldown = 0;
                showTypesCooldown.fillAmount = 0;
                cooldown = true;
            }
            yield return true;
        }
    }
    public void WarningSkillCooldown()
    {
        if (canWarning)
        {
            //Debug.Log("Skill Cooldown");
            canWarning = false;
            StartCoroutine(DelayWarning());
        }
    }
    IEnumerator DelayWarning()
    {
        if (comboList.Count > 0)
        {
            StartCoroutine(DestroyListCombo());
        }
        //Debug.Log("Skill Cooldown");
        warning.SetActive(true);
        yield return new WaitForSeconds(delayWarningUp);
        warning.SetActive(false);
        canWarning = true;
    }
    public void SetHpTower()
    {
        towerHpSlider.maxValue = maxTowerHp;
        towerHpSlider.value = maxTowerHp;
        towerHp = maxTowerHp;
    }
    public GameObject Takedamage(int damage, GameObject returnTarget)
    {
        towerHp -= damage;
        towerHpSlider.value = towerHp;
        if (towerHp <= 0)
        {
            StopAllCoroutines();
            Application.Quit();
        }
        return returnTarget;
    }
}
