using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public string plantName;
    // 0 = seed, 1 = sapling, 2 = fully grown
    public int currentStage = 0;
    public bool pickable = false;
    public float stageTimer = 0f;
    public float[] stageTimes = {1.0f, 1.0f, 1.0f}; // How long each stage is
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }


    private void Start()
    {
        SetStage(0);
    }

    private void Update()
    {
        if(currentStage == 2)
        {
            pickable = true;
            return; // fully grown
        }

        stageTimer += Time.deltaTime;

        if(stageTimer >= stageTimes[currentStage])
        {
            AdvanceStage();
        }

    }

    private void SetStage(int stage)
    {
        currentStage = stage;
        // Reset time
        stageTimer = 0f;

        // Change color of sprite
        if(stage == 0)
        {
            sr.color = Color.red;
        }
        else if(stage == 1)
        {
            sr.color = Color.yellow;
        }
        else if(stage == 2)
        {
            sr.color = Color.green;
        }
    }

    private void AdvanceStage()
    {
        if (currentStage + 1 < stageTimes.Length)
        {
            SetStage(currentStage + 1);
        }
    }



}
