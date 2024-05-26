using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Dictionary<string, InventoryUI> inventoryUIByName = new Dictionary<string, InventoryUI>();

    public List<InventoryUI> inventoryUIs;

    public static Slot_UI draggedSlot;
    public static Image draggedIcon;

    public Button saveTestButton;

    private void Awake()
    {
        Initialized();
    }

    void Start()
    {
        
    }

    private void Update()
    {

    }

    public InventoryUI GetInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            return inventoryUIByName[inventoryName];
        }

        Debug.LogWarning("There is not inventory ui for" + inventoryName);
        return null;
    }

    private void Initialized()
    {
        foreach (InventoryUI ui in inventoryUIs)
        {
            if (!inventoryUIByName.ContainsKey(ui.inventoryName))
            {
                inventoryUIByName.Add(ui.inventoryName, ui);
            }
        }
    }

    public void RefreshInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }

    public void RefreshAll()
    {
        foreach (KeyValuePair<string, InventoryUI> keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }
    }

    public void GotoScene(int i)
    {
        SceneManager.LoadScene(i);
        SaveManager.Instance.LoadSlotPlayer();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
