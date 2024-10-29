using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class SkillObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public ControlUiSkill controlUiSkill;
    [SerializeField] float speedMove;
    public bool cancelSkill;
    public Skill skill;

    public void MoveToTarget(Vector3 _target, bool _end, bool _resetSelect)
    {
        _target.z = 0;
        StartCoroutine(CallStartMoveToTarget(_target, _end, _resetSelect));
    }
    IEnumerator CallStartMoveToTarget(Vector3 _target, bool _end, bool _resetSelect)
    {
        while (gameObject.transform.position != _target)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target, speedMove * Time.deltaTime);
            yield return true;
        }
        Vector3 newPosition = gameObject.GetComponent<RectTransform>().localPosition;
        newPosition.z = 0;
        gameObject.GetComponent<RectTransform>().localPosition = newPosition;
        if (_end)
        {
            controlUiSkill.CheckOffAllUiSkill(_resetSelect);
        }
        yield break;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (cancelSkill)
        {
            controlUiSkill.CancelSkill(true);
        }
        else
        {
            controlUiSkill.CancelSkill(false);
            ControlGamePlay._instance.OffMouseControl();
            ControlGamePlay._instance.PlayerAttack(skill);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("Show Skill info");
        if (skill != null)
        {
            skill.accuract = 100 - ControlGamePlay._instance.controlEnemy.DodgeChance(skill.effectSkill);
            controlUiSkill.ShowInfoSkill(skill.name, skill.damage.ToString(), skill.effectSkill.ToString(), skill.accuract.ToString(), skill.description);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("off Skill info");
        controlUiSkill.OffInfoSkill();
    }
}
