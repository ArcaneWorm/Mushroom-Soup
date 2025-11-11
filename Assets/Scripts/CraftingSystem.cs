using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject creatureScreenUI;

    public List<string> inventoryItemList = new List<string>();

    // Category Button
    Button creatureBTN;

    // Craft Button
    Button craftFenceBTN;

    // Requirement Text
    TextMeshProUGUI fenceReq1;

    bool isOpen;

    // All Blueprints
    public CraftingBlueprint FenceBLP = new CraftingBlueprint("Fence", 1, "Wood", 4);


    public static CraftingSystem instance {get; set;}

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        creatureBTN = craftingScreenUI.transform.Find("CreatureCareButton").GetComponent<Button>();
        creatureBTN.onClick.AddListener(delegate { OpenCreatureCategory(); });

        // Fence
        fenceReq1 = creatureScreenUI.transform.Find("Fence").transform.Find("req1").GetComponent<TextMeshProUGUI>();

        craftFenceBTN = creatureScreenUI.transform.Find("Fence").transform.Find("CraftButton").GetComponent<Button>();
        craftFenceBTN.onClick.AddListener(delegate{ CraftAnyItem(FenceBLP); });
    }

    void OpenCreatureCategory()
    {
        craftingScreenUI.SetActive(false);
        creatureScreenUI.SetActive(true);
    }

    void CraftAnyItem(CraftingBlueprint blueprintToCraft)
    {
        // Add item into inventory
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        // Remove resources from inventory
        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if (blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }

        // Refresh list
        //InventorySystem.Instance.ReCalculateList();
        StartCoroutine(calculate());

        RefreshNeededItems();
    }

    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(1f);

        InventorySystem.Instance.ReCalculateList();
    }

    // Update is called once per frame
    void Update()
    {
        //RefreshNeededItems(); // Won't open crafting system with this, won't update amount of wood collected with ... ?? FIXME

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            Debug.Log("i is pressed");
            craftingScreenUI.SetActive(true);
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            creatureScreenUI.SetActive(false);
            isOpen = false;
        }

        RefreshNeededItems(); // Belongs at the beginning of Update(), not working
    }

    private void RefreshNeededItems()
    {
        int wood_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Tree":
                    wood_count += 1;
                    break;
                // case ///;
            }
        }

        // ---- Fence ---- //

        fenceReq1.text = "4 Wood [" + wood_count + "]";

        if (wood_count >= 4)
        {
            craftFenceBTN.gameObject.SetActive(true);
        }
        else
        {
            craftFenceBTN.gameObject.SetActive(false);
        }
    }
}
