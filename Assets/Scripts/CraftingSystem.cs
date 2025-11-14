using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; set; }
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
    //public CraftingBlueprint StickBLP = new CraftingBlueprint("Stick", 2, "Tree", 1);
    public CraftingBlueprint FenceBLP = new CraftingBlueprint("Fence", 1, "Tree", 4);
    //public CraftingBlueprint GateBLP = new CraftingBlueprint("Gate", 1, "Tree", 5); //?? 3-5 wood
    //public CraftingBlueprint CampfireBLP = new CraftingBlueprint("Campfire", 2, "Tree", 3, "Stick", 3); // ?? recipe
    //public CraftingBlueprint GardenBLP = new CraftingBlueprint("GardenBed", 1, "Tree", 4);

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

        // Add item into inventory
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        // Refresh list
        // StartCoroutine(calculate());
        InventorySystem.Instance.ReCalculateList();

        RefreshNeededItems();
    }

    // public IEnumerator calculate()
    // {
    //     yield return new WaitForSeconds(1f);

    //     InventorySystem.Instance.ReCalculateList();
    // }

    // Update is called once per frame
    void Update()
    {
        //RefreshNeededItems(); // Won't open crafting system with this, won't update amount of wood collected with ... ?? FIXME

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            Debug.Log("c is pressed");
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

    public void RefreshNeededItems()
    {
        int wood_count = 0;
        int cattail_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Tree":
                    wood_count += 1;
                    break;
                case "Goopy Cattail":
                    cattail_count += 1;
                    break;
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
