using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode fire = KeyCode.Space;

    public float speed = 10.0f;
    public float boundY = 2.25f;
    private Rigidbody2D rb2d;

    public GameObject PlayerBullet;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //fire bullets when spacebar is pressed
        if (Input.GetKeyDown(fire))
        {
            //instantiate bullet
            GameObject bullet = Instantiate(PlayerBullet);
            bullet.transform.position = transform.position + new Vector3(1f, 0f); //set bullet initial poss
        }

        Vector2 vel = rb2d.velocity;
        if (Input.GetKey(moveUp))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            vel.y = -speed;
        }
        else if (!Input.GetKey(moveUp) || !Input.GetKey(moveDown))
        {
            vel.y = 0;
        }
        rb2d.velocity = vel;

        var pos = transform.position;
        if (pos.y > boundY)
        {
            pos.y = boundY;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }
        transform.position = pos;
    }

    //collision:
    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            Destroy(gameObject); //destroy player's ship
        }
    }

    // instantiate explosion
    void PlayExplosion()
    {
        GameObject boom = (GameObject)Instantiate(explosion);

        //set position of explosion
        boom.transform.position = transform.position;
    }*/
}
