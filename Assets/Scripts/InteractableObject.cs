using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // public string itemName;
    // public bool playerInRange;

    // public string GetItemName()
    // {
    //     return itemName;
    // }

    // private void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Mouse0) && playerInRange 
    //     && SelectionManager.Instance.onTarget && gameObject.CompareTag("Collectable"))
    //     {
    //         // If inventory is not full
    //         if (!InventorySystem.Instance.CheckIfFull())
    //         {
    //             InventorySystem.Instance.AddToInventory(itemName);
    //             Destroy(gameObject);
    //         }
    //         else // Inventory is full
    //         {
    //             Debug.Log("Inventory is full!");
    //         }
            
    //     }

    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         playerInRange = true;
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         playerInRange = false;
    //     }
    // }
    [SerializeField] private string itemName = "DefaultItem";

    public string GetItemName()
    {
        return itemName;
    }

    // This is called by SelectionManager when the player clicks
    public void OnInteract()
    {
        Debug.Log("Picked up: " + itemName);

        // Check if inventory is full
        if (!InventorySystem.Instance.CheckIfFull())
        {
            InventorySystem.Instance.AddToInventory(itemName);

            // Destroy the object in the world after adding it
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }
}
