using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSeen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Howtoplay()
    {
        SceneManager.LoadScene("Howtoplay");
    }
    public void Gameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ExitGaame()
    {
        Application.Quit();
    }
}
