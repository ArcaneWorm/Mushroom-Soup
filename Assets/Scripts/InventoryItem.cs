using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // --- Is this item trashable --- //
    public bool isTrashable;

 
    // --- Consumption --- //
    private GameObject itemPendingConsumption;
    public bool isConsumable;

    public float healthEffect;

    public int amountInInventory = 1;
    public string thisName;
 
    // Triggered when the mouse is clicked over the item that has this script.
    public void OnPointerDown(PointerEventData eventData)
    {
        //Right Mouse Button Click on
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                // Setting this specific gameobject to be the item we want to destroy later
                itemPendingConsumption = gameObject;
                ConsumingFunction(healthEffect);
            }
        }
    }
 
    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                Destroy(gameObject);
                InventorySystem.Instance.ReCalculateList();
                CraftingSystem.Instance.RefreshNeededItems();
            }
        }
    }
 
    private void ConsumingFunction(float healthEffect)
    {
        if (!PlayerState.Instance.AtMaxHealth())
        {
            HealthEffectCalculation(healthEffect);
            Debug.Log("Healed for " + healthEffect + " health!");
        }
        else
        {
            Debug.Log("Already at max health!");
        }
    }
 

    private static void HealthEffectCalculation(float healthEffect)
    {
        // --- Health --- //
 
        float healthBeforeConsumption = PlayerState.Instance.currentHealth;
        float maxHealth = PlayerState.Instance.maxHealth;
 
        if (healthEffect != 0)
        {
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                PlayerState.Instance.SetHealth(maxHealth);
            }
            else
            {
                PlayerState.Instance.SetHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }
 
}
