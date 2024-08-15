using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PassKey : MonoBehaviour
{
    [SerializeField] GameObject passText;
    [SerializeField] GameObject passKey;
    [SerializeField] float radius;
    [SerializeField] LayerMask player;
    [SerializeField] LayerMask nullLayer;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChackPlayer();
    }

    void ChackPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, radius, player))
        {
            Gamemanager.gamemanager.onMiniGame = true;
            passText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                passKey.gameObject.SetActive(true);
                Player.player.speedMove = 0;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                passKey.gameObject.SetActive(false);
                Gamemanager.gamemanager.resetItemMap1 = true;
                Player.player.speedMove = Player.player.maxSpeedMove;

            }
        }
        else if(Physics2D.OverlapCircle(transform.position, radius,nullLayer))
        {
            passText.gameObject.SetActive(false);
            Gamemanager.gamemanager.onMiniGame = false;
        }
    }

}
