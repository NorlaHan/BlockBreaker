using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speedModifier = 5;

	private PowerPull power;
	private Paddle paddle;
	private Vector3 paddleToBallVector;
	private Vector3 ballToPaddleStart;
	private Vector2 bounceBackVector;
	private bool hasStarted = false;
	private AudioSource triggerAudio;
	public int ability = 3;

	// Use this for initialization
	void Start () {
		ability = 3;
		paddle = GameObject.FindObjectOfType<Paddle>();
		power = GameObject.FindObjectOfType<PowerPull>();
		triggerAudio = GetComponent<AudioSource>();

		paddleToBallVector = this.transform.position - paddle.transform.position;
		ballToPaddleStart = paddleToBallVector;
		print (paddleToBallVector);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		Vector2 tweak = new Vector2 (Random.Range(-0.2f, 0.2f),Random.Range(-0.2f, 0.2f));
		//print (tweak);
		if (hasStarted) {
			triggerAudio.Play();
			this.GetComponent<Rigidbody2D>().velocity = (this.GetComponent<Rigidbody2D>().velocity + tweak).normalized*speedModifier ;
		}

	}


	void Launch ()
	{
		if (hasStarted == false) {
			// Lock the ball relative to the paddle.
			this.transform.position = paddle.transform.position + paddleToBallVector;

			// Wait for a mouse press to launch.
			if (Input.GetMouseButtonDown (1)) {
				print ("Mouse clicked, launch Ball");
				hasStarted = true;
				//this.GetComponent<Rigidbody2D>().velocity = new Vector2 (2f, 10f);
				this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (1f , 1.73f).normalized*speedModifier;
			}
		}
	}

	void Cheat ()
	{
		if (hasStarted == true & ability >0) {
			// Bounce the ball toward to the paddle.
			if (Input.GetMouseButtonDown (0)) {
				Debug.Log ("Ball bounce back to paddle.");
				paddleToBallVector = paddle.transform.position - this.transform.position;
				bounceBackVector = new Vector2 (paddleToBallVector.x, paddleToBallVector.y);
				this.GetComponent<Rigidbody2D> ().velocity = bounceBackVector.normalized*speedModifier;
				ability--;
				power.PowerPullCount();

				// Reset the ball to the paddle.
				// Noticed BUG : Reset after bounce will cause ball lock under the paddle out of the view port.
			} 
			//else if (Input.GetMouseButtonDown (2)) {
			//	this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,0);
			//	Debug.Log ("ballToPaddleStart is " + ballToPaddleStart);
			//	hasStarted = false;
			//	Debug.Log ("Player is a shamless pig.");
				//this.transform.position =new Vector3 (4f ,0.56f ,0f );
			//	}
		}
	}



	void normalize (){
		
		
	}

	// Update is called once per frame
	void Update (){
		//Debug.Log ("paddle.transform.position = " + paddle.transform.position);
		Launch();
		Cheat();

	}
}
