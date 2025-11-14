using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
 
 
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        // If the slot is empty, add item
        if (transform.childCount <= 1)
        {
            DragDrop.itemBeingDragged.transform.SetParent(transform);
            DragDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);

        }
        else // Slot already has an item, check if it's the same
        {
            InventoryItem draggedItem = DragDrop.itemBeingDragged.GetComponent<InventoryItem>();
            InventoryItem storedItem  = GetStoredItem();
            

            // Check if items are the same and if stack limit will not be exceeded
            if (draggedItem.thisName == GetStoredItem().thisName && !IsLimitExceeded(draggedItem))
            {
                GetStoredItem().amountInInventory += draggedItem.amountInInventory;
                DestroyImmediate(DragDrop.itemBeingDragged);
            }
        }

    }

    InventoryItem GetStoredItem()
    {
        // return transform.GetChild(0).GetComponent<InventoryItem>();
        foreach (Transform child in transform)
        {
            InventoryItem item = child.GetComponent<InventoryItem>();
            if (item != null)
                return item;
        }

        return null;
    }
    
    bool IsLimitExceeded(InventoryItem draggedItem)
    {
        if ((draggedItem.amountInInventory + GetStoredItem().amountInInventory) > InventorySystem.Instance.stackLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
 
}