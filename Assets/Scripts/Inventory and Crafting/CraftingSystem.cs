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
    public GameObject farmingScreenUI;
    public GameObject survivalScreenUI;

    public List<string> inventoryItemList = new List<string>();

    // Category Button
    Button creatureBTN;
    Button farmingBTN;
    Button survivalBTN;

    // Craft Button
    Button craftFenceBTN;
    Button craftGateBTN;
    Button craftGardenBedBTN;
    Button craftCampfireBTN;
    Button craftStickBTN;
    Button craftCattailSeedsBTN;

    // Requirement Text
    TextMeshProUGUI stickReq1;
    TextMeshProUGUI fenceReq1;
    TextMeshProUGUI gateReq1;
    TextMeshProUGUI gardenBedReq1;
    TextMeshProUGUI campfireReq1;
    TextMeshProUGUI campfireReq2;
    TextMeshProUGUI cattailSeedReq1;

    bool isOpen;
    bool hasGarden = false;

    // All Blueprints
    public CraftingBlueprint StickBLP = new CraftingBlueprint("Stick", 1, "Tree", 1);
    public CraftingBlueprint FenceBLP = new CraftingBlueprint("Fence", 1, "Tree", 4);
    public CraftingBlueprint GateBLP = new CraftingBlueprint("Gate", 1, "Tree", 5); //?? 3-5 wood
    public CraftingBlueprint CampfireBLP = new CraftingBlueprint("Campfire", 2, "Tree", 2, "Goopy Cattail", 2); // ?? recipe
    public CraftingBlueprint GardenBLP = new CraftingBlueprint("Garden Bed", 1, "Tree", 4);
    public CraftingBlueprint CattailBLP = new CraftingBlueprint("Goopy Cattail Seeds", 1, "Goopy Cattail", 1);

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

        farmingBTN = craftingScreenUI.transform.Find("FarmingButton").GetComponent<Button>();
        farmingBTN.onClick.AddListener(delegate { OpenFarmingCategory(); });

        survivalBTN = craftingScreenUI.transform.Find("SurvivalButton").GetComponent<Button>();
        survivalBTN.onClick.AddListener(delegate { OpenSurvivalCategory(); });

        // Stick
        stickReq1 = survivalScreenUI.transform.Find("Stick").transform.Find("req1").GetComponent<TextMeshProUGUI>();

        craftStickBTN = survivalScreenUI.transform.Find("Stick").transform.Find("CraftButton").GetComponent<Button>();
        craftStickBTN.onClick.AddListener(delegate{ CraftAnyItem(StickBLP); });

        // Fence
        fenceReq1 = creatureScreenUI.transform.Find("Fence").transform.Find("req1").GetComponent<TextMeshProUGUI>();

        craftFenceBTN = creatureScreenUI.transform.Find("Fence").transform.Find("CraftButton").GetComponent<Button>();
        craftFenceBTN.onClick.AddListener(delegate{ CraftAnyItem(FenceBLP); });

        // Gate
        gateReq1 = creatureScreenUI.transform.Find("Gate").transform.Find("req1").GetComponent<TextMeshProUGUI>();

        craftGateBTN = creatureScreenUI.transform.Find("Gate").transform.Find("CraftButton").GetComponent<Button>();
        craftGateBTN.onClick.AddListener(delegate{ CraftAnyItem(GateBLP); });

        // Garden Bed
        gardenBedReq1 = farmingScreenUI.transform.Find("GardenBed").transform.Find("req1").GetComponent<TextMeshProUGUI>();

        craftGardenBedBTN = farmingScreenUI.transform.Find("GardenBed").transform.Find("CraftButton").GetComponent<Button>();
        craftGardenBedBTN.onClick.AddListener(delegate{ CraftAnyItem(GardenBLP); });

        // Campfire

        campfireReq1 = survivalScreenUI.transform.Find("Campfire").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        campfireReq2 = survivalScreenUI.transform.Find("Campfire").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftCampfireBTN = survivalScreenUI.transform.Find("Campfire").transform.Find("CraftButton").GetComponent<Button>();
        craftCampfireBTN.onClick.AddListener(delegate{ CraftAnyItem(CampfireBLP); });

        // Goopy Cattail Seeds
        cattailSeedReq1 = farmingScreenUI.transform.Find("GoopyCattailSeeds").transform.Find("req1").GetComponent<TextMeshProUGUI>();

        craftCattailSeedsBTN = farmingScreenUI.transform.Find("GoopyCattailSeeds").transform.Find("CraftButton").GetComponent<Button>();
        craftCattailSeedsBTN.onClick.AddListener(delegate{ CraftAnyItem(CattailBLP); });

    }

    void OpenCreatureCategory()
    {
        craftingScreenUI.SetActive(false);
        creatureScreenUI.SetActive(true);
    }

    void OpenFarmingCategory()
    {
        craftingScreenUI.SetActive(false);
        farmingScreenUI.SetActive(true);
    }

    void OpenSurvivalCategory()
    {
        craftingScreenUI.SetActive(false);
        survivalScreenUI.SetActive(true);
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
        if(blueprintToCraft.itemName.Equals("Garden Bed"))
        {
            hasGarden = true;
        }
        else if(blueprintToCraft.itemName.Equals("Goopy Cattail Seeds"))
        {
            // Add another seed
            InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);
        }
        

        // Refresh list
        // StartCoroutine(calculate());
        InventorySystem.Instance.ReCalculateList();

        RefreshNeededItems();
    }

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
            farmingScreenUI.SetActive(false);
            survivalScreenUI.SetActive(false);
            isOpen = false;
        }

        RefreshNeededItems(); // Belongs at the beginning of Update(), not working
    }

    public void RefreshNeededItems()
    {
        int wood_count = 0;
        int cattail_count = 0;

        // Get every InventoryItem in the inventory
        InventoryItem[] allItems = InventorySystem.Instance.inventoryScreenUI.GetComponentsInChildren<InventoryItem>();

        foreach (InventoryItem item in allItems)
        {
            switch (item.thisName)
            {
                case "Tree":
                    wood_count += item.amountInInventory;
                    break;

                case "Goopy Cattail":
                    cattail_count += item.amountInInventory;
                    break;
            }
        }


        // ---- Stick ---- //

        stickReq1.text = "1 Wood [" + wood_count + "]";

        if (wood_count >= 1)
        {
            craftStickBTN.gameObject.SetActive(true);
        }
        else
        {
            craftStickBTN.gameObject.SetActive(false);
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


        // ---- Gate ---- //

        gateReq1.text = "5 Wood [" + wood_count + "]";

        if (wood_count >= 5)
        {
            craftGateBTN.gameObject.SetActive(true);
        }
        else
        {
            craftGateBTN.gameObject.SetActive(false);
        }


        // ---- Garden Bed ---- //

        gardenBedReq1.text = "4 Wood [" + wood_count + "]";

        if (wood_count >= 4)
        {
            craftGardenBedBTN.gameObject.SetActive(true);
        }
        else
        {
            craftGardenBedBTN.gameObject.SetActive(false);
        }


        // ---- Campfire ---- //

        campfireReq1.text = "2 Wood [" + wood_count + "]";
        campfireReq2.text = "2 Cattails [" + cattail_count + "]";

        if (wood_count >= 2 && cattail_count >= 2)
        {
            craftCampfireBTN.gameObject.SetActive(true);
        }
        else
        {
            craftCampfireBTN.gameObject.SetActive(false);
        }

        // ---- Goopy Cattail Seeds ---- //
        cattailSeedReq1.text = "1 Goopy Cattail [" + cattail_count + "]";

        if(cattail_count >= 1 && hasGarden)
        {
            craftCattailSeedsBTN.gameObject.SetActive(true);
        }
        else
        {
            craftCattailSeedsBTN.gameObject.SetActive(false);
        }
    }
}
