using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEnemyFromList : Singleton<RemoveEnemyFromList>
{
    public GameObject Remove(List<GameObject> listData, GameObject target)
    {
        GameObject outTarget = null;
        foreach (GameObject l in listData)
        {
            if (target == l)
            {
                outTarget = l;
            }
        }
        return outTarget;
    }
}
