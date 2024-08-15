using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MiniGameMap1 : MonoBehaviour
{
    [SerializeField] GameObject keyMap1;
    [SerializeField] GameObject gameOvar;
    [SerializeField] GameObject doorOpen;
    Collider2D door;

    [SerializeField] int maxPointGameOver;

    [SerializeField] Image[] pointShow;
    [SerializeField] int indexPoint;
    private void Awake() 
    {
        door = doorOpen.GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Gamemanager.gamemanager.pointGameOver = maxPointGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        PassGame();
    }
    void PassGame()
    {
        if (Gamemanager.gamemanager.countPassword == 4)
        {
            if (Gamemanager.gamemanager.pointPassword == 4)
            {
                AudioManager.Instance.PlaySFX("UnlockDoor");
                Gamemanager.gamemanager.mapNum++;
                Gamemanager.gamemanager.pointGameOver = 3;
                Gamemanager.gamemanager.resetItemMap1 = true;
                Player.player.speedMove = Player.player.maxSpeedMove;
                Invoke("offAll",0.1f);
                door.transform.localScale = new Vector3(-1,1.35f,1);
                door.enabled = false;
            }
            else
            {
                AudioManager.Instance.PlaySFX("LockOpen");
                for(int i = 0;i < 1;i++)
                {
                    indexPoint++;
                    pointShow[indexPoint].color = Color.black;
                }
                Gamemanager.gamemanager.resetItemMap1 = true;
                Gamemanager.gamemanager.pointGameOver--;
            }
        }
    }

    void offAll()
    {
        keyMap1.SetActive(false);
    }
}
