using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceablePreview : MonoBehaviour
{
    public bool canPlace = true;

    // Size of object
    public Vector3 checkSize = new Vector3(0.5f, 0.5f, 0.5f);

    public LayerMask collisionLayers; 
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        CheckCollisions();

        if (sr != null)
        {
            sr.color = canPlace ? Color.blue : Color.red;
        }
    }

    void CheckCollisions()
    {
        // Perform box overlap
        Collider[] hits = Physics.OverlapBox(
            transform.position,
            checkSize / 2f,
            transform.rotation,
            collisionLayers
        );

        // Valid if no collisions
        canPlace = hits.Length == 0;
        // Debug.Log("canPlace = "+ canPlace);
    }
}
