using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Gamemanager : MonoBehaviour
{
    static public Gamemanager gamemanager;
    public bool onSlot;
    public int pointPassword;
    public int countPassword;

    public int pointGameOver;

    public bool onMiniGame;

    public bool resetItemMap1;
    public bool resetItemMap2;
    public bool resetItemMap3;

    [Header("Map2")]
    public bool notMatch;
    public bool offItem;
    public int slotId;

    [Header("Map4")]
    public int idItem;
    public int idlistItem;
    public float wightItem;
    public bool resetFormola;

    [Header("Map05")]
    public bool candrag;
    public int pointImageTrue;
    public bool unlock;

    [Header("Map06")]
    public float playerFly;


    [Header("MapNum")]
    public int mapNum;

    [Header("GameOver")]
    [SerializeField] GameObject gameOvar;
    [SerializeField] float delayOver;

    private void Awake()
    {
        gamemanager = this;
    }

    void Update()
    {
        GameOver();
    }

    void GameOver()
    {
        if (pointGameOver == 0)
        {
            pointGameOver  = 1;
            AudioManager.Instance.ToggleMusic();
            StartCoroutine(DelayLoadGame());
        }
    }

    IEnumerator DelayLoadGame()
    {
        gameOvar.SetActive(true);
        AudioManager.Instance.PlaySFX("LoseMiniGame");
        Gamemanager.gamemanager.playerFly = 0;
        yield return new WaitForSeconds(delayOver);
        SceneManager.LoadScene(1);
        while(true)
        {
            break;
        }
    }
}
