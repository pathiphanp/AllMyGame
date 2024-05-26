using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailItem : MonoBehaviour
{
    public UnityEngine.UI.Image mainSprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    // Start is called before the first frame update
    void Start()
    {
        mainSprite.sprite = null;
        mainSprite.color = new Color(1, 1, 1, 0);
        nameText.text = "";
        priceText.text = "";
    }

    void OnEnable()
    {
        mainSprite.sprite = null;
        mainSprite.color = new Color(1, 1, 1, 0);
        nameText.text = "";
        priceText.text = "";
    }

    public void SelectItem(Slot_UI i)
    {
        if (i.itemTpye != ItemType.Sell_Only)
        {
            priceText.text = "Can not sell this item.";
        }
        else
        {
            priceText.text = i.price.ToString() + " $";
        }

        mainSprite.color = new Color(1, 1, 1, 1);
        mainSprite.sprite = i.itemIcon.sprite;
        nameText.text = i.type;
    }
}
