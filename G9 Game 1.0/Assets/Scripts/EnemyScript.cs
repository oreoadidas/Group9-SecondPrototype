using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public Image energyBar;
    public float energy;
    public float maxEnergy = 100f; // player increases this var when they hit the enemy
    public bool isLazy = true;
    SpriteRenderer sr;
    public Sprite geek; // enemy turns into this sprite once it gets enough energy

    float speed;

    // Use this for initialization
    void Start()
    {
        energy = 0f;
        speed = -4f;
        energyBar.fillAmount = 0;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position; //get the current position of the enemy
        position = new Vector2(position.x + speed * Time.deltaTime, position.y); //Compute the new position of the enemy
        transform.position = position; //update enemy position

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // end of screen
        if (transform.position.x < min.x)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {   // destroy the bullet if it hits an enemy ship
        if (col.tag == "PlayerBulletTag")
        {
            energy += 50f;
            energyBar.fillAmount = energy / maxEnergy;
            if (energy >= 100.0f)
            {
                isLazy = false;
                sr.sprite = geek;
            }
        }
    }
}
