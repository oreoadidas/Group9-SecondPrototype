using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GcuHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 1f;
    public int score = 0;
    public Text scoreText;
    public Image healthBar;
    public AudioClip healthLoss;
    public AudioClip inspiredStudent;

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = 1;
        scoreText.text = "Score: " + score.ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            bool lazy = col.GetComponent<EnemyScript>().isLazy;
            if (lazy)
            {
                StartCoroutine(PlaySoundOnHealthLoss());
                healthBar.fillAmount = health / maxHealth;
                GcuFail();
                health -= 10f;
            }

            bool book = col.GetComponent<EnemyScript>().hasBook;
            if (book)
            {
                healthBar.fillAmount = health / maxHealth;
                GcuFail();
                if (health < 100f)
                {
                    health += 10f;
                }
            }


            if (!lazy)
            {
                StartCoroutine(PlaySoundOnInspiredStudent());
                score++;
                scoreText.text = "Score: " + score.ToString();
            }
        }
    }

    void GcuFail()
    {
        if (health <= 0f)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    IEnumerator PlaySoundOnHealthLoss()
    {
        GetComponent<AudioSource>().PlayOneShot(healthLoss);
        yield return new WaitForSeconds(
        GetComponent<AudioSource>().clip.length);
    }

    IEnumerator PlaySoundOnInspiredStudent()
    {
        GetComponent<AudioSource>().PlayOneShot(inspiredStudent);
        yield return new WaitForSeconds(
        GetComponent<AudioSource>().clip.length);
    }
}
