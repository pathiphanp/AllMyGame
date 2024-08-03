using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ItemShopList itemShopList;
    public GameObject shopWindow;
    [Header("Shop detail display")]
    public TextMeshProUGUI itemName;
    public Image itemImage;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI rentPrice;

    [Header("Money")]
    public TextMeshProUGUI moneyMerchantText;
    public TextMeshProUGUI moneyWizardText;

    [Header("Item list")]
    public GameObject[] itemList;

    void Start()
    {
        RandomItemInShop();
    }
    private void Update()
    {
        moneyMerchantText.text = GameManagerPor.Instance.dataManager.money.ToString() + "$";
        moneyWizardText.text = GameManagerPor.Instance.dataManager.money.ToString() + "$";
    }

    public void OpenShopWimdow()
    {
        shopWindow.SetActive(true);
    }

    public void CloseShopWindow()
    {
        shopWindow.SetActive(false);
    }

    #region Set on/off
    public void SetOff(GameObject i)
    {
        i.SetActive(false);
    }

    public void SetOn(GameObject i)
    {
        i.SetActive(true);
    }
    #endregion

    #region Shop function
    public void BuyItem()
    {
        int canBuy = GameManagerPor.Instance.dataManager.money - itemShopList.itemShop.price;
        if (canBuy >= 0)
        {
            InventoryData.Instance.inventory.Add("Backpack", itemShopList.itemData, 1);
            GameManagerPor.Instance.dataManager.money -= itemShopList.itemShop.price;
        }
        else
        {
            Debug.Log("Don't have enoung money");
        }
    }

    public void BuyItem2()
    {
        int canBuy = GameManagerPor.Instance.dataManager.money - itemShopList.itemShop.price;
        if (canBuy >= 0)
        {
            InventoryData.Instance.inventory.Add(InventoryData.Instance.inventory.backpack, itemShopList.itemData, 1);
            GameManagerPor.Instance.dataManager.money -= itemShopList.itemShop.price;
        }
        else
        {
            Debug.Log("Don't have enoung money");
        }
    }

    public void SelectItem(ItemShopList i)
    {
        itemName.text = i.itemShop.name;
        itemImage.sprite = i.itemShop.image;
        itemDescription.text = i.itemShop.description;
        itemPrice.text = i.itemShop.price.ToString() + " $";

        itemShopList = i;
    }
    #endregion
    #region Wizard function
    public void PayRent(Button btn)
    {
        if(GameManagerPor.Instance.dataManager.isDept == true)
        {
            btn.interactable = true;
        }
        int canPay = GameManagerPor.Instance.dataManager.money - GameManagerPor.Instance.dataManager.rent;
        if(canPay >= 0)
        {
            GameManagerPor.Instance.dataManager.money -= GameManagerPor.Instance.dataManager.rent;
            GameManagerPor.Instance.dataManager.isDept = false;
            btn.interactable = false;
        }
        else
        {
            Debug.Log("Can't Pay Rent!");
        }
    }

    public void WizardBillDisplay()
    {
        rentPrice.text = GameManagerPor.Instance.dataManager.rent.ToString() + " $";
    }

    public void CheckIsDepth(Button btn)
    {
        if (GameManagerPor.Instance.dataManager.isDept)
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }
    }

    #endregion

    public void RandomItemInShop()
    {
        foreach (GameObject obj in itemList)
        {
            obj.SetActive(false);
        }

        for(int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, itemList.Length);
            Debug.Log(randomIndex);
            itemList[randomIndex].SetActive(true);
        }
    }
}
