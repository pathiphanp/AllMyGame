using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnmyRandom : MonoBehaviour
{
    public GameObject player;

    public int num;
    public float timeSpawn;

    public GameObject[] wall;
    public GameObject wallNew;

    public float spawnEnmy;
    Vector3 z = new Vector3(0, 0, 25);
    // Start is called before the first frame update
    void Start()
    {
        num = Random.Range(0, 9);
        wallNew = Instantiate(wall[num], new Vector3(wall[num].transform.position.x, wall[num].transform.position.y, wallNew.transform.position.z) + z, wall[num].transform.rotation);
        wallNew.transform.position = wall[num].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position,wallNew.transform.position);
        if (dist < spawnEnmy)
        {
            StartCoroutine(spawnEnemy());
        }
    }

    IEnumerator spawnEnemy()
    {        
        num = Random.Range(0, 9);
        wallNew = Instantiate(wall[num], new Vector3(wall[num].transform.position.x, wall[num].transform.position.y, wallNew.transform.position.z) + z, wall[num].transform.rotation);
        yield return null;
    }



}
