using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthCounter;

    public GameObject playerState;

    private float currentHealth, maxHealth;
    private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;

        float fillVal = currentHealth / maxHealth;
        slider.value = fillVal;

        healthCounter.text = currentHealth + "/" + maxHealth;
    }
}
