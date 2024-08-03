using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamewaves : SkillWave
{
    void Start()
    {
        StartCoroutine(Move());
        AudioManager.Instance.PlaySFX("StartFireSpawn");
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
