using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseHoldTutorial : MonoBehaviour
{
    [SerializeField] GameObject videoTutorial;
    [SerializeField] ControlTutorialBattle controlTutorialBattle;
    [SerializeField] PlayerCombat playerCombat;
    void OnMouseOver()
    {
        controlTutorialBattle.ChangeSkillVideo(playerCombat.element);
        videoTutorial.SetActive(true);
        Time.timeScale = 0;
    }
    void OnMouseExit()
    {
        videoTutorial.SetActive(false);
        Time.timeScale = 1;
    }
}
