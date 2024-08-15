using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameMap4 : MonoBehaviour
{
    [Header("PointHp")]
    [SerializeField] Image[] pointShow;
    [SerializeField] int indexPoint;

    [Header("Formula")]
    [SerializeField] Image[] formulaImage;

    [SerializeField] int formula;
    [SerializeField] int item;
    [SerializeField] int wigth;

    [SerializeField] int[] itemWight;
    [SerializeField] int indexItem;
    [SerializeField] int index;
    [SerializeField] int indexformula;

    [Header("PassGame")]
    [SerializeField] GameObject miniGameMap;
    [SerializeField] Collider2D doorMap3;
    [SerializeField] GameObject doorOpen;

    void Awake()
    {
        doorOpen = doorMap3.GetComponent<GameObject>();
    }
    void Start()
    {
        itemWight = new int[8];
    }
    void Update()
    {
        ChangeId();
        ResetFormola();
    }
    void ChangeId()
    {
        if (formula == 1)
        {
            formulaImage[0].gameObject.SetActive(true);
            if (item == 1)
            {
                Gamemanager.gamemanager.idItem = 4;
                wigth = 5;
            }
            else if (item == 2)
            {
                Gamemanager.gamemanager.idItem = 10;
                wigth = 12;
            }
            else if (item == 3)
            {
                Gamemanager.gamemanager.idItem = 8;
                wigth = 9;
            }
            else
            {
                index++;
            }
        }
        if (formula == 2)
        {
            formulaImage[1].gameObject.SetActive(true);
            if (item == 1)
            {
                Gamemanager.gamemanager.idItem = 6;
                wigth = 10;
            }
            else if (item == 2)
            {
                Gamemanager.gamemanager.idItem = 5;
                wigth = 50;
            }
            else if (item == 3)
            {
                Gamemanager.gamemanager.idItem = 1;
                wigth = 3;
            }
            else if (item == 4)
            {
                Gamemanager.gamemanager.idItem = 2;
                wigth = 3;
            }
            else if (item == 5)
            {
                Gamemanager.gamemanager.idItem = 3;
                wigth = 8;
            }
            else
            {
                index++;
            }
        }
        if (formula == 3)
        {
            formulaImage[2].gameObject.SetActive(true);
            if (item == 1)
            {
                Gamemanager.gamemanager.idItem = 5;
                wigth = 20;
            }
            else if (item == 2)
            {
                Gamemanager.gamemanager.idItem = 7;
                wigth = 15;
            }
            else if (item == 3)
            {
                Gamemanager.gamemanager.idItem = 9;
                wigth = 80;
            }
            else if (item == 4)
            {
                Gamemanager.gamemanager.idItem = 3;
                wigth = 4;
            }
            else if (item == 5)
            {
                Gamemanager.gamemanager.idItem = 4;
                wigth = 3;
            }
            else if (item == 6)
            {
                Gamemanager.gamemanager.idItem = 6;
                wigth = 5;
            }
            else if (item == 7)
            {
                Gamemanager.gamemanager.idItem = 10;
                wigth = 6;
            }
            else if (item == 8)
            {
                Gamemanager.gamemanager.idItem = 8;
                wigth = 12;
            }
            else
            {
                Gamemanager.gamemanager.pointGameOver = 3;
                Gamemanager.gamemanager.resetItemMap1 = true;
                Player.player.speedMove = Player.player.maxSpeedMove;
                Invoke("offAll", 0.1f);
                doorMap3.transform.localScale = new Vector3(-1, 1.35f, 1);
                doorMap3.enabled = false;
            }
        }
        if (Gamemanager.gamemanager.wightItem == wigth)
        {
            Gamemanager.gamemanager.wightItem = 0;
            item++;
        }
        if (index == indexformula)
        {
            item = 1;
            index = 0;
            formula++;
            AudioManager.Instance.PlaySFX("SucceedPot");
        }
    }


    void ResetFormola()
    {
        if (Gamemanager.gamemanager.resetFormola == true)
        {
            Gamemanager.gamemanager.resetFormola = false;
            Gamemanager.gamemanager.wightItem = 0;
            formulaImage[1].gameObject.SetActive(false);
            formulaImage[2].gameObject.SetActive(false);

            formula = 1;
            item = 1;
            for (int i = 0; i < 1; i++)
            {
                pointShow[indexPoint].color = Color.black;
                indexPoint++;
            }
            Gamemanager.gamemanager.pointGameOver--;
        }
    }

    void offAll()
    {
        miniGameMap.SetActive(false);
    }

}
