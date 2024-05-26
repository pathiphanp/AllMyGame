using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EarthSpike : MonoBehaviour, CancelSkill
{
    [SerializeField] Skill skill;
    [Header("spike")]
    [SerializeField] GameObject spikePrefab;
    List<GameObject> spikeList = new List<GameObject>();
    [Header("Wave")]
    [SerializeField] float waveMoveNum;
    [SerializeField] float distanceMove;
    [SerializeField] float delayWaveMove;
    bool onCast = false;
    PlayerCombat player;
    DataSkill dataSkill;
    [Header("Sound")]
    [SerializeField] AudioSource sfx;

    void OnEnable()
    {
        SkillManager.UpdateDataSKill += UpdateDataSkill;
    }
    void OnDisable()
    {
        SkillManager.UpdateDataSKill -= UpdateDataSkill;
    }
    void UpdateDataSkill()
    {
        dataSkill = SkillManager.Instance.PullDataSkill(skill);
    }

    public void CastSpell(PlayerCombat _player)
    {
        if (!onCast)
        {
            onCast = true;
            AudioManager.Instance.PlaySFXInObject(sfx, "Spike");
            player = _player;
            StartCoroutine(SpawnSpike());
        }
    }
    IEnumerator SpawnSpike()
    {
        StartCoroutine(WaveSpike());
        yield return new WaitForSeconds(dataSkill.delaySpawnSkill);
    }
    public void CancelSkill()
    {
        AudioManager.Instance.StopSFXInObject(sfx);
        StopAllCoroutines();
        foreach (GameObject sp in spikeList)
        {
            Destroy(sp.gameObject);
        }
        player.CancelCombo();
        onCast = false;
    }
    IEnumerator WaveSpike()
    {
        for (int i = 0; i < dataSkill.durationSkill; i++)
        {
            float _waveMove = 0;
            for (int w = 0; w < waveMoveNum; w++)
            {
                GameObject _spike = Instantiate(spikePrefab, transform);
                spikeList.Add(_spike);
                _spike.GetComponent<Collider2D>().enabled = true;
                Vector3 moveD = new Vector3(_waveMove, 0, 0);
                _spike.transform.position += moveD;
                yield return new WaitForSeconds(delayWaveMove);
                _spike.GetComponent<Collider2D>().enabled = false;
                _waveMove += distanceMove;
            }
        }
        CancelSkill();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Takedamage enemyT = other.GetComponent<Takedamage>();
            enemyT.Takedamage(dataSkill.damage, dataSkill.damageGuard, dataSkill.element,dataSkill);
            AddStun enemyS = other.GetComponent<AddStun>();
            enemyS.AddStun(dataSkill.durationStun, true);
        }
    }
}
