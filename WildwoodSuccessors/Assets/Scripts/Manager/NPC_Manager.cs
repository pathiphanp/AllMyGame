using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{
    [Header("NPC Body")]
    public GameObject merchantBoat;
    public GameObject wizardBoat;

    [Header("Position NPC")]
    public GameObject startPoisitionBoat;
    public GameObject dockShopPosition;
    public GameObject dockShopPositionExit;

    [Header("Value")]
    public float merchantBoatSpeed;

    [Header("Test")]
    public bool testSwitch;

    [Header("Interface NPC")]
    public bool merchantHere;
    public bool wizardHere;

    [Header("NPC Son and path")]
    public GameObject sonGameObject;
    public float sonSpeed;
    public Transform[] sonPath;
    int sonNoPoint;

    [Header("NPC Mom and path")]
    public GameObject momGameObject;
    public float momSpeed;
    public Transform[] momPath;
    [SerializeField] int momNoPoint;

    [Header("NPC Manage")]
    public bool testNPC;
    [SerializeField] internal bool isDay;

    [Header("Interface shop")]
    public GameObject interfaceMerchent;
    public GameObject interfaceWizard;

    public void Start()
    {
        sonNoPoint = 0;
        // sonGameObject.transform.position = sonPath[0].position;
        // momGameObject.transform.position = momPath[0].position;
    }

    private void Update()
    {
        CheckNpcBoat();
        CheckTime();
    }

    public void FixedUpdate()
    {
        //NpcBoatShow();
        if(GameManagerPor.Instance.timeCycle.dayMerchantValue <= 0)
        {
            MerchantMove();
        }
        if(GameManagerPor.Instance.timeCycle.dayMerchantValue == 1)
        {
            WizardMove();
        }

        // if (testNPC)
        // {
        //     NpcSon(sonPath, sonGameObject, momSpeed);
        //     NpcMom(momPath, momGameObject, momSpeed);
        // }
    }

    public void MerchantMove()
    {
        if (GameManagerPor.Instance.timeCycle.hours >= 18f && GameManagerPor.Instance.timeCycle.hours < 19)
        {
            merchantBoat.transform.position = Vector2.MoveTowards(merchantBoat.transform.position, dockShopPositionExit.transform.position, Time.deltaTime * merchantBoatSpeed);
        }

        else if (GameManagerPor.Instance.timeCycle.hours >= 11f && GameManagerPor.Instance.timeCycle.hours < 18)
        {
            merchantBoat.transform.position = Vector2.MoveTowards(merchantBoat.transform.position, dockShopPosition.transform.position, Time.deltaTime * merchantBoatSpeed);
        }
        else
        {
            merchantBoat.transform.position = startPoisitionBoat.transform.position;
        }
    }

    public void WizardMove()
    {
        if (GameManagerPor.Instance.timeCycle.hours >= 18f && GameManagerPor.Instance.timeCycle.hours < 19)
        {
            wizardBoat.transform.position = Vector2.MoveTowards(wizardBoat.transform.position, dockShopPositionExit.transform.position, Time.deltaTime * merchantBoatSpeed);
        }

        else if (GameManagerPor.Instance.timeCycle.hours >= 11f && GameManagerPor.Instance.timeCycle.hours < 18)
        {
            wizardBoat.transform.position = Vector2.MoveTowards(wizardBoat.transform.position, dockShopPosition.transform.position, Time.deltaTime * merchantBoatSpeed);
        }
        else
        {
            wizardBoat.transform.position = startPoisitionBoat.transform.position;
        }
    }

    public void CheckNpcBoat()
    {
        if (merchantBoat.transform.position == dockShopPosition.transform.position)
        {
            merchantHere = true;
            interfaceMerchent.SetActive(true);
        }
        else
        {
            merchantHere = false;
            interfaceMerchent.SetActive(false);
        }

        if (wizardBoat.transform.position == dockShopPosition.transform.position)
        {
            wizardHere = true;
            interfaceWizard.SetActive(true);
        }
        else
        {
            wizardHere = false;
            interfaceWizard.SetActive(false);
        }
    }

    public void NpcBoatShow()
    {
        if (merchantHere && GameManagerPor.Instance.stateCamera == StateInGame.ShopArea)
        {
            merchantBoat.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            //interfaceMerchent.SetActive(false);
            merchantBoat.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        if(wizardHere && GameManagerPor.Instance.stateCamera == StateInGame.ShopArea)
        {
            wizardBoat.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            wizardBoat.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void CheckTime()
    {
        if(GameManagerPor.Instance.timeCycle.hours >= 6 && GameManagerPor.Instance.timeCycle.hours < 19)
        {
            isDay = true;
        }

        if(GameManagerPor.Instance.timeCycle.hours >= 19 || GameManagerPor.Instance.timeCycle.hours < 6)
        {
            isDay = false;
        }
    }
    public void NpcSon(Transform[] path,GameObject npc , float speed)
    {
        npc.transform.position = Vector2.MoveTowards(npc.transform.position, path[sonNoPoint].position, speed * Time.deltaTime);
        if (isDay)
        {
            if (Vector2.Distance(npc.transform.position, path[sonNoPoint].position) < 0.05f)
            {
                if (sonNoPoint != path.Length - 1)
                {
                    sonNoPoint++;
                }
            }
        }
        else
        {
            if (Vector2.Distance(npc.transform.position, path[sonNoPoint].position) < 0.05f)
            {
                if (sonNoPoint != 0)
                {
                    sonNoPoint--;
                }
            }
        }
    }

    public void NpcMom(Transform[] path, GameObject npc, float speed)
    {
        npc.transform.position = Vector2.MoveTowards(npc.transform.position, path[momNoPoint].position, speed * Time.deltaTime);
        if (isDay)
        {
            if (Vector2.Distance(npc.transform.position, path[momNoPoint].position) < 0.05f)
            {
                if (momNoPoint != path.Length - 1)
                {
                    momNoPoint++;
                }
            }
        }
        else
        {
            if (Vector2.Distance(npc.transform.position, path[momNoPoint].position) < 0.05f)
            {
                if (momNoPoint != 0)
                {
                    momNoPoint--;
                }
            }
        }
    }
}
