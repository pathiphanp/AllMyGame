 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBat : Boss
{
    [Header("ShadowMode")]
    [HideInInspector] public BossBat baseBatSpawn;
    [SerializeField] GameObject prefabBatClone;
    [HideInInspector] public bool shadowMode;
    List<GameObject> batClone = new List<GameObject>();
    
    public override void HalfHp()
    {
        
    }
}
