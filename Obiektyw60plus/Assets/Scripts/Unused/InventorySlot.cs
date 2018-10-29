using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;

    Inventory inventory;

    Item item;

    void Start()
    {
        inventory = Inventory.instance;
    }

    public void AddItem(Item itemToAdd)
    {
        item = itemToAdd;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }


    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        inventory.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}