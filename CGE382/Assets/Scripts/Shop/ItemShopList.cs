using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopList : MonoBehaviour
{
    public ItemData itemData;
    public ItemShop itemShop;

    Image image;

    public void Start()
    {
        image = GetComponent<Image>();

        image.sprite = itemShop.image;
    }
}
