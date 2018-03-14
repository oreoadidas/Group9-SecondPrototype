using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode fire = KeyCode.Space;
    public KeyCode fire1 = KeyCode.F;
    public KeyCode fire2 = KeyCode.E;

    public float speed = 10.0f;
    public float boundY = 2.25f;
    private Rigidbody2D rb2d;

    public GameObject PlayerBullet;
    public GameObject explosion;
    public GameObject EnergyDrink;
    public GameObject Book;
    private int bookAmmo = 6;
    private int energyAmmo = 10;

    //UI:
    public Text energyAmmoText;
    public Text bookAmmoText;

    Animator anim;
    public AudioClip CoffeeThrow;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //UI for ammo:
        energyAmmoText.text = energyAmmo.ToString(); // displays how much energy drinks the player has
        bookAmmoText.text = bookAmmo.ToString(); // displays how much energy drinks the player has

        //fire bullets when spacebar is pressed
        if (Input.GetKeyDown(fire))
        {
            anim.SetBool("Bool",true);
            //instantiate bullet
            GameObject bullet = Instantiate(PlayerBullet);
            bullet.transform.position = transform.position + new Vector3(1f, 0f); //set bullet initial poss
        }

        else
        {
            anim.SetBool("Bool", false);
        }

        // checks if there is any energy Ammo left
        if (energyAmmo >0)
        {
            // fire energy drink if f key is pressed
            if (Input.GetKeyDown(fire1))
            {
                anim.SetBool("Bool",true);
                //instantiate energy drink
                GameObject edrink = Instantiate(EnergyDrink);
                edrink.transform.position = transform.position + new Vector3(1f, 0f); //set energy drink initial poss
                energyAmmo--; // removes an Energy Drink from energy Ammo
            }
        }

        // checks if there is any book Ammo left
        if (bookAmmo > 0)
        {
            // fire book if e key is pressed
            if (Input.GetKeyDown(fire2))
            {
                anim.SetBool("Bool",true);
                //instantiate energy drink
                GameObject book = Instantiate(Book);
                book.transform.position = transform.position + new Vector3(1f, 0f); //set energy drink initial poss
                bookAmmo--; // removes a Book from bookAmmo
            }
        }

        Vector2 vel = rb2d.velocity;
        if (Input.GetKey(moveUp))
        {
            anim.SetInteger("State",1);
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            anim.SetInteger("State",1);
            vel.y = -speed;
        }
        else if (!Input.GetKey(moveUp) || !Input.GetKey(moveDown))
        {
            anim.SetInteger("State",0);
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
