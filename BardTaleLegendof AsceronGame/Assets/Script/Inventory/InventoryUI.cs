using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;   // The parent object of all the items
    public GameObject inventoryUI;  // The entire UI

    Inventory inventory;    // Our current inventory

    InventorySlot[] slots;  // List of all the slots

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}