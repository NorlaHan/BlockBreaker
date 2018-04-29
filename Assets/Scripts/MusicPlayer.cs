using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;
	public AudioClip[] audioClip;
	private LevelManager levelmanager;

	void Awake () {
		Debug.Log ("Music player Awake " + GetInstanceID());
		if (instance != null) {
			Destroy (gameObject);
			Debug.Log ("Duplicated music player instance " + GetInstanceID() + " Destroyed!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			Debug.Log ("The instance carry on is " + GetInstanceID());
		} 
	}

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Music player Start " + GetInstanceID());

	}

	// Change music according to the level.
	public void ChangeMusic (){
		this.GetComponent<AudioSource>().clip = audioClip [Application.loadedLevel];
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
