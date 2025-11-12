using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    [SerializeField] string itemName = "DefaultItem";
    [SerializeField] bool inRange = false;


    public string GetItemName()
    {
        return itemName;
    }

    // This is called by SelectionManager when the player clicks
    public void OnInteract()
    {
        Debug.Log("Picked up: " + itemName);

        if (SelectionManager.Instance.selectedObject == gameObject) {
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
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range.");
            inRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited range.");
            inRange = false;
        }
    }

    public bool IsInRange()
    {
        return inRange;
    }
}
