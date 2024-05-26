using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationFarming : MonoBehaviour
{
    public bool haveAction;
    public GameObject notification;

    private void Update()
    {
        if(haveAction)
        {
            notification.SetActive(true);
        }
        else
        {
            notification.SetActive(false);
        }
    }
}
