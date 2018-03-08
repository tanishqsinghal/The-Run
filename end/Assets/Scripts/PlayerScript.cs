using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using GoogleMobileAds.Api;

public class PlayerScript : MonoBehaviour {

	public float jumpPower = 10.0f;
	Rigidbody2D myRigidBody;

	private bool isGrounded = false;



	//float posX = 0.0f;

	bool isGameOver = false;

	ChallengeController myChallengeController;

	GameController myGameController;

	public AudioClip jump;
	AudioSource myAudioPlayer;
	public AudioClip scoreSFX;
	public AudioClip deadSFX;
	//public Animator anim;

	void Start () {

		myRigidBody = transform.GetComponent<Rigidbody2D> ();
		//posX = transform.position.x;
		myChallengeController = GameObject.FindObjectOfType<ChallengeController> ();
		myGameController = GameObject.FindObjectOfType<GameController>();
		myAudioPlayer = GameObject.FindObjectOfType<AudioSource>();

		//anim = GetComponent<Animator>();
		//anim.SetBool ("grounded", isGrounded);

	}




	

	void Update () {

		if((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space)) && isGrounded && !isGameOver)
		{
			myRigidBody.AddForce (Vector3.up * (jumpPower * myRigidBody.mass * myRigidBody.gravityScale * 20.0f));
			myAudioPlayer.PlayOneShot (jump);
			isGrounded = false;
			//anim.SetBool("grounded", isGrounded);
		}
		//anim.SetBool("grounded", isGrounded);
		/*if (transform.position.x < posX && !isGameOver) 
		{
			GameOver ();
		}*/
	}



	void GameOver()
	{
		isGameOver = true;
		myAudioPlayer.PlayOneShot (deadSFX);
		myChallengeController.GameOver ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag == "Ground")
		{
			isGrounded = true;
		}

		if(other.collider.tag == "Enemy")
		{
			GameOver ();
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if(other.collider.tag == "Ground")
		{
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if(other.collider.tag == "Ground")
		{
			isGrounded = false;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Star") 
		{
			Destroy (other.gameObject);
			myGameController.IncreamentScore ();
			myAudioPlayer.PlayOneShot (scoreSFX);
		}
	}
}
