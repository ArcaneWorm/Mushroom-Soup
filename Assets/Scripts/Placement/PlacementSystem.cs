using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public static PlacementSystem Instance;

    private GameObject previewObj;
    private BuildableItem currentItem;
    private bool canPlace = true;

    private void Awake() { Instance = this; }

    private void Update()
    {

        if (previewObj == null) return;

        UpdatePreviewPosition();
        CheckPlacementValidity();

        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            PlaceObject();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CancelPlacement();
        }
    }

    public void StartPlacement(BuildableItem item)
    {
        currentItem = item;
        previewObj = Instantiate(item.previewPrefab);
    }

    private void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane ground = new Plane(Vector3.up, Vector3.zero); // y = 0 ground

        if (ground.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            previewObj.transform.position = hitPoint;
        }
    }

    private void CheckPlacementValidity()
    {
       PlaceablePreview preview = previewObj.GetComponent<PlaceablePreview>();

        if (preview != null)
        {
            canPlace = preview.canPlace;
            // Debug.Log("PlacementSystem canPlace = " + canPlace);
        }
    }

    private void PlaceObject()
    {
        Instantiate(
            currentItem.placedPrefab,
            previewObj.transform.position,
            previewObj.transform.rotation
        );

        InventorySystem.Instance.RemoveItem(currentItem.itemID, 1);

        CancelPlacement();
    }

    private void CancelPlacement()
    {
        if (previewObj != null) Destroy(previewObj);

        previewObj = null;
        currentItem = null;
    }

}

