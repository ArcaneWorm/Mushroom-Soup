using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public GameObject tutorialUI;

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.T))
        {
            tutorialUI.SetActive(!tutorialUI.activeSelf);
        } 
    }
}
