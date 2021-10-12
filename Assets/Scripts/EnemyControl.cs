using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    GameObject scoreUITextGO;

    public GameObject ExplosionGO; //Explosion Prefab

    float speed; //Enemy speed



    // Start is called before the first frame update
    void Start()
    {
        speed = 2f; //Set enemy speed

        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag"); //Get score text UI
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position; //Enemy current position

        position = new Vector2(position.x, position.y - speed * Time.deltaTime); //Enemy new position

        transform.position = position; //Update enemy new position

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //Bottom left of the screen

        if (transform.position.y < min.y) //If enemy leaves the screen
        {
            Destroy(gameObject); //Then destroy the enemy
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")) //If collision detected
        {

            PlayExplosion();

            scoreUITextGO.GetComponent<GameScore>().Score += 100; //Increment score 

           Destroy(gameObject); //Destroy enemy ship

        }

    }

    void PlayExplosion() //Instantiate explosion
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position; //Set position of explosion
    }
}