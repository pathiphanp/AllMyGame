using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantedArea : MonoBehaviour
{
    public GameObject field;
    public GameObject plants;
    public GameObject selectFrame;
    public BoxCollider2D boxCollider;
    public bool isReady;
    //public NotificationFarming notification;

    public void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        plants.GetComponent<PlantFarming>().plantedArea = this;
    }
    public void Start()
    {
        
    }
    public void Update()
    {
        
    }
    public void harvest()
    {
        field.SetActive(true);
        plants.SetActive(true);
        boxCollider.enabled = false;
    }
    private void OnMouseOver()
    {
        selectFrame.SetActive(true);
    }

    private void OnMouseExit()
    {
        selectFrame.SetActive(false);
    }
}
