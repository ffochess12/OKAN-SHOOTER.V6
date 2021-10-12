using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
	public GameObject GameManagerGO;

	public GameObject PlayerBulletGO; //Bullet prefab
	public GameObject bulletPosition01;
	public GameObject bulletPosition02;
	public GameObject ExplosionGO; //Explosion prefab

	public Text LivesUIText; //Lives UI Text

	const int MaxLives = 3; //Maximum Lives
	int lives; //Current lives

	public float speed;

	public void Init()
    {
		lives = MaxLives;

		LivesUIText.text = lives.ToString(); //Update lives

		transform.position = new Vector2(0, 0); //Player poisiton is reset to the center

		gameObject.SetActive(true); //Player game object is set to active
    }

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown("space")) //If space is pressed 
        {
			GetComponent<AudioSource>().Play(); //play laser sound


			GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO); //instantiate bullet
			bullet01.transform.position = bulletPosition01.transform.position;

			GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
			bullet02.transform.position = bulletPosition02.transform.position;

        }

		float x = Input.GetAxisRaw("Horizontal");//the value will be -1, 0 or 1 (for left, no input, and right)
		float y = Input.GetAxisRaw("Vertical");//the value will be -1, 0 or 1 (for down, no input, and up)

		//now based on the input we compute a direction vector, and we normalize it to get a unit vector
		Vector2 direction = new Vector2(x, y).normalized;

		//noe we call the function that computes and sets the player's position
		Move(direction);
	}

	void Move(Vector2 direction)
	{
		//find the screen limits to the player's movement (left, right, top and bottom edges of the screen)
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //this is the bottom-left point (corner) of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //this is the top-right point (corner) of the screen

		max.x = max.x - 0.225f; //subtract the player sprite half width
		min.x = min.x + 0.225f; //add the player sprite half width

		max.y = max.y - 0.285f; //subtract the player sprite half height
		min.y = min.y + 0.285f; //add the player sprite half height

		//Get the player's current position
		Vector2 pos = transform.position;

		//Calculate the new position
		pos += direction * speed * Time.deltaTime;

		//Make sure the new position is outside the screen
		pos.x = Mathf.Clamp(pos.x, min.x, max.x);
		pos.y = Mathf.Clamp(pos.y, min.y, max.y);

		//Update the player's position
		transform.position = pos;
	}

	void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag")) //If collision detected
        {

			PlayExplosion();

			lives--;
			LivesUIText.text = lives.ToString();

            if (lives == 0)
            {

				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

				gameObject.SetActive(false);
				//Destroy(gameObject);
			}
			

        }
    }

	void PlayExplosion() //Insantiate explosion
    {
		GameObject explosion = (GameObject)Instantiate (ExplosionGO);

		explosion.transform.position = transform.position; //Set explosion position
    }
}