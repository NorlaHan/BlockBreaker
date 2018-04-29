using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {

	private LevelManager levelmanager;

	void OnTriggerEnter2D (Collider2D Ball) {
		//print ("Trigger");
		levelmanager.LoadLevel("LoseScreen");

	}

	void OnCollisionEnter2D (Collision2D Ball) {
		print ("Collide");
	}

	// Use this for initialization
	void Start () {
		levelmanager = GameObject.FindObjectOfType<LevelManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
