using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkillWithMouse : MonoBehaviour
{
    [SerializeField] GameObject Skill;
    void Update()
    {

    }
    public void CastSkill()
    {
        SpawnSkill();
    }
    public void SpawnSkill()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = 0;
        Instantiate(Skill, mousePos, Skill.transform.rotation);
    }
}
