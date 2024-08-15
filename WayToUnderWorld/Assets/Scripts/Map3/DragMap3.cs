using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragMap3 : Drag
{
    public GameObject fullJigSaw;

    public override void NotOnSlot()
    {
        base.NotOnSlot();
        fullJigSaw.SetActive(false);
    }

    public override void ResetPositionMap1()
    {
        base.ResetPositionMap1();
        fullJigSaw.SetActive(false);
    }
    
}
