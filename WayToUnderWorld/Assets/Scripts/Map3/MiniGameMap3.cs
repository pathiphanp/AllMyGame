using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniGameMap3 : MonoBehaviour
{
    [SerializeField] GameObject[] allUIMap2;
    [SerializeField] CanvasGroup[] fadeUIMap2;
    [SerializeField] CanvasGroup backGround;
    [SerializeField] Image[] pointShow;
    [SerializeField] int indexPoint;

    [SerializeField] GameObject miniGameMap;
    Collider2D doorMap3;
    [SerializeField] GameObject door;
    [SerializeField] bool closeUI;
    void Awake()
    {
        doorMap3 = door.GetComponent<Collider2D>();
        allUIMap2 = GameObject.FindGameObjectsWithTag("UIMap2");
        fadeUIMap2 = new CanvasGroup[allUIMap2.Length];
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChackJigSaw();
    }
    //Chack endGame //Chack pointHp //ChackReGame

    void ChackJigSaw()
    {
        if (Gamemanager.gamemanager.countPassword == 16)
        {
            if (Gamemanager.gamemanager.pointPassword == 16)
            {
                AudioManager.Instance.PlaySFX("UnlockDoor");
                Gamemanager.gamemanager.pointGameOver = 3;
                Gamemanager.gamemanager.resetItemMap3 = true;
                Player.player.speedMove = Player.player.maxSpeedMove;
                closeUI = true;
                Invoke("offAll", 5f);
                doorMap3.transform.localScale = new Vector3(-1, 1.35f, 1);
                doorMap3.enabled = false;
            }
            else
            {
                AudioManager.Instance.PlaySFX("LockOpen");
                for (int i = 0; i < 1; i++)
                {
                    pointShow[indexPoint].color = Color.black;
                    indexPoint++;
                }
                Gamemanager.gamemanager.resetItemMap1 = true;
                Gamemanager.gamemanager.pointGameOver--;
            }
        }
        if (closeUI)
        {
            for (int i = 0; i < fadeUIMap2.Length; i++)
            {
                fadeUIMap2[i] = allUIMap2[i].GetComponent<CanvasGroup>();
                fadeUIMap2[i].alpha -= Time.deltaTime;
            }
            backGround.alpha = 1f;
        }

    }

    void offAll()
    {
        miniGameMap.SetActive(false);
    }


}
