using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public TextMeshProUGUI amountTXT;
    public InventoryItem itemInSlot;
    public bool isBuildable = false; // If item is placeable

    private void Update()
    {
        InventoryItem item = CheckInventoryItem();

        if (item != null)
        {
            itemInSlot = item;
            if (item.buildableData != null)
            {
                isBuildable = true;
            }
            else
            {
                isBuildable = false;
            }
        }
        else
        {
            itemInSlot = null;
            isBuildable = false;
        }

        if (itemInSlot != null)
        {
            amountTXT.gameObject.SetActive(true);
            amountTXT.text = $"{itemInSlot.amountInInventory}";
            amountTXT.transform.SetAsLastSibling();
        }
        else
        {
            amountTXT.gameObject.SetActive(false);
        }
    }

    private InventoryItem CheckInventoryItem()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<InventoryItem>())
            {
                return child.GetComponent<InventoryItem>();
            }
        }
        return null;
    }

    public void OnSlotClicked()
    {
        if (itemInSlot == null) return;

        // If this slot has buildableData AND it matches the item in this slot
        if (isBuildable)
        {
            PlacementSystem.Instance.StartPlacement(itemInSlot.buildableData);
        }
    }

}
