using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public interface Takedamage
{
    void Takedamage(int damage, int damageShield, Element element,DataSkill dataSkill);
}
public interface TakedamageS
{
    GameObject Takedamage(int damage, GameObject returnTarget);
}
public interface AddSlow
{
    void AddSlow(float durationSlow, bool add, float speed);
}
public interface AddPull
{
    void AddPull(GameObject target, float _pullspeed, float _durationPull, bool addKnockback,
    Vector2 knockbackDiraction, float knockbackForce, float durationknockbackForce);
    void AddPull(GameObject target, float _pullspeed, float _durationPull);
}
public interface CancelPulled
{
    // void CancelWasPulled();
}
public interface AddKnockback
{
    void AddKnockback(Vector2 knockbackDiraction, float knockbackForce, float durationknockbackForce);
}

public interface CancelSkill
{
    void CancelSkill();
}

public interface AddDataSkill
{
    void AddDataSkill(DataSkill _dataSkill);
}

public interface AddBubble
{
    void AddBubble(GameObject bubble, BubbleSpawn bubbleSpawn);
    void CancelBubble();
}
public interface AddStun
{
    void AddStun(float duration, bool playKnockUp);
}


