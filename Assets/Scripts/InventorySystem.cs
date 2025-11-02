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
    private GameObject itemToAdd;
    private GameObject slotToEquip;

    //public bool isOpen;
    //public bool isFull;
 
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
        //isOpen = false;
        //isFull = false;
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
            //inventoryScreenUI.SetActive(true);
            //Cursor.lockState = CursorLockMode.None;
            //isOpen = true;
    }

    public void AddToInventory(string itemName)
    {
        // slotToEquip = FindNextSlot();

        // itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), slotToEquip.transform.position, slotToEquip.transform.rotation);
        // itemToAdd.transform.SetParent(slotToEquip.transform);

        // itemList.Add(itemName);
            
        slotToEquip = FindNextSlot();
        if (slotToEquip == null)
        {
            Debug.LogWarning("Tried to add item, but inventory is full!");
            return;
        }

        // GameObject prefab = Resources.Load<GameObject>(itemName);
        // if (prefab == null)
        // {
        //     Debug.LogError($"Could not find prefab for item '{itemName}' in Resources!");
        //     return;
        // }

        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), slotToEquip.transform.position, slotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(slotToEquip.transform);

        itemList.Add(itemName);
    }

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }

        }

        if (counter == 14)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

    private GameObject FindNextSlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return null;
    }
 
}