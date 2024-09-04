using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControlPlayer : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] ControlUIPlayer controlUIPlayer;

    [Header("Status")]
    [SerializeField] public int maxHp;
    [SerializeField] Slider hpSlider;
    [SerializeField] int damage;
    bool isBuffDamage = false;
    [SerializeField] public int myCrystal;
    [SerializeField] public int chancePointLettersUp;
    [SerializeField] public int healPotion;
    [SerializeField] public int buffdamagePotion;
    private void Start()
    {
        ControlGamePlay._instance.controlPlayer = this;
        MoveToGame();
        SetPlayer();
        ResetAllUiStatus();
    }
    void SetPlayer()
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
    }
    public void Attack()
    {
        anim.Play("Attack");
        AudioManager._instance.PlaySFX("Hit");
    }
    public void Idel()
    {
        anim.Play("Idel");
        anim.Play("Noting");
    }
    public void MoveToGame()
    {
        anim.Play("Run");
        anim.Play("StartGame");
    }
    public void StartGame()
    {
        Idel();
        StartCoroutine(IntrolGame());
    }
    public void EnemyTurn()
    {
        ControlGamePlay._instance.EnemyTurn();
    }
    IEnumerator IntrolGame()
    {
        ControlGamePlay._instance.textIntrolStartGame.SetActive(true);
        yield return new WaitForSeconds(2);
        ControlGamePlay._instance.textIntrolStartGame.SetActive(false);
        ControlGamePlay._instance.controlBoxLetters.SpawnAllBoxLetters();
        ControlGamePlay._instance.controlSpawnMoster.SpawMonster();
    }
    public void RunAnim()
    {
        anim.Play("Run");
    }
    public void TakeDamage(int _damage)
    {
        hpSlider.value -= _damage;
        if (hpSlider.value <= 0)
        {
            Debug.Log("Die");
            ControlGamePlay._instance.GameOver();
        }
        else
        {
            controlUIPlayer.hpText.text = hpSlider.value.ToString();
            anim.Play("Hurt");
        }
    }
    public void PlayerTurn()
    {
        ControlGamePlay._instance.PlayerTurn();
    }

    public void AddHealPotion()
    {
        healPotion++;
        //Set UI
        controlUIPlayer.SetHealPotion(healPotion);
    }
    public void UseHealPotion()
    {
        if (healPotion > 0)
        {
            hpSlider.value += hpSlider.maxValue * 0.25f;
            healPotion--;
            AudioManager._instance.PlaySFX("UsePotion");
            controlUIPlayer.SetHealPotion(healPotion);
            ResetAllUiStatus();
        }
    }

    public void AddBuffDamagePotion()
    {
        buffdamagePotion++;
        //Set UI
        controlUIPlayer.SetBuffDamagePotion(buffdamagePotion);
    }
    public void UseBuffDamagePotion()
    {
        if (buffdamagePotion > 0)
        {
            if (!isBuffDamage)
            {
                isBuffDamage = true;
                buffdamagePotion--;
                AudioManager._instance.PlaySFX("UsePotion");
                controlUIPlayer.SetBuffDamagePotion(buffdamagePotion);
            }
            else
            {
                // ControlGamePlay._instance.StartWarning(ControlGamePlay._instance.warningHaveBuffDamage);
            }
        }
    }
    public void UpgradeSkill(TypeUpgradeSkill _typeUpgrade)
    {
        if (_typeUpgrade == TypeUpgradeSkill.MAXHP)
        {
            hpSlider.maxValue += 50;
            controlUIPlayer.maxHPText.text = hpSlider.maxValue.ToString();
        }
        if (_typeUpgrade == TypeUpgradeSkill.DAMAGE)
        {
            damage += 5;
            controlUIPlayer.damageText.text = damage.ToString();
        }
        if (_typeUpgrade == TypeUpgradeSkill.CHANCEPOINTLETTERSUP)
        {
            chancePointLettersUp += 1;
            controlUIPlayer.chancePointLettersUpText.text = chancePointLettersUp.ToString();
        }

    }
    public int Damage()
    {
        int _damage = damage;
        if (isBuffDamage)
        {
            isBuffDamage = false;
            _damage *= 2;
        }
        return _damage;
    }
    public void AddMyCrystal(int _crystal)
    {
        myCrystal += _crystal;
        controlUIPlayer.ResetPlayerCrystal();
    }
    public void RestMyCrystal()
    {
        controlUIPlayer.ResetPlayerCrystal();
    }

    void ResetAllUiStatus()
    {
        controlUIPlayer.maxHPText.text = hpSlider.maxValue.ToString();
        controlUIPlayer.hpText.text = hpSlider.value.ToString();
        controlUIPlayer.damageText.text = damage.ToString();
        controlUIPlayer.chancePointLettersUpText.text = chancePointLettersUp.ToString();
        controlUIPlayer.ResetPlayerCrystal();
        controlUIPlayer.SetHealPotion(healPotion);
        controlUIPlayer.SetBuffDamagePotion(buffdamagePotion);
    }

    public void ResetPlayer()
    {
        SetPlayer();
        damage = 10;
        isBuffDamage = false;
        myCrystal = 0;
        chancePointLettersUp = 0;
        healPotion = 0;
        buffdamagePotion = 0;
        ResetAllUiStatus();
        MoveToGame();
    }
}
