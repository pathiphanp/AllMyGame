using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour, CancelSkill
{
    [SerializeField] Skill skill;
    Collider2D coll;
    [Header("Bubble")]
    [SerializeField] GameObject bubble;
    List<AddBubble> addBubbles = new List<AddBubble>();
    [Header("Bubble List")]
    [HideInInspector] public List<Bubble> bubbleList = new List<Bubble>();
    bool setbubble = true;
    bool startBubble = true;
    [Header("Player Combat")]
    PlayerCombat player;
    [Header("DataSkill")]
    [HideInInspector] public DataSkill dataSkill;
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
    void Awake()
    {
        coll = this.GetComponent<Collider2D>();
    }
    public void CastSkill(PlayerCombat _player)
    {
        if (setbubble)
        {
            setbubble = false;
            sfx.volume = AudioManager.Instance.sfxSource.volume;
            AudioManager.Instance.PlaySFXInObject(sfx, "InBubble");
            player = _player;
            coll.enabled = true;
            StartCoroutine(BubbleDuration());
        }
        if (!coll.enabled && startBubble)
        {
            startBubble = false;
            StartCoroutine(CallSpawnBubble());
        }
    }
    public void CancelSkill()
    {
        AudioManager.Instance.StopSFXInObject(sfx);
        foreach (Bubble b in bubbleList)
        {
            b.Die();
        }
        setbubble = true;
        startBubble = true;
        addBubbles.Clear();
        bubbleList.Clear();
        player.CancelCombo();
    }
    public void RemoveBubbleFromList(Bubble _bubble)
    {
        bubbleList.Remove(_bubble);
        _bubble.Die();
    }
    IEnumerator BubbleDuration()
    {
        yield return new WaitForSeconds(dataSkill.durationSkill);
        CancelSkill();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            addBubbles.Add(other.gameObject.GetComponent<AddBubble>());
            coll.enabled = false;
        }
    }
    IEnumerator CallSpawnBubble()
    {
        AddBubble[] rndBubble = new AddBubble[addBubbles.Count];
        for (int i = 0; i < addBubbles.Count; i++)
        {
            int rndIndex = Random.Range(0, addBubbles.Count);
            while (rndBubble[rndIndex] != null)
            {
                rndIndex = (rndIndex + 1) % addBubbles.Count;
            }
            rndBubble[rndIndex] = addBubbles[i];
        }
        for (int i = 0; i < addBubbles.Count; i++)
        {
            rndBubble[i].AddBubble(bubble, this);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
