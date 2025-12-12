using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateInteration : InteractableObject
{

    public SpriteRenderer spriteRenderer;
    public BoxCollider gateCollider;

    public Sprite closedSprite;
    public Sprite openSprite;

    private bool isOpen = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnInteract()
    {
        Debug.Log("Interacted with gate");
        AudioManager.Instance.PlaySFX(audioSource.clip);

        // Change open/close condition
        if (isOpen)
        {
            CloseGate();
        }
        else
        {
            OpenGate();
        }

        // Update sprite
        spriteRenderer.sprite = isOpen ? openSprite : closedSprite;
    }

    public void OpenGate()
    {
        isOpen = true;
        // Disable collider when opened
        gateCollider.enabled = false;
    }

    public void CloseGate()
    {
        isOpen = false;
        gateCollider.enabled = true;
    }

}
