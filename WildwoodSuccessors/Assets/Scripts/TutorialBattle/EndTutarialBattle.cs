using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTutarialBattle : MonoBehaviour
{
    [SerializeField] Button btnEndTutorail;
    void Start()
    {
        btnEndTutorail.onClick.AddListener(OnEndTutorail);
    }

    private void OnEndTutorail()
    {
        SceneManager.LoadScene(0);
    }
}
