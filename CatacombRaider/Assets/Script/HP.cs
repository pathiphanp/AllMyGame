using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    public CheckAndDesty checkAndDesty;
    public CameraShake cameraShake;
    public Image[] hpimage;
    Color off;
    Color on;
    public int Hp;
    public float duration;
    public float magnitude;
    // Start is called before the first frame update
    void Start()
    {
        Hp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp == 0)
        {
            off = hpimage[0].color;
            off.a = 0;
            hpimage[0].color = off;
        }
        if (Hp == 1)
        {
            off = hpimage[1].color;
            off.a = 0;
            hpimage[1].color = off;
            on = hpimage[0].color;
            on.a = 100;
            hpimage[0].color = on;
        }
        if (Hp == 2)
        {
            off = hpimage[2].color;
            off.a = 0;
            hpimage[2].color = off;
            on = hpimage[1].color;
            on.a = 100;
            hpimage[1].color = on;
        }
        if (Hp == 3)
        {
            on = hpimage[2].color;
            on.a = 100;
            hpimage[2].color = on;
        }
    }

    public void HealHP(int shard)
    {
        if(shard  == 15)
        {
            Hp++;
            if(Hp > 3)
            {
                Hp = 3;
            }
            checkAndDesty.shard = 0; 
        }
    } 
    public void DestoyHp()
    {
        if(Hp <= 0)
        {
            Invoke("Restart", 1f);
        }     
        StartCoroutine(cameraShake.Shake(duration, magnitude));
        Hp--;
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene("Gameover");
    }
}
