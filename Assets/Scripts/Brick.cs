using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	public static int breakableCount = 0;
	public AudioClip crack;
	public AudioClip crash;
	public AudioClip powerUp;
	public Sprite[] hitSprites;
	public GameObject smoke;

	private Ball ball;
	private PowerPull powerPull;
	private int maxHits;
	private int timesHit;
	private LevelManager levelmanager;
	private bool isBreakable;
	private bool isPower;
	private Color startColor;
	//test
	//private Sprite[] test;

	void Awake ()
	{
		// Adds breakableCount for every brick with Breakable tag.
		isBreakable = (this.tag == "Breakable");
		isPower = (this.tag == "Power");
		if (isBreakable) {
			breakableCount++;
			Debug.Log ("Breakable count is " + breakableCount);
		}
	}

	// Use this for initialization
	void Start ()
	{
		timesHit = 0;
		maxHits = hitSprites.Length + 1;
		levelmanager = GameObject.FindObjectOfType<LevelManager> ();
		ball = GameObject.FindObjectOfType<Ball>();
		powerPull = GameObject.FindObjectOfType<PowerPull>();
		startColor = gameObject.GetComponent<SpriteRenderer>().color;
		//test
		//if (test != null) {print ("test is not null");}else {print ("test is null");}
	}

	void OnCollisionExit2D (Collision2D Ball)
	{
		if (isBreakable) {
			AudioSource.PlayClipAtPoint (crack, transform.position, 2f);
			HandleHits ();
		} else if (isPower) {
			ball.ability++;
			powerPull.PowerPullCount();
			AudioSource.PlayClipAtPoint (powerUp, transform.position, 2f);
		} 
	}

	void HandleHits ()
	{
		timesHit++;
		//print (timesHit);
		//SimulateWin();
		if (timesHit >= maxHits) {
			AudioSource.PlayClipAtPoint (crash, transform.position, 0.5f);
			breakableCount--;
			levelmanager.BrickDestroyed();
			SmokePuff ();
			Destroy (gameObject);
		} else {LoasSprits();}
	}

	void SmokePuff() {
		GameObject smokePuff;
		smoke.GetComponent<ParticleSystem> ().startColor = startColor;
		smokePuff = Instantiate (smoke, gameObject.transform.position, Quaternion.identity);
	}

	// Load sprites according to the hit.
	void LoasSprits ()
	{
		int spriteIndex = (timesHit - 1);
		// if the sprites element exist, then switch it to the Index sprite. Or do nothing.
		if (hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer> ().sprite = hitSprites [spriteIndex];
			//Debug.LogError ("Brick sprite found");
		} else {Debug.LogError ("Brick sprite missing");}
	}

	// TODO Remove this method once we can actually win!
	void SimulateWin ()	{
		levelmanager.LoadNextLevel();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
