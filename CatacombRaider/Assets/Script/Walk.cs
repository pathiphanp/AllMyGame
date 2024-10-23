using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Walk : MonoBehaviour
{
    Rigidbody rb;
    public GameObject player;
    public GameObject floor;
    public float zForwd;
    public float zFloorForwd;
    public int speed;
    public float timespeed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(speed == 1)
        {
            timespeed -= Time.deltaTime;
            if (timespeed <= 0)
            {
                speed = 8;
            }
        }
        floor.transform.position = new Vector3(5, 0, player.transform.position.z + zFloorForwd);
        rb.AddForce(0, 0, zForwd * Time.deltaTime / speed, ForceMode.VelocityChange);
        //CanMove();

    }
    void CanMove()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, zForwd * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -zForwd * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

}
