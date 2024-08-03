using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManger : MonoBehaviour
{
    [SerializeField] Button[] btnClickSound;
    [SerializeField] Button btnMorningSound;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < btnClickSound.Length; i++)
        {
            btnClickSound[i].onClick.AddListener(OnClickSound);
        }
        btnMorningSound.onClick.AddListener(OnMorningSound);
    }

    private void OnMorningSound()
    {
        
    }

    private void OnClickSound()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
