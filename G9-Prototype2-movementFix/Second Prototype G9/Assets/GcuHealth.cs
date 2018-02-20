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

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = 1;
        scoreText.text = "Score: " + score.ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyShipTag")
        {
            bool lazy = col.GetComponent<EnemyScript>().isLazy;
            if (lazy)
            {
                healthBar.fillAmount = health / maxHealth;
                GcuFail();
                health -= 10f;
            }

            if (!lazy)
            {
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
}
