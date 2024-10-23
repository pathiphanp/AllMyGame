using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public HP hP;
    public int numStagePlayer;
    public int numStageGo;
    public float num;
    public float speed;

    public bool dashStageKey_A = false;
    public bool dashStageKey_D = false;
    // Start is called before the first frame update
    void Start()
    {
        Stage();
    }

    // Update is called once per frame
    void Update()
    {
        num += Time.deltaTime * speed;

        if (Input.GetKeyDown("a"))
        {
            if (dashStageKey_D == true) 
            {
                dashStageKey_A = false;
            }//เช็คไม่ให้ทำซ้อนกัน
            else
            {
                dashStageKey_A = true;
                num = 0;    
            }        
        }
        if (dashStageKey_A == true)
        {
            DashKeyA();
        }

        if (Input.GetKeyDown("d"))
        {
            if (dashStageKey_A == true)
            {
                dashStageKey_D = false;
            }//เช็คไม่ให้ทำซ้อนกัรน
            else
            {
                dashStageKey_D = true;
                num = 0;
            } 
        }
        if (dashStageKey_D == true)
        {
            DashKeyD();
        }
    }
    void  DashKeyA() //สั่งให้เคลื่อน
    {
        if (numStagePlayer == 1)
        {
            hP.DestoyHp();
            dashStageKey_A = false;
        }
        else if (numStagePlayer == 2)
        {
            Left();

        }
        if (numStagePlayer == 3)
        {
            Mid();
        }
    }
    void DashKeyD() //สั่งให้เคลื่อน
    {
        if (numStagePlayer == 1)
        {
            Mid();
        }
        else if (numStagePlayer == 2)
        {            
            Rigth();
        }
        else if (numStagePlayer == 3)
        {
            hP.DestoyHp();
            dashStageKey_D = false;
        }
    }
    void Stage() //เช็คตำแหน่งที่อยู่
    {
        if (transform.position == new Vector3(1.5f, 1.2f, transform.position.z))
        {
            numStagePlayer = 1;
        }
        if (transform.position == new Vector3(5f, 1.2f, transform.position.z))
        {
            numStagePlayer = 2;
        }
        if (transform.position == new Vector3(8.5f, 1.2f, transform.position.z))
        {
            numStagePlayer = 3;
        }
    }
    
    void Left() //Left  x 1.5 ต่ำแหน่งที่จะไป
    {
        Vector3 leftDash = new Vector3(1.5f, 1.2f, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, leftDash, num);
        Stage();
        numStageGo = 1;
        if (numStageGo == numStagePlayer)
        {
            dashStageKey_A = false;
            dashStageKey_D = false;
        }
        
    }
    
    void Mid() //Mid   x 5 ต่ำแหน่งที่จะไป
    {
        Vector3 midDash = new Vector3(5, 1.2f, transform.position.z);        
        transform.position = Vector3.Lerp(transform.position, midDash, num);
        Stage();
        numStageGo = 2;       
        if (numStageGo == numStagePlayer)
        {
            dashStageKey_A = false;
            dashStageKey_D = false;
        }
    }
    
    void Rigth() //Rigth x 8.5 ต่ำแหน่งที่จะไป
    {
        Vector3 rigthDash = new Vector3(8.5f, 1.2f, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, rigthDash, num);
        Stage();
        numStageGo = 3;        
        if (numStageGo == numStagePlayer)
        {
            dashStageKey_A = false;
            dashStageKey_D = false;
        }
    }


}
