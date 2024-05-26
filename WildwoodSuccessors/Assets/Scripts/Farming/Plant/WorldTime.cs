using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float worldTime;
    public float speedWorldTime;
    void Update()
    {
        worldTime += speedWorldTime * Time.deltaTime;
    }
}
