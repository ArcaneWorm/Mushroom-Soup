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
        craftFenceBTN.onClick.AddListener(delegate{ CraftAnyItem(); });
    }

    void OpenCreatureCategory()
    {
        craftingScreenUI.SetActive(false);
        creatureScreenUI.SetActive(true);
    }

    void CraftAnyItem()
    {
        // Add item into inventory

        // Remove resources from inventory
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
