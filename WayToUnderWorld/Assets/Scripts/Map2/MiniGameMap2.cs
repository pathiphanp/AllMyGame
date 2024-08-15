using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameMap2 : MonoBehaviour
{
    [SerializeField] Drop slotid;

    [Header("Hint")]
    [SerializeField] Image showHint;
    //[SerializeField] Image[] listHint;
    [SerializeField] Sprite[] listHint;
    int index;

    [Header("OffMinigame")]
    [SerializeField] GameObject miniGameMap;
    [SerializeField] GameObject doorMap2;
    Collider2D collDoorMap2;

    [Header("HpMap2")]
    [SerializeField] Image[] pointShow;
    int indexPoint = -1;
    [SerializeField] int lastpoint;
    void Awake()
    {
        collDoorMap2 = doorMap2.GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Gamemanager.gamemanager.notMatch = false;
        slotid.id = Gamemanager.gamemanager.slotId;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHint();
        PassMap2();
        ChackMatch();
    }

    void ChangeHint()
    {

        index = Gamemanager.gamemanager.pointPassword;
        if (Gamemanager.gamemanager.pointPassword > (listHint.Length - 1))
        {
            showHint.gameObject.SetActive(false);
        }
        else
        {
            showHint.sprite = listHint[index];
        }
    }

    void PassMap2()
    {
        //WIN
        if (Gamemanager.gamemanager.pointPassword == 5)
        {
            AudioManager.Instance.PlaySFX("UnlockDoor");
            Gamemanager.gamemanager.mapNum = 0;
            Gamemanager.gamemanager.pointGameOver = 3;
            Gamemanager.gamemanager.resetItemMap1 = true;
            Player.player.speedMove = Player.player.maxSpeedMove;
            Invoke("offAll", 0.1f);
            collDoorMap2.transform.localScale = new Vector3(-1, 1.35f, 1);
            collDoorMap2.enabled = false;
        }
        //Lose
        if (Gamemanager.gamemanager.notMatch == true)
        {
            for (int i = 0; i < 1; i++)
            {
                indexPoint++;
                pointShow[indexPoint].color = Color.black;
            }
            Gamemanager.gamemanager.resetItemMap2 = true;
            Gamemanager.gamemanager.pointGameOver--;
            Gamemanager.gamemanager.notMatch = false;
        }

    }

    void offAll()
    {
        miniGameMap.SetActive(false);
    }

    void ChackMatch()
    {
        if (Gamemanager.gamemanager.pointPassword > lastpoint)
        {
            Gamemanager.gamemanager.slotId++;
            slotid.id = Gamemanager.gamemanager.slotId;
            Gamemanager.gamemanager.offItem = true;
        }
        lastpoint = Gamemanager.gamemanager.pointPassword;
    }
}
