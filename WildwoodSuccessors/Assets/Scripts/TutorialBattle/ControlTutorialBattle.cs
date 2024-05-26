using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ControlTutorialBattle : MonoBehaviour
{
    [SerializeField] VideoClip[] skillClip;
    [SerializeField] VideoPlayer controlVideo;
    public void ChangeSkillVideo(Element element)
    {
        if(element == Element.FIRE)
        {
            controlVideo.clip = skillClip[0];
        }
        if(element == Element.WATER)
        {
            controlVideo.clip = skillClip[1];
        }
        if(element == Element.WIND)
        {
            controlVideo.clip = skillClip[2];
        }
        if(element == Element.EARTH)
        {
            controlVideo.clip = skillClip[3];
        }
    }
}
