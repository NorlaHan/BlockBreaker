using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPull : MonoBehaviour {

	public Text text;

	private Ball ball;
	//private Brick brick;
	private int powerCount;


	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
		//brick = GameObject.FindObjectOfType<Brick>();
		PowerPullCount();
	}

	public void PowerPullCount() {
		powerCount = ball.ability;
		text.text = (" Power pull : " + powerCount + "         remains: " + Brick.breakableCount);
		print (" Power pull : " +powerCount);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
