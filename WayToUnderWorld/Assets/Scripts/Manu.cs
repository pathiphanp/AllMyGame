using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Manu : MonoBehaviour
{
    CanvasGroup my;
    public AudioSource click;
    public Sound clickSound;
    // Start is called before the first frame update
    void Start()
    {
        my = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        click.PlayOneShot(clickSound.clip);
        my.alpha = 0.5f;
        SceneManager.LoadScene(1);
        my.alpha =1f;

    }

    public void ExisGame()
    {
        click.PlayOneShot(clickSound.clip);
        my.alpha = 0.5f;
        Application.Quit();
        my.alpha =1f;

    }
}
