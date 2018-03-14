using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public GameObject pauseUI; //pause game UI
    public bool isPaused; //checks if the game is paused or not
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        pauseUI.SetActive(false); //game starts out without the UI overlay since it's not paused
        GetComponent<Movement>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (Time.timeScale == 1)
            {
                isPaused = true;
                player.GetComponent<Movement>().enabled = false;
                pauseUI.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                isPaused = false;
                player.GetComponent<Movement>().enabled = true;
                pauseUI.SetActive(false);
                Time.timeScale = 1;
            }
    }
}
