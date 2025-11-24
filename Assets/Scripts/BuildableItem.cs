using UnityEngine;

[CreateAssetMenu(menuName = "Buildable Item")]
public class BuildableItem : ScriptableObject
{
    public string itemID;                     // Match inventory itemName
    public GameObject placedPrefab;           // The real object
    public GameObject previewPrefab;          // Transparent ghost
    public float maxPlaceDistance = 5f;
}
