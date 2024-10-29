using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChance : Singleton<RandomChance>
{
    public void GetRandomItem(int _item)
    {
        List<int> categoryItem = new List<int>();

        //create item
        for (int i = 1; i <= _item; i++)
        {
            categoryItem.Add(i);
        }
        foreach (int _i in categoryItem)//Debug
        {
            Debug.Log(_i);
        }
    }
    public bool GetRandomChance(int _chanceItemA)
    {
        List<int> item = new List<int>();
        int[] itemSwapOrder = new int[100];
        int _chanceItemB = 100 - _chanceItemA;
        //Create Item A
        for (int itemA = 1; itemA <= _chanceItemA; itemA++)
        {
            item.Add(1);
        }
        //Create Item B
        for (int itemB = 1; itemB <= _chanceItemB; itemB++)
        {
            item.Add(2);
        }
        //Spaw Item Order
        for (int i = 0; i < item.Count; i++)
        {
            int rndIndexOrder = Random.Range(0, 100);
            if (itemSwapOrder[rndIndexOrder] == 0)
            {
                itemSwapOrder[rndIndexOrder] = item[i];
            }
            else
            {
                while (itemSwapOrder[rndIndexOrder] != 0)
                {
                    rndIndexOrder++;
                    rndIndexOrder %= itemSwapOrder.Length;
                }
                itemSwapOrder[rndIndexOrder] = item[i];
            }
        }
        int rndItem = Random.Range(0, 100);
        // Debug.Log(itemSwapOrder[rndItem]);
        if (itemSwapOrder[rndItem] == 1)
        {
            return true;
        }
        return false;
    }
}
