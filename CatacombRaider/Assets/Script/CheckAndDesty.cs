using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckAndDesty : MonoBehaviour
{
    public Walk walk;
    public HP hP;
    public TextMeshProUGUI score;
    public TextMeshProUGUI scoreGameOver;
    public int scorePoint;
    public int shard;
    float allpoint;

    // Start is called before the first frame update
    void Start()
    {
        scoreGameOver.text = PlayerPrefs.GetFloat("PointGameOver").ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        allpoint = this.transform.position.z + scorePoint;
        PlayerPrefs.SetFloat("PointGameOver", allpoint);              
        score.text = (this.transform.position.z + scorePoint).ToString("0");       
    }

    private void OnTriggerEnter(Collider targetShard)
    {
        if(targetShard.gameObject.tag == "shard")
        {
            shard++;
            hP.HealHP(shard);
            scorePoint += 15;          
            Destroy(targetShard.gameObject);
        }
    }

    private void OnCollisionEnter(Collision wall)
    {
        if(wall.gameObject.tag == "wall")
        {
            walk.speed = 1;
            walk.timespeed = 5;
            hP.DestoyHp();
            Destroy(wall.gameObject);
        }
    }
}
