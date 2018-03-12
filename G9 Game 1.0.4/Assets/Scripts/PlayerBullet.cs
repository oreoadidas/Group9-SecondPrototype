using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed;

    // Use this for initialization
    void Start()
    {
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position; //gets the bullet's current pos
        position = new Vector2(position.x + speed * Time.deltaTime, position.y); //calculate the bullet's new pos
        transform.position = position; //update bullet's pos
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //top-right of the screen
        if (transform.position.x > max.x) //if the bullet goes off the screen, it gets destroyed
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            bool lazy = col.GetComponent<EnemyScript>().isLazy;
            if (lazy)
            {
                Destroy(gameObject);
            }
        }
    }
}
