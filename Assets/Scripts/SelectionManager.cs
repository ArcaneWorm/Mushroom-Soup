using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectionManager : MonoBehaviour
{

    // public static SelectionManager Instance { get; set; }
    // public bool onTarget;
    // public GameObject interaction_Info_UI;
    // TextMeshProUGUI interaction_text;

    // private void Start()
    // {
    //     onTarget = false;
    //     interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    // }
    
    // private void Awake()
    // {
    //     if (Instance != null && Instance != this)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         Instance = this;
    //     }
    // }
 
    // void Update()
    // {
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     RaycastHit hit;
    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         var selectionTransform = hit.transform;
    //         InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

    //         if (interactable && interactable.playerInRange)
    //         {
    //             onTarget = true;
                
    //             interaction_text.text = interactable.GetItemName();
    //             interaction_Info_UI.SetActive(true);
    //         }
    //         else
    //         {
    //             onTarget = false;
    //             interaction_Info_UI.SetActive(false);
    //         }

    //     }
    //     else
    //     {
    //         onTarget = false;
    //         interaction_Info_UI.SetActive(false);
    //     }
    // }
        public static SelectionManager Instance { get; private set; }

    [Header("UI")]
    public GameObject interactionInfoUI;
    private TextMeshProUGUI interactionText;

    private InteractableObject currentTarget;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        interactionText = interactionInfoUI.GetComponent<TextMeshProUGUI>();
        interactionInfoUI.SetActive(false);
    }

    void Update()
    {
        HandleHover();
        HandleClick();
    }

    private void HandleHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            InteractableObject interactable = hit.transform.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                currentTarget = interactable;
                interactionText.text = interactable.GetItemName();
                interactionInfoUI.SetActive(true);
                return;
            }
        }

        // If nothing was hit or not interactable
        currentTarget = null;
        interactionInfoUI.SetActive(false);
    }

    private void HandleClick()
    {
        if (Input.GetMouseButtonDown(0) && currentTarget != null)
        {
            currentTarget.OnInteract();
        }
    }
}
