using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	public static bool autoPlay = false;
	public Sprite[] paddleSprite;

	private Ball ball;
	private float mousePosInBlock;
	private float ballPosInBlock;
	private Vector3 paddlePos;

	private float clampMin;
	private float clampMax;

	private enum paddle {square_L, trapezoid};
	private paddle paddleStates = paddle.square_L;


//	void Awake (){
//		if (paddle != null) {
//			Destroy (gameObject);
//		}else {
//			paddle = this;
//			GameObject.DontDestroyOnLoad(gameObject);
//		}
//	}


	// Use this for initialization
	void Start ()
	{
		ball = GameObject.FindObjectOfType<Ball> ();
		if (ball != null) {
			Debug.Log ("ball finded !");
			}
		paddlePos = new Vector3 (0.5f,this.transform.position.y,0f);
		PaddleShape();
	}
	
	// Update is called once per frame
	void Update ()	{
		if (!autoPlay) {MoveWithMouse ();} else {AutoPlay ();}
		if (Input.GetMouseButtonDown (2)) {if (!autoPlay) {autoPlay = true;} else {autoPlay = false;}}

		// TODO make paddle shape change with game rule instead of pressing button. 
		if (Input.GetKeyDown (KeyCode.R)) {
			if (paddleStates == paddle.square_L) {
				paddleStates = paddle.trapezoid;
				PaddleShape();
			} else if (paddleStates == paddle.trapezoid) {
				paddleStates = paddle.square_L;
				PaddleShape();
			}
		}
		// TODO make the auto play continue all over the level.
	}
	
	void PaddleShape ()
	{
		int spriteIndex;
		if (paddleStates == paddle.square_L) {
			clampMin = 0.5f;
			clampMax = 7.5f;
			spriteIndex = 0;
			this.GetComponent<SpriteRenderer>().sprite = paddleSprite [spriteIndex];
			this.GetComponent<PolygonCollider2D>().enabled = false;
			this.GetComponent<BoxCollider2D>().enabled = true;
		} else if (paddleStates == paddle.trapezoid) {
			clampMin = 0.625f;
			clampMax = 7.375f;
			spriteIndex = 1;
			this.GetComponent<SpriteRenderer>().sprite = paddleSprite [spriteIndex];
			this.GetComponent<BoxCollider2D>().enabled = false;
			this.GetComponent<PolygonCollider2D>().enabled = true;
		}
	}

	void MoveWithMouse () {
		mousePosInBlock = (Input.mousePosition.x / Screen.width * 8);
		//print (mousePosInBlock);
		paddlePos.x = Mathf.Clamp (mousePosInBlock,clampMin,clampMax);
		this.transform.position = paddlePos;
	}

	void AutoPlay() {
		// ball position will always in game unit. No need to normalize it as mouse does.
		ballPosInBlock = ball.transform.position.x;
		//Debug.Log("ball.transform.position.x " + ball.transform.position.x);
		paddlePos.x = Mathf.Clamp (ballPosInBlock, clampMin, clampMax);
		//Debug.Log("ballPosInBlock " + ballPosInBlock);
		this.transform.position = paddlePos;
	}

	void OnCollisionEnter2D (Collision2D Ball) {
		//print ("Paddle Collude the Ball");

	}
}
