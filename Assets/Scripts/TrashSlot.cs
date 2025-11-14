using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashSlot : MonoBehaviour, IDropHandler
{
    GameObject draggedItem
    {
        get
        {
            return DragDrop.itemBeingDragged;
        }
    }
 
    GameObject itemToBeDeleted;
 
    public string itemName
    {
        get
        {
            string name = itemToBeDeleted.name;
            string toRemove = "(Clone)";
            string result = name.Replace(toRemove, "");
            return result;
        }
    }
 
    public void OnDrop(PointerEventData eventData)
    {
        if (draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            itemToBeDeleted = draggedItem.gameObject;
            DeleteItem();
            gameObject.SetActive(false);
        }
        
    }
 
    private void DeleteItem()
    {
        Debug.Log("Threw away " + itemName);
        DestroyImmediate(itemToBeDeleted.gameObject);
        InventorySystem.Instance.ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }
}
