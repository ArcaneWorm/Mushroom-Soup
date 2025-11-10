using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }
    public GameObject interactionInfoUI;
    private TextMeshProUGUI interactionText;

    private InteractableObject currentTarget;

    public GameObject selectedObject;

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

            if (interactable != null && interactable.IsInRange())
            {
                currentTarget = interactable;
                selectedObject = interactable.gameObject;
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
        if (Input.GetMouseButtonDown(0) && currentTarget != null && currentTarget.IsInRange())
        {
            currentTarget.OnInteract();
        }
    }
}
