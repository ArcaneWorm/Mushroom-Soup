using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }
    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    // How many items you can have in a stack
    public int stackLimit = 5;

    private GameObject itemToAdd;
    private GameObject slotToEquip;


 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        PopulateSlotList();
    }
 
    private void PopulateSlotList()
    {
        foreach(Transform child in inventoryScreenUI.transform){
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
            
    }

    public void AddToInventory(string itemName)
    {
        GameObject stack = CheckIfStackExists(itemName);

        if (stack != null)
        {
            stack.GetComponent<InventorySlot>().itemInSlot.amountInInventory++; 
        }
        else
        {
           slotToEquip = FindNextSlot();
            if (slotToEquip == null)
            {
                Debug.LogWarning("Tried to add item, but inventory is full!");
                return;
            }

            itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), slotToEquip.transform.position, slotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(slotToEquip.transform);

            itemList.Add(itemName); 
        }
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        // int counter = amountToRemove;

        // for (var i = slotList.Count - 1; i >= 0; i--)
        // {
        //     if (slotList[i].transform.childCount > 0)
        //     {
        //         if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
        //         {
        //             //Destroy the object immediately in case we need to add a crafted object right after
        //             DestroyImmediate(slotList[i].transform.GetChild(0).gameObject);
        //             counter -= 1;
        //         }
        //     }
        // }

        int remaining = amountToRemove;

        // Loop through slots from last to first
        for (int i = slotList.Count - 1; i >= 0; i--)
        {
            if (remaining <= 0)
                break;

            if (slotList[i].transform.childCount > 1)
            {
                InventoryItem item = slotList[i].transform.GetChild(0).GetComponent<InventoryItem>();

                if (item.thisName == nameToRemove)
                {
                    // Case 1 — Stack has enough items to cover the removal
                    if (item.amountInInventory > remaining)
                    {
                        item.amountInInventory -= remaining;
                        remaining = 0;
                    }
                    else
                    {
                        // Case 2 — Stack has less or exactly the amount needed 
                        remaining -= item.amountInInventory;

                        // Remove the whole stack
                        DestroyImmediate(item.gameObject);
                    }
                }
            }
    }

    // Update the inventory list after changes
    ReCalculateList();
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 1)
            {
                counter += 1;
            }

        }

        if (counter == 9)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

    public GameObject FindNextSlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount <= 1)
            {
                return slot;
            }
        }

        return null;
    }

    public void ReCalculateList()
    {
        itemList.Clear();

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name; // Material (Clone)
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");

                itemList.Add(result);
            }
        }
    }
    
    public GameObject CheckIfStackExists(string itemName)
    {
        foreach (GameObject slot in slotList)
        {
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            if (inventorySlot != null && inventorySlot.itemInSlot != null)
            {
                if (inventorySlot.itemInSlot.thisName == itemName &&
                    inventorySlot.itemInSlot.amountInInventory < stackLimit)
                {
                    return slot;
                }
            }
        }
        return null;
    }



}