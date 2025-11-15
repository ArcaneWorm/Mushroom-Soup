using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject player;
    public GameObject inGameUI;

    public void Setup()
    {
        inGameUI.SetActive(false);
        player.gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
}