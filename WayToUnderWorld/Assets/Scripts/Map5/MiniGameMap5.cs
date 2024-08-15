using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameMap5 : MonoBehaviour
{
    [SerializeField] CanvasGroup[] mycanvas;
    [SerializeField] GameObject[] circle;
    [SerializeField] GameObject keymap5;
    [SerializeField] GameObject btn;
    [SerializeField] Image[] hp;
    [SerializeField] int indexPoint;
    [SerializeField] GameObject doorMap5;
    Collider2D doorOpen;

    [SerializeField] bool canCheck;
    void Awake()
    {
        doorOpen = doorMap5.GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        circle = GameObject.FindGameObjectsWithTag("Circle");
        mycanvas = new CanvasGroup[circle.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (canCheck)
        {
            CheckEndGame();
        }
    }
    public void CheckEndGame()
    {
        if (!Gamemanager.gamemanager.unlock)
        {
            for (int i = 0; i < circle.Length; i++)
            {
                mycanvas[i] = circle[i].GetComponent<CanvasGroup>();
                mycanvas[i].alpha -= Time.deltaTime;
            }
        }
        if (Gamemanager.gamemanager.pointImageTrue == 5 && Gamemanager.gamemanager.unlock)
        {
            Gamemanager.gamemanager.unlock = false;
            Gamemanager.gamemanager.pointGameOver = 3;
            AudioManager.Instance.PlaySFX("UnlockDoor");
            Invoke("offAll", 5f);
            doorOpen.transform.localScale = new Vector3(-1, 1.35f, 1);
            doorOpen.enabled = false;
        }
        else if(Gamemanager.gamemanager.pointImageTrue != 5 && Gamemanager.gamemanager.unlock)
        {
            AudioManager.Instance.PlaySFX("LockOpen");
            canCheck = false;
            for (int i = 0; i < 1; i++)
            {
                indexPoint++;
                hp[indexPoint].color = Color.black;
            }
            Gamemanager.gamemanager.resetItemMap1 = true;
            Gamemanager.gamemanager.pointGameOver--;
        }


    }
    void offAll()
    {
        keymap5.SetActive(false);
        Player.player.speedMove = Player.player.maxSpeedMove;
    }

    public void BtnCanCheck()
    {
        canCheck = true;
    }
}
