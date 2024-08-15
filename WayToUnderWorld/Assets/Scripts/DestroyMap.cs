using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMap : MonoBehaviour
{
    [SerializeField] GameObject blockBack;
    [SerializeField] GameObject map;
    [SerializeField] GameObject hintMap6;
    [SerializeField] float radius;
    [SerializeField] float fly;
    [SerializeField] LayerMask player;
    [SerializeField] int idmap;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }
    void Check()
    {
        if (Physics2D.OverlapCircle(transform.position, radius, player))
        {
            blockBack.SetActive(true);
            Destroy(map);
            if (idmap == 6)
            {
                hintMap6.SetActive(true);
                Invoke("StartMap6",2f);
            }
        }
    }
    void StartMap6()
    {
        Gamemanager.gamemanager.playerFly = fly;
    }
}
