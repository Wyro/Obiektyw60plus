using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;
    public bool IsInInventory = false;
    public WandHolder wandHolder;
    EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        GameObject wandGameObject = GameObject.Find("WandHolder");
        wandHolder = wandGameObject.GetComponent<WandHolder>();
    }
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        int SlotOccupied = wandHolder.SlotsNum; //index of slot currently occupied in wand holder

        //bool wasPickedUp = Inventory.instance.Add(item);

        //if (wasPickedUp) //check if we have enough space
        //{
        if (!equipmentManager.IsItemEquipped) //add items only if no item is equipped
        {
            if (!item.IsInInventory) //don't add the item to wand holder if it is already in inventory
            {
                SlotOccupied = wandHolder.OccupyFirstEmptySlot(gameObject);
                wandHolder.IsSlotOccupied[SlotOccupied] = true;
                item.IsInInventory = true;
            }
            else 
            {
                SlotOccupied = wandHolder.CheckFirstEmptySlot();
                //free the slot occupied by the item equipped
                if (SlotOccupied != wandHolder.SlotsNum) wandHolder.IsSlotOccupied[SlotOccupied] = false;

                //activate the script, which the item uses
                equipmentManager.ToggleScript(item, true);

                //equip the item
                equipmentManager.EquipActiveItem();

                //deactivate this script
                gameObject.GetComponent<ItemPickup>().enabled = false; //this isn't doing anything
            }
        }

    }
    void OnDestroy()
    {
        item.IsInInventory = false;
    }
}
