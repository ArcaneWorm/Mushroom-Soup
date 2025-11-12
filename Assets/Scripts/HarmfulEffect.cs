using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulEffect : MonoBehaviour
{
    [SerializeField] int damage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerState.Instance.currentHealth -= damage;
            if (gameObject.CompareTag("Projectile"))
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerState.Instance.currentHealth -= damage;
        }
    }
}
