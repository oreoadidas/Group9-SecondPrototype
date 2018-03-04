using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = true; //display cursor
        Cursor.lockState = CursorLockMode.None; //unlocks the cursor
	}

    IEnumerator DelayPlay()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        SceneManager.LoadScene(1);
    }

    IEnumerator DelayMainMenu()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        SceneManager.LoadScene("MainMenu");
    }

    public void DelayPlay(string Level1)
    {
        StartCoroutine(DelayPlay());
    }

    public void DelayMenu(string MainMenu)
    {
        StartCoroutine(DelayMainMenu());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
