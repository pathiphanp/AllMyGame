using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSkipToNight : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManagerPor.Instance.timeCycle.isTimeSkip = true;
    }
}
